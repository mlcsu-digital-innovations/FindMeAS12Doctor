using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Exceptions;
using Fmas12d.Business.Extensions;
using Fmas12d.Business.Helpers;
using Fmas12d.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Fmas12d.Business.Services
{
  public class UserAssessmentClaimService :
    ServiceBase<Entities.UserAssessmentClaim>,
    IServiceBase,
    IUserAssessmentClaimService
  {
    private readonly IUserService _userService;
    private readonly IContactDetailTypeService _contactDetailTypeService;
    private readonly ILocationDetailService _locationDetailService;
    private readonly IUserNotificationService _notificationService;
    private readonly IDistanceCalculationService _distanceCalculationService;

    public UserAssessmentClaimService(
      ApplicationContext context,
      IUserService userService,
      IUserClaimsService userClaimsService,
      IContactDetailTypeService contactDetailTypeService,
      ILocationDetailService locationDetailService,
      IUserNotificationService notificationService,
      IDistanceCalculationService distanceCalculationService
    )
      : base(context, userClaimsService)
    {
      _contactDetailTypeService = contactDetailTypeService;
      _locationDetailService = locationDetailService;
      _userService = userService;
      _notificationService = notificationService;
      _distanceCalculationService = distanceCalculationService;
    }

    public async Task<UserAssessmentClaimDetail> GetAssessmentAndContactAsync(int assessmentId, int userId)
    {
      UserAssessmentClaimDetail model = await _context
      .Assessments
      .Include(a => a.AmhpUser)
      .Include(a => a.UnsuccessfulAssessmentType)
      .WhereIsActiveOrActiveOnly(true)
      .Where(a => a.Id == assessmentId)
      .Select(UserAssessmentClaimDetail.ProjectFromEntity)
      .SingleOrDefaultAsync();

      if (model == null)
      {
        throw new ModelStateException("Id",
          $"An assessment claim with an id of {assessmentId} was not found.");
      }

      // get the available contact detail types
      IEnumerable<ContactDetailType> contactDetailTypes = 
        await _contactDetailTypeService.GetAsync(userId, true, true);

      model.UserContactDetailTypes = contactDetailTypes.Select(cd => cd).ToList();

      IEnumerable<AssessmentLocation> otherAssessments = 
        await GetOtherAssessments(assessmentId, userId);

      model.PreviousAssessmentLocations =
        otherAssessments.Where(oa => oa.AssessmentDate < model.ScheduledTime).ToList();
      model.NextAssessmentLocations =
        otherAssessments.Where(oa => oa.AssessmentDate > model.ScheduledTime).ToList();

      return model;
    }

    public async Task<UserAssessmentClaim> GetAssessmentClaimAsync(int id)
    {
      UserAssessmentClaim model = await _context
      .UserAssessmentClaims
      .Include(uac => uac.ClaimStatus)
      .Include(uac => uac.Assessment)
        .ThenInclude(a => a.AmhpUser)
      .Include(uac => uac.Assessment)
        .ThenInclude(a => a.CompletedByUser)
      .Include(uac => uac.Assessment)
        .ThenInclude(a => a.UnsuccessfulAssessmentType)
      .WhereIsActiveOrActiveOnly(true)
      .Where(a => a.Id == id)
      .Select(UserAssessmentClaim.ProjectFromEntity)
      .SingleOrDefaultAsync();

      if (model == null)
      {
        throw new ModelStateException("Id",
          $"An assessment claim with an id of {id} was not found.");
      }

      return model;
    }

    public async Task<UserAssessmentClaimResult> ValidateAssessmentClaimAsync(
      int assessmentId,
      int userId,
      UserAssessmentClaimCreate model
    ) {

      Entities.Assessment assessment = await GetAssessmentAndCcgAsync(assessmentId);
      await GetPreviousClaimsCountAsync(userId, assessmentId);

      // ToDo: Sort out rules determining if claim can be made or not

      UserAssessmentClaimResult assessmentClaim = new UserAssessmentClaimResult();
      assessmentClaim.IsValidClaim = true; // ToDo: sort this!
      assessmentClaim.Mileage = await CalculateDistanceAsync(assessment, model);
      
      assessmentClaim.MileagePayment = assessment.IsSuccessful == true 
        ? assessmentClaim.Mileage * assessment.Ccg.SuccessfulPencePerMile
        : assessmentClaim.Mileage * assessment.Ccg.UnsuccessfulPencePerMile;

      assessmentClaim.AssessmentPayment = assessment.IsSuccessful == true
        ? assessment.Ccg.SuccessfulAssessmentPayment
        : assessment.Ccg.FailedAssessmentPayment;

      return assessmentClaim;
    }

    public async Task<UserAssessmentClaimList> GetAssessmentClaimsByUserIdAsync(int userId) {

      UserAssessmentClaimList userAssessmentClaimList = new UserAssessmentClaimList();

      List<UserAssessmentClaim> claims = await _context
      .UserAssessmentClaims
      .Include(uac => uac.Assessment)
      .Include(uac => uac.ClaimStatus)
      .WhereIsActiveOrActiveOnly(true)
      .Where(uac => uac.UserId == userId)
      .Select(UserAssessmentClaim.ProjectFromEntity)
      .ToListAsync();

      userAssessmentClaimList.Claims = claims;

      List<Assessment> assessments = await _context
      .AssessmentDoctors
      .Include(ad => ad.Assessment)
        .ThenInclude(a => a.UnsuccessfulAssessmentType)
      .Include(ad => ad.Assessment)
        .ThenInclude(a => a.Referral)
      .Include(ad => ad.Assessment)
        .ThenInclude(a => a.UserAssessmentClaims)
      .WhereIsActiveOrActiveOnly(true)
      .Where(ad => ad.DoctorUserId == userId)
      .Where(ad => ad.StatusId == AssessmentDoctorStatus.ATTENDED)
      .Where(ad => ad.Assessment.UserAssessmentClaims.Where(uac => uac.UserId == userId).Count() == 0)
      .Select(ad => new Assessment(ad.Assessment, false))
      .ToListAsync();

      userAssessmentClaimList.Assessments = assessments;

      return userAssessmentClaimList;
    }

    public async Task<UserAssessmentClaim> ConfirmAssessmentClaimAsync(
      int assessmentId,
      int userId,
      UserAssessmentClaimCreate model
    ) {

      Entities.Assessment assessment = await GetAssessmentAndCcgAsync(assessmentId);
      await GetPreviousClaimsCountAsync(userId, assessmentId);

      // ToDo: Sort out rules determining if claim can be made or not

      UserAssessmentClaim assessmentClaim = new UserAssessmentClaim();
      assessmentClaim.Mileage = await CalculateDistanceAsync(assessment, model);
      
      assessmentClaim.AssessmentId = assessmentId;
      assessmentClaim.EndPostcode = model.EndPostcode;
      assessmentClaim.IsUsersPatient = model.OwnPatient;
      assessmentClaim.NextAssessmentId = model.NextAssessmentId;
      assessmentClaim.PreviousAssessmentId = model.PreviousAssessmentId;
      assessmentClaim.StartPostcode = model.StartPostcode;

      assessmentClaim.IsAttendanceConfirmed =
        await ConfirmDoctorAssessmentAttendance(userId, assessmentId);

      assessmentClaim.UserId = userId;
      assessmentClaim.ClaimStatusId = ClaimStatus.SUBMITTED;
      
      assessmentClaim.ClaimReference = CreateClaimReference(assessmentId, userId);

      assessmentClaim.MileagePayment = CalculateMileagePayment(
        assessment.IsSuccessful.Value,
        assessment.Ccg.SuccessfulPencePerMile,
        assessment.Ccg.UnsuccessfulPencePerMile,
        assessmentClaim.Mileage.Value
      );

      assessmentClaim.AssessmentPayment = CalculateAssessmentPayment(
        assessment.IsSuccessful.Value,
        assessment.Ccg.SuccessfulAssessmentPayment,
        assessment.Ccg.FailedAssessmentPayment
      );

      return await CreateUserAssessmentClaimAsync(assessmentClaim);
    }

    public static decimal CalculateAssessmentPayment(
      bool assessmentSuccessful,
      decimal ccgSuccessfulAssessmentPayment,
      decimal ccgFailedAssessmentPayment
    )
    {
      return assessmentSuccessful 
        ? ccgSuccessfulAssessmentPayment
        : ccgFailedAssessmentPayment;
    }    

    public static decimal CalculateMileagePayment(
      bool assessmentSuccessful,
      decimal ccgSuccessfulPencePerMile,
      decimal ccgUnsuccessfulPencePerMile,
      decimal mileage
    )
    {
      return assessmentSuccessful 
        ? mileage * ccgSuccessfulPencePerMile
        : mileage * ccgUnsuccessfulPencePerMile;
    }

    public static int CreateClaimReference(int assessmentId, int userId)
    {
      // ToDo: temp value until it is determined where this value comes from
      bool isClaimReferenceParsed =
        int.TryParse(
          $"{DateTime.Now.Day}{DateTime.Now.Month}{assessmentId}{userId}",
          out int claimReference
        );  

      return isClaimReferenceParsed ? claimReference : assessmentId;
    }  

    public async Task<IEnumerable<UserAssessmentClaim>> GetAssessmentClaimsListByUserIdAsync(
      int userId
    ) {
      IEnumerable<UserAssessmentClaim> claims = await _context
      .UserAssessmentClaims
      .Include(uac => uac.Assessment)
      .Include(uac => uac.ClaimStatus)
      .WhereIsActiveOrActiveOnly(true)
      .Where(uac => uac.UserId == userId)
      .Select(uac => new UserAssessmentClaim(uac))
      .ToListAsync(); 

      return claims;
    }

    public async Task<UserAssessmentClaim> CreateUserAssessmentClaimAsync(UserAssessmentClaim model)
    {
      Entities.UserAssessmentClaim entity = model.MapToEntity();

      entity.Id = 0;
      entity.IsActive = true;
      entity.ClaimStatusId = ClaimStatus.SUBMITTED;

      UpdateModified(entity);

      _context.Add(entity);

      await _context.SaveChangesAsync();

      return await GetAsync(entity.Id);
    }

    private async Task<IEnumerable<AssessmentLocation>> GetOtherAssessments(
      int assessmentId,
      int userId
    ) {
      IEnumerable<AssessmentLocation> otherAssessments = await _context
      .AssessmentDoctors
      .Include(ad => ad.Assessment)
      .WhereIsActiveOrActiveOnly(true)
      .Where(ad => ad.DoctorUserId == userId)
      .Where(ad => (ad.StatusId == AssessmentDoctorStatus.ALLOCATED) || (ad.StatusId == AssessmentDoctorStatus.ATTENDED))
      .Where(ad => ad.Assessment.Id != assessmentId)
      .Select(ad => new AssessmentLocation {
        Id = ad.AssessmentId,
        Address1 = ad.Assessment.Address1,
        Address2 = ad.Assessment.Address2,
        Address3 = ad.Assessment.Address3,
        Address4 = ad.Assessment.Address4,
        AssessmentDate = 
          ad.Assessment.ScheduledTime == null 
          ? ad.Assessment.MustBeCompletedBy 
          : ad.Assessment.ScheduledTime,
        Postcode = ad.Assessment.Postcode
      })
      .ToListAsync();

      return otherAssessments;

    }

    private async Task<UserAssessmentClaim> GetAsync(
      int id,
      bool activeOnly = true,
      bool asNoTracking = true
      )
    {
      UserAssessmentClaim userAssessmentClaim =
      await _context.UserAssessmentClaims
        .Where(uac => uac.Id == id)
        .WhereIsActiveOrActiveOnly(activeOnly)
        .AsNoTracking(asNoTracking)
        .Select(UserAssessmentClaim.ProjectFromEntity)
        .SingleOrDefaultAsync();

      return userAssessmentClaim;
    }

    private async Task<Entities.Assessment> GetAssessmentAndCcgAsync(int assessmentId) {

      Entities.Assessment assessment = await _context
      .Assessments
      .Include(a => a.Ccg)
      .WhereIsActiveOrActiveOnly(true)
      .Where(a => a.Id == assessmentId)
      .SingleOrDefaultAsync();

      if (assessment == null) 
      {
        throw new ModelStateException("Id",
          $"An assessment with an id of {assessmentId} was not found.");
      } else {
        if (assessment.Ccg == null) 
        {
          throw new ModelStateException("Id",
            $"Unable to determine CCG for assessment with an id of {assessmentId}.");
        }
      }

      return assessment;
    }

    private async Task<bool> ConfirmDoctorAssessmentAttendance(int userId, int assessmentId) {

      bool attendanceConfirmed = await _context
      .AssessmentDoctors
      .WhereIsActiveOrActiveOnly(true)
      .Where(ad => ad.AssessmentId == assessmentId)
      .Where(ad => ad.DoctorUserId == userId)
      .Where(ad => ad.StatusId == AssessmentDoctorStatus.ATTENDED)
      .Select(ad => ad.AttendanceConfirmedByUserId != null)
      .SingleOrDefaultAsync();

      if (!attendanceConfirmed) 
      {
        throw new ModelStateException("Id",
         $"Unable to confirm attendance for user {userId} and assessment {assessmentId}.");
      }

      return attendanceConfirmed;
    }

    private async Task<int> GetPreviousClaimsCountAsync(int userId, int assessmentId) {

      IEnumerable<Entities.UserAssessmentClaim> previousClaims = await _context
      .UserAssessmentClaims
      .WhereIsActiveOrActiveOnly(true)
      .Where(uac => uac.UserId == userId)
      .Where(uac => uac.AssessmentId == assessmentId)
      .ToListAsync();

      if (previousClaims.Count() > 0)
      {
        throw new ModelStateException("Id",
          $"User {userId} has an existing claim for assessment with an id of {assessmentId}.");
      } 
      else
      {
        return previousClaims.Count();
      }
    }

    private async Task<int> CalculateDistanceAsync(
      Entities.Assessment assessment,
      UserAssessmentClaimCreate model
    ) {
      Location assessmentLocation = 
        await _locationDetailService.GetPostcodeDetailsAsync(assessment.Postcode);
      Location startLocation = 
        await _locationDetailService.GetPostcodeDetailsAsync(model.StartPostcode);
      
      Location endLocation;

      endLocation =
        model.StartPostcode == model.EndPostcode 
        ? startLocation 
        : await _locationDetailService.GetPostcodeDetailsAsync(model.EndPostcode);

      decimal outDistance = await _distanceCalculationService.CalculateRoadDistanceBetweenPoints(
        startLocation.Latitude,
        startLocation.Longitude,
        assessmentLocation.Latitude,
        assessmentLocation.Longitude
      );

      decimal inDistance =
        model.StartPostcode == model.EndPostcode
        ? outDistance
        : await _distanceCalculationService.CalculateRoadDistanceBetweenPoints(
          assessmentLocation.Latitude,
          assessmentLocation.Longitude,
          endLocation.Latitude,
          endLocation.Longitude
          );

      return (int)(Decimal.Round(outDistance) + Decimal.Round(inDistance));
    }
  }
}