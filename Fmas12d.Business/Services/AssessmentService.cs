using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Exceptions;
using Fmas12d.Business.Extensions;
using Fmas12d.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Fmas12d.Business.Helpers;

namespace Fmas12d.Business.Services
{
  public class AssessmentService :
    ServiceBaseNoAutoMapper<Entities.Referral>,
    IServiceBaseNoAutoMapper,
    IAssessmentService
  {
    private readonly ILocationDetailService _locationDetailService;
    private readonly IReferralService _referralService;
    private readonly IUserService _userService;
    private readonly IUserAvailabilityService _userAvailabilityService;

    public AssessmentService(
      ApplicationContext context,
      ILocationDetailService locationDetailService,
      IReferralService referralService,
      IUserService userService,
      IUserAvailabilityService userAvailabilityService)
      : base(context)
    {
      _locationDetailService = locationDetailService;
      _referralService = referralService;
      _userService = userService;
      _userAvailabilityService = userAvailabilityService;
    }

    private async Task<bool> AddAmhpToUserAssessmentNotifications(
      int amhpUserId, Entities.Assessment entity)
    {
      await _userService.CheckUserIsAnAmhpById(amhpUserId);

      if (entity.UserAssessmentNotifications == null)
      {
        entity.UserAssessmentNotifications = new List<Entities.UserAssessmentNotification>();
      }

      Entities.UserAssessmentNotification userAssessmentNotification =
        new Entities.UserAssessmentNotification
        {
          IsActive = true,
          NotificationTextId = Models.NotificationText.SELECTED_FOR_ASSESSMENT,
          UserId = amhpUserId
        };
      UpdateModified(userAssessmentNotification);
      entity.UserAssessmentNotifications.Add(userAssessmentNotification);

      return true;
    }

    private void AddAssessmentDetail(int assessmentDetailTypeId, Entities.Assessment entity)
    {
      Entities.AssessmentDetail assessmentDetail = new Entities.AssessmentDetail()
      {
        AssessmentDetailTypeId = assessmentDetailTypeId,
        IsActive = true
      };
      UpdateModified(assessmentDetail);
      entity.Details.Add(assessmentDetail);
    }

    private void AddAssessmentDetails(IList<int> detailTypeIds, Entities.Assessment entity)
    {
      if (detailTypeIds != null && detailTypeIds.Any())
      {
        if (entity.Details == null)
        {
          entity.Details = new List<Entities.AssessmentDetail>();
        }
        foreach (int assessmentDetailTypeId in detailTypeIds)
        {
          AddAssessmentDetail(assessmentDetailTypeId, entity);
        }
      }
    }

    private async Task<bool> AddLatitudeAndLongitude(string postcode, Entities.Assessment entity)
    {
      Models.Postcode postcodeModel = await 
        _locationDetailService.GetPostcodeDetailsAsync(postcode);

      if (postcodeModel == null)
      {
        throw new ModelStateException("postcode", 
          $"Unable to find a match for postcode {postcode}");
      }
      
      entity.Latitude = postcodeModel.Latitude;
      entity.Longitude = postcodeModel.Longitude;

      return true;
    }    

    private void CheckAssessmentCanBeUpdated(Entities.Assessment entity)
    {
      if (entity.CompletionConfirmationByUserId != null)
      {
        throw new ModelStateException("Id",
          $"The Assessment with an id of {entity.Id} cannot be updated because its completion " +
           "has been confirmed.");
      }
    }    

    private void CheckAssessmentDoesNotAlreadyHaveAnOutcome(Entities.Assessment entity)
    {
      if (entity.IsSuccessful.HasValue ||
          entity.CompletedTime.HasValue)
      {
        throw new AssessmentAlreadyHasOutcomeException(
          entity.Id,
          entity.IsSuccessful,
          entity.CompletedTime,
          entity.CompletedByUser?.DisplayName
        );
      }
    }

    private async Task<bool> CheckReferralDoesNotAlreadyHaveACurrentAssessment(
      AssessmentCreate model)
    {
      bool hasCurrentAssessment = await _referralService.HasCurrentAssessment(model.ReferralId);
      if (hasCurrentAssessment)
      {
        throw new ModelStateException("ReferralId",
        $"The Referral with an id of {model.ReferralId} already has a current assessment.");
      }
      return true;
    }

    private async Task<Models.Referral> GetReferral(int referralId)
    {
      Models.Referral referral = await _referralService.GetAsync(referralId, true, false);
      if (referral == null)
      {
        throw new ModelStateException("ReferralId",
        $"Cannot find an active Referral with an id of {referralId}.");
      }
      return referral;
    }    

    /// <summary>
    /// Sets the assessment entity's properties from the provided business model
    /// The CCG id is set from the referral patient's ccg, it's duplicated on the assessment so 
    /// that the patient's CCG is fixed at the time of the assessment so if they change their CCG
    /// after the assessment has taken place to won't change the CCG of the claim 
    /// </summary>
    public virtual async Task<AssessmentCreate> CreateAsync(AssessmentCreate model)
    {
      await GetReferral(model.ReferralId);
      await CheckReferralDoesNotAlreadyHaveACurrentAssessment(model);

      Entities.Assessment entity = new Entities.Assessment();
      model.MapToEntity(entity);

      entity.CcgId = await _referralService.GetCcgIdFromReferralPatient(model.ReferralId);
      UpdateModified(entity);
      entity.CreatedByUserId = entity.ModifiedByUserId;
      entity.Id = 0;
      entity.IsActive = true;
      AddAssessmentDetails(model.DetailTypeIds, entity);
      await AddAmhpToUserAssessmentNotifications(model.AmhpUserId, entity);
      await AddLatitudeAndLongitude(model.Postcode, entity);      
      _context.Add(entity);

      Entities.Referral referral = _context.Referrals.Find(model.ReferralId);
      referral.ReferralStatusId = Models.ReferralStatus.SELECTING_DOCTORS;

      await _context.SaveChangesAsync();

      model = _context.Assessments
                      .Include(e => e.Details)
                      .Where(e => e.Id == entity.Id)
                      .WhereIsActiveOrActiveOnly(true)
                      .AsNoTracking(true)
                      .Select(AssessmentCreate.ProjectFromEntity)
                      .Single();
      return model;
    }

    public async Task<Models.Assessment> GetAvailableDoctorsAsync(
      int id,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.Assessment entity =
        await _context.Assessments
                .Include(e => e.AmhpUser)
                .Include(e => e.Doctors)
                  .ThenInclude(d => d.DoctorUser)
                .Include(e => e.Referral)
                  .ThenInclude(r => r.Patient)
                .Include(e => e.PreferredDoctorGenderType)
                .Include(e => e.Referral)
                  .ThenInclude(r => r.LeadAmhpUser)                  
                .Include(e => e.Speciality)
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(u => u.Id == id);

      Models.Assessment model = new Models.Assessment(entity);

      model.AvailableDoctors = await _userAvailabilityService.GetAvailableDoctors(
        model.DateTime, true, true);

      foreach (IUserAvailabilityDoctor availabilityDoctor in model.AvailableDoctors)
      {
        availabilityDoctor.Distance = Distance.CalculateDistanceAsCrowFlies(
          entity.Latitude, 
          entity.Longitude,
          availabilityDoctor.Latitude.Value,
          availabilityDoctor.Longitude.Value
        );
      }

      foreach (Iuser)

      return model;
    }

    public async Task<IEnumerable<Models.Assessment>> GetAllFilterByAmhpUserIdAsync(
      int amhpUserId,
      bool asNoTracking,
      bool activeOnly)
    {

      List<Entities.Assessment> entities =
        await _context.Assessments
                      .Where(e => e.AmhpUserId == amhpUserId)
                      .WhereIsActiveOrActiveOnly(activeOnly)
                      .AsNoTracking(asNoTracking)
                      .ToListAsync();

      IEnumerable<Models.Assessment> models =
        entities.Select(e => new Models.Assessment(e)).ToList();

      return models;
    }

    public async Task<Models.Assessment> GetByIdAsync(
      int id,
      bool activeOnly)
    {
      Entities.Assessment entity = await
        _context.Assessments
                .Include(e => e.AmhpUser)
                .Include(e => e.CompletedByUser)
                .Include(e => e.CreatedByUser)
                .Include(e => e.Details)
                  .ThenInclude(d => d.AssessmentDetailType)
                .Include(e => e.Doctors)
                  .ThenInclude(d => d.DoctorUser)
                .Include(e => e.Referral)
                  .ThenInclude(r => r.Patient)
                .Include(e => e.Speciality)
                .Include(e => e.UserAssessmentNotifications)
                  .ThenInclude(u => u.User)
                    .ThenInclude(u => u.ProfileType)
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(true)
                .SingleOrDefaultAsync(u => u.Id == id);

      Models.Assessment model = new Models.Assessment(entity);

      return model;
    }    

    protected async Task<Entities.Assessment> GetEntityByIdAsync(
       int entityId,
       bool asNoTracking,
       bool activeOnly)
    {
      Entities.Assessment entity = await
        _context.Assessments
                .Include(e => e.AmhpUser)
                .Include(e => e.CompletedByUser)
                .Include(e => e.CreatedByUser)
                .Include(e => e.PreferredDoctorGenderType)
                .Include(e => e.Details)
                  .ThenInclude(d => d.AssessmentDetailType)
                .Include(e => e.Doctors)
                  .ThenInclude(d => d.DoctorUser)
                .Include(e => e.Referral)
                  .ThenInclude(r => r.Patient)
                .Include(e => e.Speciality)
                .Include(e => e.UserAssessmentNotifications)
                  .ThenInclude(u => u.User)
                    .ThenInclude(u => u.ProfileType)
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(u => u.Id == entityId);

      return entity;
    }

    private void UpdateAssessmentDetails(AssessmentUpdate model, Entities.Assessment entity)
    {
      if (entity.HasDetails)
      {
        foreach (Entities.AssessmentDetail assessmentDetail in entity.Details)
        {
          UpdateModified(assessmentDetail);
          assessmentDetail.IsActive = false;
        }
      }

      if (model.HasDetailTypeIds)
      {
        if (entity.HasDetails)
        {
          foreach (int detailTypeId in model.DetailTypeIds)
          {
            Entities.AssessmentDetail assessmentDetail =
              entity.Details.SingleOrDefault(d => d.AssessmentDetailTypeId == detailTypeId);
            if (assessmentDetail == null)
            {
              AddAssessmentDetail(detailTypeId, entity);
            }
            else
            {
              assessmentDetail.IsActive = true;
            }
          }
        }
        else
        {
          AddAssessmentDetails(model.DetailTypeIds, entity);
        }
      }
    }

    private async Task<bool> UpdateAmhpUserAssessmentNotification(
      AssessmentUpdate model, Entities.Assessment entity)
    {
      if (entity.HasUserAssessmentNotifications)
      {
        Entities.UserAssessmentNotification currentUserAssessmentNotification =
          entity.UserAssessmentNotifications
                .Where(u => u.IsActive)
                .SingleOrDefault(u => u.UserId == entity.AmhpUserId);

        if (currentUserAssessmentNotification == null)
        {
          throw new EntityNotLoadedException(
            "Assessment",
            model.Id,
            "UserAssessmentNotification.User.ProfileType of AMHP",
            Models.ProfileType.AMHP);
        }
        else
        {

          UpdateModified(currentUserAssessmentNotification);

          if (entity.AmhpUserId != model.AmhpUserId)
          {
            currentUserAssessmentNotification.IsActive = false;

            Entities.UserAssessmentNotification previousUserAssessmentNotification =
              entity.UserAssessmentNotifications
                    .SingleOrDefault(u => u.UserId == model.AmhpUserId);

            if (previousUserAssessmentNotification == null)
            {
              await AddAmhpToUserAssessmentNotifications(model.AmhpUserId, entity);
            }
            else
            {
              previousUserAssessmentNotification.HasAccepted = null;
              previousUserAssessmentNotification.IsActive = true;
              previousUserAssessmentNotification.RespondedAt = null;
              UpdateModified(previousUserAssessmentNotification);
            }
          }
        }
      }
      else
      {
        throw new EntityNotLoadedException(
          "Assessment",
          model.Id,
          "UserAssessmentNotification.User.ProfileType of AMHP",
          Models.ProfileType.AMHP);
      }
      return true;
    }

    public async Task<AssessmentUpdate> UpdateAsync(AssessmentUpdate model)
    {

      Entities.Assessment entity = _context.Assessments
                                           .Include(a => a.Details)
                                           .Include(a => a.UserAssessmentNotifications)
                                            .ThenInclude(a => a.User)
                                           .Where(a => a.Id == model.Id)
                                           .AsNoTracking(false)
                                           .WhereIsActiveOrActiveOnly(true)
                                           .SingleOrDefault();

      CheckAssessmentCanBeUpdated(entity);

      if (entity == null)
      {
        throw new ModelStateException("Id",
          $"An active Assessment with an id of {model.Id} could not be found.");
      }

      UpdateAssessmentDetails(model, entity);
      await UpdateAmhpUserAssessmentNotification(model, entity);

      model.MapToEntity(entity);

      UpdateModified(entity);

      await _context.SaveChangesAsync();

      model = _context.Assessments
                      .Include(e => e.Details)
                      .Where(e => e.Id == entity.Id)
                      .WhereIsActiveOrActiveOnly(true)
                      .AsNoTracking(true)
                      .Select(AssessmentUpdate.ProjectFromEntity)
                      .Single();
      return model;
    }

    /// <summary>
    /// Update the doctor statuses checking that the doctors are those expected
    /// </summary>
    private void UpdateDoctorStatuses(AssessmentOutcome model, Entities.Assessment entity)
    {
      int[] attendingDoctorIds = model.AttendingDoctors.Select(d => d.Id).ToArray();
      Entities.AssessmentDoctor[] allocatedDoctors = entity.Doctors
        .Where(d => d.StatusId == Models.AssessmentDoctorStatus.ALLOCATED)
        .ToArray();
      int[] allocatedDoctorIds = allocatedDoctors.Select(a => a.DoctorUserId).ToArray();

      if (attendingDoctorIds.Except(allocatedDoctorIds).Any())
      {
        throw new ModelStateException(
          "AttendingDoctors",
          "Expected the following doctor user id's:(" +
          $"{string.Join(",", allocatedDoctorIds.OrderBy(id => id))}" +
          ") but received: (" +
          $"{string.Join(",", attendingDoctorIds.OrderBy(id => id))}).");
      }

      foreach (Entities.AssessmentDoctor assessmentDoctor in allocatedDoctors)
      {
        AssessmentOutcomeDoctor assessmentOutcomeDoctor =
          model.AttendingDoctors.Single(d => d.Id == assessmentDoctor.DoctorUserId);

        assessmentDoctor.AttendanceConfirmedByUserId = entity.ModifiedByUserId;
        assessmentDoctor.StatusId = assessmentOutcomeDoctor.Attended
          ? Models.AssessmentDoctorStatus.ATTENDED
          : Models.AssessmentDoctorStatus.NOT_ATTENDED;
        UpdateModified(assessmentDoctor);
      }
    }

    public async Task<AssessmentOutcome> UpdateOutcomeAsync(AssessmentOutcome model)
    {
      if (!model.IsSuccessful && !model.UnsuccessfulAssessmentTypeId.HasValue)
      {
        throw new ModelStateException("UnsuccessfulAssessmentTypeId",
          "The field UnsuccessfulAssessmentTypeId must have a value when the field " +
          "IsSuccessful is true.");
      }

      Entities.Assessment entity = await GetEntityByIdAsync(model.Id, false, true);
      if (entity == null)
      {
        throw new ModelStateException("Id",
          $"An active Assessment with an id of {model.Id} was not found.");
      }
      else if (entity.Referral.ReferralStatusId != Models.ReferralStatus.ASSESSMENT_SCHEDULED)
      {
        throw new ModelStateException("Id",
          $"The Assessment with an id of {model.Id} does not have the correct status " +
          $"[{entity.Referral.ReferralStatusId}]");
      }
      else
      {

        CheckAssessmentDoesNotAlreadyHaveAnOutcome(entity);

        UpdateModified(entity);

        UpdateDoctorStatuses(model, entity);

        entity.CompletedByUserId = entity.ModifiedByUserId;
        entity.CompletedTime = model.CompletedTime;
        entity.IsSuccessful = model.IsSuccessful;
        entity.UnsuccessfulAssessmentTypeId = model.UnsuccessfulAssessmentTypeId;
        entity.Referral.ReferralStatusId = Models.ReferralStatus.AWAITING_REVIEW;

        await _context.SaveChangesAsync();

        entity = await GetEntityByIdAsync(model.Id, false, false);

        model.CompletedTime = entity.CompletedTime ?? default;
        model.IsSuccessful = entity.IsSuccessful ?? default;
        model.UnsuccessfulAssessmentTypeId = entity.UnsuccessfulAssessmentTypeId ?? default;

        if (entity.Doctors.Any())
        {
          model.AttendingDoctors = entity.Doctors
            .Select(doctor => new AssessmentOutcomeDoctor
            {
              Attended = doctor.StatusId == Models.AssessmentDoctorStatus.ATTENDED,
              Id = doctor.DoctorUserId
            })
            .ToList();
        }

        return model;
      }
    }
  }
}
