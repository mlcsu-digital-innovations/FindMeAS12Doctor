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
  public class AssessmentService :
    ServiceBase<Entities.Assessment>,
    IServiceBase,
    IAssessmentService
  {
    private readonly IContactDetailsService _contactDetailsService;
    private readonly ILocationDetailService _locationDetailService;
    private readonly IReferralService _referralService;
    private readonly IUserService _userService;
    private readonly IUserAvailabilityService _userAvailabilityService;
    private readonly IUserNotificationService _userNotificationService;
    private readonly IDistanceCalculationService _distanceCalculationService;

    public AssessmentService(
      ApplicationContext context,
      IContactDetailsService contactDetailsService,
      ILocationDetailService locationDetailService,
      IReferralService referralService,
      IUserService userService,
      IUserAvailabilityService userAvailabilityService,
      IUserClaimsService userClaimsService,
      IUserNotificationService notificationService,
      IDistanceCalculationService distanceCalculationService
    )
      : base(context, userClaimsService)
    {
      _contactDetailsService = contactDetailsService;
      _locationDetailService = locationDetailService;
      _referralService = referralService;
      _userService = userService;
      _userAvailabilityService = userAvailabilityService;
      _userNotificationService = notificationService;
      _distanceCalculationService = distanceCalculationService;
    }

    public async Task<IAssessmentDoctorsUpdate> AllocateUnregisteredDoctorAsync(
      int id, 
      IUnregisteredDoctor unregisteredDoctor
    )
    {
      Entities.Assessment entity = await _context
        .Assessments
        .Include(a => a.Doctors)
          .ThenInclude(d => d.Status)
        .Include(a => a.Referral)
        .WhereIsActiveOrActiveOnly(true)
        .Where(a => a.Id == id)
        .SingleOrDefaultAsync();

      if (entity == null)
      {
        throw new ModelStateException("Id",
          $"An active Assessment with an id of {id} was not found.");
      }

      // add the user
      User newUser = new User() {
        DisplayName = unregisteredDoctor.DisplayName,
        GmcNumber = unregisteredDoctor.GmcNumber,
        GenderTypeId = unregisteredDoctor.GenderTypeId,
        IdentityServerIdentifier = Guid.NewGuid().ToString(),
        OrganisationId = 1,
        ProfileTypeId = ProfileType.UNREGISTERED_DOCTOR       
      };
      newUser = await _userService.CreateAsync(newUser); 

      if (newUser.Id == 0)
      {
        throw new ModelStateException("Id",
          $"Unable to create new unregistered doctor.");
      }

      // add the contact detail
      if (unregisteredDoctor.TelephoneNumber != null) {
        ContactDetail contact = new ContactDetail() {
          TelephoneNumber = unregisteredDoctor.TelephoneNumber,
          ContactDetailTypeId = ContactDetailType.BASE,
          UserId = newUser.Id,
          Address1 = "Unregistered User",
          Latitude = 0,
          Longitude = 0
        };

        contact = await _contactDetailsService.CreateAsync(contact);

        if (contact.Id == 0)
        {
          throw new ModelStateException("Id",
            $"Unable to create new unregistered doctor contact.");
        }
      }
     
      // add the assessment allocation
      return await AddAllocatedDoctorDirectAsync(id, newUser.Id, true);

    }

    public async Task<IAssessmentDoctorsUpdate> AddAllocatedDoctorDirectAsync(
      int id,
      int userId,
      bool setHasAccepted
    )
    {
      await _userService.CheckIsADoctorAsync(userId, "UserId", true, true);

      Entities.Assessment entity = await _context
        .Assessments
        .Include(a => a.Doctors)
          .ThenInclude(d => d.Status)
        .Include(a => a.Referral)
        .WhereIsActiveOrActiveOnly(true)
        .Where(a => a.Id == id)
        .SingleOrDefaultAsync();

      if (entity == null)
      {
        throw new ModelStateException("Id",
          $"An active Assessment with an id of {id} was not found.");
      }

      Entities.AssessmentDoctor existingAssessmentDoctor = entity
        .Doctors
        .Where(d => d.StatusId != AssessmentDoctorStatus.REMOVED)
        .SingleOrDefault(d => d.DoctorUserId == userId);

      if (existingAssessmentDoctor != null)
      {
        throw new ModelStateException("UserId",
          $"User Id {userId} is already associated with Assessment Id {id} with a status of " +
          $"{existingAssessmentDoctor.Status.Name}");
      }

      CheckAssessmentHasCorrectReferralStatusToAddAllocatedDoctorDirectly(
        id,
        entity.Referral.ReferralStatusId
      );

      UpdateModified(entity);

      // entity will be in the context from the call to CheckIsADoctorAsync
      Entities.User doctorToAllocate = _context.Users.Find(userId);

      Entities.AssessmentDoctor assessmentDoctor = entity
        .Doctors
        .Where(d => d.StatusId == AssessmentDoctorStatus.REMOVED)
        .SingleOrDefault(d => d.DoctorUserId == userId);

      if (assessmentDoctor == null)
      {
        assessmentDoctor = new Entities.AssessmentDoctor();
      }
      assessmentDoctor.DoctorUserId = userId;
      assessmentDoctor.IsActive = true;
      assessmentDoctor.StatusId = AssessmentDoctorStatus.ALLOCATED;

      if (setHasAccepted == true) {
        assessmentDoctor.HasAccepted = true;
      }

      UpdateModified(assessmentDoctor);

      if (!await AddDoctorAvailabilityToAssessmentDoctorIfAvailableAsync(assessmentDoctor, entity))
      {
        await AddDoctorBaseContactDetailToAssessmentDoctorAsync(
          assessmentDoctor,
          entity
        );
      }

      entity.Doctors.Add(assessmentDoctor);

      AddUserAssessmentNotification(
        entity, userId, NotificationText.ALLOCATED_TO_ASSESSMENT);


      await _context.SaveChangesAsync();
      await SendUnsentNotifications(entity.UserAssessmentNotifications);

      return new AssessmentDoctorsUpdate()
      {
        Id = entity.Id,
        UserIds = entity.Doctors.Where(d => d.StatusId == AssessmentDoctorStatus.ALLOCATED)
                                .Select(d => d.DoctorUserId)
                                .ToList()
      };
    }

    public async Task<IAssessmentDoctorsUpdate> AddAllocatedDoctorsAsync(
      IAssessmentDoctorsUpdate updateModel
    )
    {
      Entities.Assessment entity = await _context
        .Assessments
        .Include(a => a.Doctors)
        .Include(a => a.Referral)
        .WhereIsActiveOrActiveOnly(true)
        .Where(a => a.Id == updateModel.Id)
        .SingleOrDefaultAsync();

      if (entity == null)
      {
        throw new ModelStateException("Id",
          $"An active Assessment with an id of {updateModel.Id} was not found.");
      }
      CheckAssessmentHasCorrectReferralStatusToAddAllocatedDoctors(
        updateModel.Id, entity.Referral.ReferralStatusId
      );
      CheckDoctorsAreSelected(entity, updateModel.UserIds);
      CheckDoctorsAreSelectedAndHaveAccepted(entity, updateModel.UserIds);

      UpdateModified(entity);

      foreach (int userId in updateModel.UserIds)
      {
        Entities.AssessmentDoctor assessmentDoctor =
          entity.Doctors.Single(d => d.DoctorUserId == userId);
        assessmentDoctor.StatusId = AssessmentDoctorStatus.ALLOCATED;
        UpdateModified(assessmentDoctor);

        AddUserAssessmentNotification(
          entity, userId, NotificationText.ALLOCATED_TO_ASSESSMENT);
      }

      await _context.SaveChangesAsync();
      await SendUnsentNotifications(entity.UserAssessmentNotifications);

      return new AssessmentDoctorsUpdate()
      {
        Id = entity.Id,
        UserIds = entity.Doctors.Where(d => d.StatusId == AssessmentDoctorStatus.ALLOCATED)
                                .Select(d => d.DoctorUserId)
                                .ToList()
      };
    }

    public async Task<IAssessmentDoctorsUpdate> AddSelectedDoctorsAsync(
      IAssessmentDoctorsUpdate updateModel
    )
    {
      Assessment model = await GetAvailableDoctorsAsync(updateModel.Id, false, true);

      if (model == null)
      {
        throw new ModelStateException("Id",
          $"An active Assessment with an id of {updateModel.Id} was not found.");
      }

      CheckAssessmentHasCorrectReferralStatusToAddSelectedDoctors(
        model.Id, model.Referral.ReferralStatusId);
      CheckSelectedDoctorsAreAvailable(model, updateModel.UserIds);
      CheckSelectedDoctorsAreNotAlreadySelected(model, updateModel.UserIds);

      // because the assessment entity has already been loaded from the call to 
      // GetAvailableDoctorsAsync we can use find here to obtain it directly from the context
      Entities.Assessment entity = await _context.Assessments.FindAsync(updateModel.Id);
      entity.Referral.ReferralStatusId = ReferralStatus.AWAITING_RESPONSES;
      UpdateModified(entity);

      foreach (int userId in updateModel.UserIds)
      {
        IUserAvailabilityDoctor availabilityDoctor =
          model.AvailableDoctors.Single(ad => ad.UserId == userId);

        Entities.AssessmentDoctor assessmentDoctor = entity
          .Doctors
          .Where(d => d.StatusId == AssessmentDoctorStatus.REMOVED)
          .SingleOrDefault(d => d.DoctorUserId == userId);

        if (assessmentDoctor == null)
        {
          assessmentDoctor = new Entities.AssessmentDoctor();
        }
        assessmentDoctor.ContactDetailId = availabilityDoctor.Location.ContactDetailId;
        assessmentDoctor.Distance = availabilityDoctor.Distance;
        assessmentDoctor.DoctorUserId = userId;
        assessmentDoctor.IsActive = true;
        assessmentDoctor.Latitude = availabilityDoctor.Location.Latitude;
        assessmentDoctor.Longitude = availabilityDoctor.Location.Longitude;
        assessmentDoctor.Postcode = availabilityDoctor.Location.Postcode;
        assessmentDoctor.StatusId = AssessmentDoctorStatus.SELECTED;

        UpdateModified(assessmentDoctor);
        entity.Doctors.Add(assessmentDoctor);

        AddUserAssessmentNotification(
          entity, userId, NotificationText.SELECTED_FOR_ASSESSMENT
        );
      }

      if (entity.Referral.ReferralStatusId == ReferralStatus.SELECTING_DOCTORS)
      {
        entity.Referral.ReferralStatusId = ReferralStatus.AWAITING_RESPONSES;
      }
      else if (entity.Referral.ReferralStatusId == ReferralStatus.RESPONSES_COMPLETE)
      {
        entity.Referral.ReferralStatusId = ReferralStatus.RESPONSES_PARTIAL;
      }

      await _context.SaveChangesAsync();
      await SendUnsentNotifications(entity.UserAssessmentNotifications);

      return new AssessmentDoctorsUpdate()
      {
        Id = entity.Id,
        UserIds = entity.Doctors.Where(d => d.StatusId == AssessmentDoctorStatus.SELECTED)
                                .Select(d => d.DoctorUserId)
                                .ToList()
      };
    }

    public async Task<bool> Complete(int id)
    {
      Entities.Assessment entity = await _context
        .Assessments
        .Include(a => a.Referral)
        .Include(a => a.Doctors)
        .Include(a => a.UserAssessmentNotifications)
        .Where(a => a.Id == id)
        .WhereIsActiveOrActiveOnly(true)
        .SingleOrDefaultAsync();

      if (entity == null)
      {
        throw new ModelStateException("id",
          $"An active Assessment with an id of {id} could not be found.");
      }

      if (entity.Referral.ReferralStatusId != ReferralStatus.AWAITING_REVIEW)
      {
        throw new ModelStateException("id",
          $"An active Assessment with an id of {id} cannot be completed because " +
          $"its Referral Status is {entity.Referral.ReferralStatusId} when it needs to be "+
          $"{ReferralStatus.AWAITING_REVIEW}."
        );
      }

      entity.Referral.ReferralStatusId = ReferralStatus.OPEN;
      UpdateModified(entity.Referral);

      entity.CompletionConfirmationByUserId = _userClaimsService.GetUserId();
      UpdateModified(entity);

      foreach (Entities.AssessmentDoctor assessmentDoctor in entity.Doctors)
      {
        if (assessmentDoctor.StatusId != AssessmentDoctorStatus.REMOVED)
        {
          AddUserAssessmentNotification(
            entity,
            assessmentDoctor.DoctorUserId,
            NotificationText.ASSESSMENT_COMPLETED);
        }
      }

      await _context.SaveChangesAsync();
      await SendUnsentNotifications(entity.UserAssessmentNotifications);

      return true; 
    }

    /// <summary>
    /// Sets the assessment entity's properties from the provided business model
    /// The CCG id is set from the referral patient's ccg, it's duplicated on the assessment so 
    /// that the patient's CCG is fixed at the time of the assessment so if they change their CCG
    /// after the assessment has taken place to won't change the CCG of the claim 
    /// </summary>
    public async Task<AssessmentCreate> CreateAsync(
      AssessmentCreate model
    )
    {
      await GetReferral(model.ReferralId);
      await CheckReferralDoesNotAlreadyHaveACurrentAssessmentAsync(model);

      Entities.Assessment entity = new Entities.Assessment();
      model.MapToEntity(entity);

      entity.CcgId = await _referralService.GetCcgIdFromReferralPatient(model.ReferralId);
      UpdateModified(entity);
      entity.CreatedByUserId = entity.ModifiedByUserId;
      entity.Id = 0;
      entity.IsActive = true;
      AddAssessmentDetails(model.DetailTypeIds, entity);

      await _userService.CheckIsAmhpAsync(model.AmhpUserId, "amhpUserId");
      AddUserAssessmentNotification(
        entity,
        model.AmhpUserId,
        NotificationText.ALLOCATED_TO_ASSESSMENT
      );

      await AddLatitudeAndLongitudeAsync(model.Postcode, entity);
      _context.Add(entity);

      Entities.Referral referral = _context.Referrals.Find(model.ReferralId);
      referral.ReferralStatusId = ReferralStatus.SELECTING_DOCTORS;

      await _context.SaveChangesAsync();
      await SendUnsentNotifications(entity.UserAssessmentNotifications);

      model = _context.Assessments
                      .Include(e => e.Details)
                      .Where(e => e.Id == entity.Id)
                      .WhereIsActiveOrActiveOnly(true)
                      .AsNoTracking(true)
                      .Select(AssessmentCreate.ProjectFromEntity)
                      .Single();
      return model;
    }

    public async Task<Assessment> GetAvailableDoctorsAsync(
      int id,
      bool asNoTracking,
      bool activeOnly
    )
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

      if (entity == null)
      {
        return null;
      }
      else
      {
        Assessment model = new Assessment(entity);

        model.AvailableDoctors = await _userAvailabilityService.GetAvailableDoctorsAsync(
          model.DateTime, true, true);

        // don't include doctors that are already selected or allocated
        model.AvailableDoctors =
         model.AvailableDoctors
         .Where(d1 => !entity.Doctors.Any(
           d2 => d1.UserId == d2.DoctorUserId && d2.StatusId != AssessmentDoctorStatus.REMOVED)
          );

        foreach (IUserAvailabilityDoctor availabilityDoctor in model.AvailableDoctors)
        {

          availabilityDoctor.Distance =
            await _distanceCalculationService.CalculateRoadDistanceBetweenPoints(
              availabilityDoctor.Location.Latitude,
              availabilityDoctor.Location.Longitude,
              entity.Latitude,
              entity.Longitude
            );
        }

        return model;
      }
    }

    public async Task<IEnumerable<Assessment>> GetListByUserIdAsync(
      int userId,
      int? doctorStatusId,
      int? referralStatusId,
      bool asNoTracking,
      bool activeOnly
    )
    {
      int userProfileTypeId = await _userService.GetByProfileTypeIdAsync(
        userId,
        asNoTracking,
        activeOnly
      );

      return userProfileTypeId switch
      {
        0 => throw new ModelStateException("userId",
              $"An active user with an id of {userId} cannot be found."),

        ProfileType.AMHP => await GetListByAmhpUserIdAsync(
          userId, referralStatusId, asNoTracking, activeOnly),

        ProfileType.GP => await GetListByDoctorUserIdAsync(
          userId, doctorStatusId, referralStatusId, asNoTracking, activeOnly),

        ProfileType.PSYCHIATRIST => await GetListByDoctorUserIdAsync(
          userId, doctorStatusId, referralStatusId, asNoTracking, activeOnly),
          
        _ => throw new ModelStateException("userId",
             "Assessments cannot be associated with a User that has a ProfileType of " +
              $"{userProfileTypeId}."),
      };
    }

    public async Task<Assessment> GetByIdAsync(
      int id,
      bool activeOnly,
      bool asNoTracking
    )
    {
      Entities.Assessment entity = await
        _context.Assessments
                .Include(e => e.AmhpUser)
                  .ThenInclude(a => a.ContactDetails)
                .Include(e => e.CompletedByUser)
                .Include(e => e.CreatedByUser)
                .Include(e => e.Details)
                  .ThenInclude(d => d.AssessmentDetailType)
                .Include(e => e.Doctors)
                  .ThenInclude(d => d.DoctorUser)
                .Include(e => e.Doctors)
                  .ThenInclude(d => d.ContactDetail)
                    .ThenInclude(cd => cd.ContactDetailType)
                .Include(e => e.Referral)
                  .ThenInclude(r => r.Patient)
                .Include(e => e.Referral)
                  .ThenInclude(r => r.ReferralStatus)
                .Include(e => e.Speciality)
                .Include(e => e.UserAssessmentNotifications)
                  .ThenInclude(u => u.User)
                    .ThenInclude(u => u.ProfileType)
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(u => u.Id == id);

      foreach(var x in entity.Doctors) {
        Entities.User doctor = await 
          _context.Users
          .Include(u => u.ContactDetails)
            .ThenInclude(c => c.ContactDetailType)
          .Where(u => u.Id == x.DoctorUserId)
          .SingleOrDefaultAsync();

          x.DoctorUser.ContactDetails = doctor.ContactDetails;
      };

      Assessment model = new Assessment(entity);

      return model;
    }

    public async Task<Assessment> GetByIdForUserAsync(
      int id,
      int userId,
      bool asNoTracking,
      bool activeOnly
    )
    {
      Assessment model = await GetByIdAsync(id, activeOnly, asNoTracking);
      // TODO Refactor if too slow
      model.Doctors = model.Doctors.Where(d => d.DoctorUserId == userId).ToList();

      return model;
    }

    public async Task<Assessment> GetSelectedDoctorsAsync(
      int id,
      bool asNoTracking,
      bool activeOnly
    )
    {
      Assessment model =
        await _context.Assessments
                .Include(e => e.AmhpUser)
                .Include(e => e.Doctors)
                  .ThenInclude(d => d.DoctorUser)
                    .ThenInclude(u => u.GenderType)
                .Include(e => e.Doctors)
                  .ThenInclude(d => d.DoctorUser)
                    .ThenInclude(u => u.ProfileType)
                .Include(e => e.Doctors)
                  .ThenInclude(d => d.DoctorUser)
                    .ThenInclude(u => u.UserSpecialities)
                .Include(e => e.Referral)
                  .ThenInclude(r => r.Patient)
                .Include(e => e.PreferredDoctorGenderType)
                .Include(e => e.Referral)
                  .ThenInclude(r => r.LeadAmhpUser)
                .Include(e => e.Speciality)
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .Select(a => new Assessment()
                {
                  AmhpUser = new User()
                  {
                    DisplayName = a.AmhpUser.DisplayName
                  },
                  Doctors = a.Doctors.Select(d => new AssessmentDoctor()
                  {
                    DoctorUser = new User()
                    {
                      DisplayName = d.DoctorUser.DisplayName,
                      GenderType = new GenderType()
                      {
                        Name = d.DoctorUser.GenderType.Name
                      },
                      GmcNumber = d.DoctorUser.GmcNumber,
                      Id = d.DoctorUserId,
                      IsActive = d.DoctorUser.IsActive,
                      ProfileType = new ProfileType()
                      {
                        Name = d.DoctorUser.ProfileType.Name
                      },
                      UserSpecialities = d.DoctorUser
                                          .UserSpecialities
                                          .Select(us => new UserSpeciality()
                                          {
                                            Speciality = new Speciality()
                                            {
                                              Name = us.Speciality.Name
                                            }
                                          }).ToList()
                    },
                    DoctorUserId = d.DoctorUserId,
                    Distance = d.Distance,
                    HasAccepted = d.HasAccepted,                    
                    IsActive = d.IsActive,
                    IsAvailable = true,
                    RespondedAt = d.RespondedAt,
                    StatusId = d.StatusId
                  }).ToList(),
                  Id = a.Id,
                  IsActive = a.IsActive,
                  Latitude = a.Latitude,
                  Longitude = a.Longitude,
                  MustBeCompletedBy = a.MustBeCompletedBy,
                  Postcode = a.Postcode,
                  PreferredDoctorGenderType = new GenderType()
                  {
                    Name = a.PreferredDoctorGenderType.Name
                  },
                  Referral = new Referral()
                  {
                    Id = a.Referral.Id,
                    LeadAmhpUser = new User()
                    {
                      DisplayName = a.Referral.LeadAmhpUser.DisplayName
                    },
                    Patient = new Patient()
                    {
                      AlternativeIdentifier = a.Referral.Patient.AlternativeIdentifier,
                      Id = a.Referral.Patient.Id,
                      NhsNumber = a.Referral.Patient.NhsNumber
                    }
                  },
                  ScheduledTime = a.ScheduledTime,
                  Speciality = new Speciality()
                  {
                    Name = a.Speciality.Name
                  }
                })
                .SingleOrDefaultAsync(u => u.Id == id);

      return model;
    }

    public async Task<bool> RemoveDoctorsAsync(IAssessmentDoctorsRemove model)
    {
      Entities.Assessment entity = await _context
        .Assessments
        .Include(a => a.Referral)
        .Include(a => a.Doctors)
        .Where(a => a.Id == model.Id)
        .WhereIsActiveOrActiveOnly(true)
        .SingleOrDefaultAsync();

      if (entity == null)
      {
        throw new ModelStateException("id",
          $"An active Assessment with an id of {model.Id} could not be found.");
      }

      if (!model.UserIds.Any())
      {
        throw new ModelStateException("userIds",
          $"No user ids have been supplied to remove from assessment {model.Id}.");
      }

      if (entity.Referral.ReferralStatusId != ReferralStatus.AWAITING_RESPONSES &&
          entity.Referral.ReferralStatusId != ReferralStatus.RESPONSES_COMPLETE &&
          entity.Referral.ReferralStatusId != ReferralStatus.RESPONSES_PARTIAL &&
          entity.Referral.ReferralStatusId != ReferralStatus.SELECTING_DOCTORS)
      {
        throw new ModelStateException("id",
          $"An active Assessment with an id of {model.Id} cannot be scheduled because " +
          $"its Referral Status is {entity.Referral.ReferralStatusId} when it needs to be in [" +
          $"{ReferralStatus.AWAITING_RESPONSES}," +
          $"{ReferralStatus.RESPONSES_PARTIAL}," +
          $"{ReferralStatus.RESPONSES_COMPLETE}]" +
          $"{ReferralStatus.SELECTING_DOCTORS},."
        );
      }

      foreach (int userId in model.UserIds)
      {
        Entities.AssessmentDoctor assessmentDoctor =
          entity.Doctors.SingleOrDefault(d => d.DoctorUserId == userId);

        if (assessmentDoctor == null)
        {
          throw new ModelStateException("userIds",
            $"Assessment Id {model.Id} is not associated with the user id {userId} an therefore." +
            "the doctor cannot be removed.");
        }

        assessmentDoctor.HasAccepted = null;
        assessmentDoctor.RespondedAt = null;
        assessmentDoctor.StatusId = AssessmentDoctorStatus.REMOVED;
        UpdateModified(assessmentDoctor);

        AddUserAssessmentNotification(
          entity,
          assessmentDoctor.DoctorUserId,
          NotificationText.REMOVED_FROM_ASSESSMENT
        );
      }

      // check if referral status needs to be updated
      if (entity.Doctors.Where(d => d.StatusId != AssessmentDoctorStatus.REMOVED).Count() == 0) 
      {
        entity.Referral.ReferralStatusId = ReferralStatus.SELECTING_DOCTORS;
      }

      await _context.SaveChangesAsync();
      await SendUnsentNotifications(entity.UserAssessmentNotifications);

      return true;
    }

    public async Task<bool> Schedule(
      int id,
      DateTimeOffset scheduledTime
    )
    {
      Entities.Assessment entity = await _context
        .Assessments
        .Include(a => a.Referral)
        .Include(a => a.Doctors)
        .Where(a => a.Id == id)
        .WhereIsActiveOrActiveOnly(true)
        .SingleOrDefaultAsync();

      if (entity == null)
      {
        throw new ModelStateException("id",
          $"An active Assessment with an id of {id} could not be found.");
      }

      if (entity.Referral.ReferralStatusId != ReferralStatus.SELECTING_DOCTORS &&
          entity.Referral.ReferralStatusId != ReferralStatus.AWAITING_RESCHEDULING &&
          entity.Referral.ReferralStatusId != ReferralStatus.AWAITING_RESPONSES &&
          entity.Referral.ReferralStatusId != ReferralStatus.RESPONSES_PARTIAL &&
          entity.Referral.ReferralStatusId != ReferralStatus.RESPONSES_COMPLETE)
      {
        throw new ModelStateException("id",
          $"An active Assessment with an id of {id} cannot be scheduled because " +
          $"its Referral Status is {entity.Referral.ReferralStatusId} when it needs to be in [" +
          $"{ReferralStatus.SELECTING_DOCTORS}," +
          $"{ReferralStatus.AWAITING_RESCHEDULING}," +
          $"{ReferralStatus.AWAITING_RESPONSES}," +
          $"{ReferralStatus.RESPONSES_PARTIAL}," +
          $"{ReferralStatus.RESPONSES_COMPLETE}]."
        );
      }

      if (!entity.Doctors.Any(d => d.StatusId == AssessmentDoctorStatus.ALLOCATED))
      {
        throw new ModelStateException("id",
          $"An active Assessment with an id of {id} cannot be scheduled because it needs " +
           "to have at least one allocated doctor");
      }

      foreach (Entities.AssessmentDoctor assessmentDoctor in entity.Doctors)
      {
        if (assessmentDoctor.StatusId != AssessmentDoctorStatus.ALLOCATED)
        {
          AddUserAssessmentNotification(
            entity,
            assessmentDoctor.DoctorUserId,
            NotificationText.NOT_ALLOCATED_TO_ASSESSMENT
          );
          assessmentDoctor.StatusId = AssessmentDoctorStatus.NOT_ALLOCATED;
        }
        else
        {
            AddUserAssessmentNotification(
            entity,
            assessmentDoctor.DoctorUserId,
            NotificationText.ASSESSMENT_SCHEDULED
          );
        }
      }

      entity.ScheduledTime = scheduledTime;
      entity.Referral.ReferralStatusId = ReferralStatus.ASSESSMENT_SCHEDULED;
      UpdateModified(entity.Referral);
      await _context.SaveChangesAsync();
      await SendUnsentNotifications(entity.UserAssessmentNotifications);
      return true;
    }

    public async Task<AssessmentDoctor> UpdateAssessmentDoctorAcceptance(
      AssessmentDoctor model
    )
    {
      Entities.Assessment entity = await _context
        .Assessments
        .Include(a => a.Referral)
        .Include(a => a.Doctors)
        .Include(a => a.Doctors)
          .ThenInclude(d => d.DoctorUser)
        .Include(a => a.Doctors)
          .ThenInclude(d => d.Status)
        .Where(a => a.Id == model.AssessmentId)
        .WhereIsActiveOrActiveOnly(true)
        .AsNoTracking(false)
        .SingleOrDefaultAsync();

      if (entity == null)
      {
        throw new ModelStateException("assessmentId",
          $"An active Assessment with an id of {model.AssessmentId} could not be found.");
      }

      if (entity.Referral.ReferralStatusId != ReferralStatus.AWAITING_RESPONSES &&
          entity.Referral.ReferralStatusId != ReferralStatus.RESPONSES_PARTIAL &&
          entity.Referral.ReferralStatusId != ReferralStatus.RESPONSES_COMPLETE)
      {
        throw new ModelStateException("assessmentId",
          $"The Assessment with an id of {model.AssessmentId} cannot receive doctor acceptance " +
          $"updates because its Referral has a status of {entity.Referral.ReferralStatusId} " +
          $"when it needs to be one of [{ReferralStatus.AWAITING_RESPONSES}, " +
          $"{ReferralStatus.RESPONSES_PARTIAL},{ReferralStatus.RESPONSES_COMPLETE}]");
      }

      Entities.AssessmentDoctor doctor = entity
        .Doctors
        .Where(d => d.IsActive)
        .Where(d => d.DoctorUser.IsActive)
        .SingleOrDefault(d => d.DoctorUserId == model.DoctorUserId);

      if (doctor == null)
      {
        throw new ModelStateException("doctorId",
          $"The Assessment with an id of {model.AssessmentId} is not associated with " +
          $"an active Doctor with an id of {model.DoctorUserId}");
      }

      if (doctor.StatusId != AssessmentDoctorStatus.SELECTED &&
          doctor.StatusId != AssessmentDoctorStatus.ALLOCATED)
      {
        throw new ModelStateException("doctorId",
          $"The Doctor with an id of {model.DoctorUserId} associated with the Assessment with " +
          $"an id of {model.AssessmentId} has an invalid status of {doctor.Status.Name}");
      }

      doctor.HasAccepted = model.HasAccepted;
      doctor.RespondedAt = DateTimeOffset.Now;

      if (model.ContactDetailId.HasValue)
      {
        ContactDetail contactDetail = await _contactDetailsService.GetByIdAndUserIdAsync(
          model.ContactDetailId.Value, model.DoctorUserId, true, true);

        if (contactDetail == null)
        {
          throw new ModelStateException("contactDetailId",
            $"Unable to find the Contact Details with an id of {model.ContactDetailId} " +
            $"for a User with an id of {model.DoctorUserId}");
        }
        doctor.ContactDetailId = contactDetail.Id;
        doctor.Latitude = contactDetail.Latitude.HasValue ? contactDetail.Latitude.Value : 0;
        doctor.Longitude = contactDetail.Longitude.HasValue ? contactDetail.Longitude.Value : 0;
        doctor.Postcode = null;
      }
      else if (!string.IsNullOrWhiteSpace(model.Postcode))
      {
        Location postcode = await _locationDetailService.GetPostcodeDetailsAsync(model.Postcode);

        if (postcode == null)
        {
          throw new ModelStateException("postcode",
            $"Unable to find a valid postcode for {model.Postcode}");
        }
        doctor.ContactDetailId = null;
        doctor.Latitude = postcode.Latitude;
        doctor.Longitude = postcode.Longitude;
        doctor.Postcode = postcode.Postcode;
      }
      else if (model.Latitude.HasValue && model.Longitude.HasValue)
      {
        doctor.ContactDetailId = null;
        doctor.Latitude = model.Latitude.Value;
        doctor.Longitude = model.Longitude.Value;
        doctor.Postcode = null;
      }

      doctor.Distance = await _distanceCalculationService.CalculateRoadDistanceBetweenPoints(
        entity.Latitude,
        entity.Longitude,
        doctor.Latitude,
        doctor.Longitude
      );

      UpdateModified(doctor);

      if (entity.Referral.ReferralStatusId == ReferralStatus.AWAITING_RESPONSES)
      {
        if (entity.Doctors.All(d => d.HasAccepted.HasValue))
        {
          entity.Referral.ReferralStatusId = ReferralStatus.RESPONSES_COMPLETE;
        }
        else
        {
          entity.Referral.ReferralStatusId = ReferralStatus.RESPONSES_PARTIAL;
        }
      }
      UpdateModified(entity.Referral);

      //TODO - WHAT TO DO IF ALLOCATED DOCTOR DECLINES AFTER ACCEPTANCE???

      await _context.SaveChangesAsync();

      return model;
    }

    public async Task<AssessmentUpdate> UpdateAsync(
      AssessmentUpdate model
    )
    {

      Entities.Assessment entity = _context.Assessments
                                           .Include(a => a.Details)
                                           .Include(a => a.Doctors)
                                           .Where(a => a.Id == model.Id)
                                           .AsNoTracking(false)
                                           .WhereIsActiveOrActiveOnly(true)
                                           .SingleOrDefault();

      CheckAssessmentCanBeUpdated(entity);

      bool amhpChanged = CheckForChangeOfAmphpUser(model, entity);

      if (entity == null)
      {
        throw new ModelStateException("Id",
          $"An active Assessment with an id of {model.Id} could not be found.");
      }

      UpdateAssessmentDetails(model, entity);
      model.MapToEntity(entity);
      UpdateModified(entity);

      if (!amhpChanged)
      {
        AddUserAssessmentNotification(
          entity, model.AmhpUserId, NotificationText.ASSESSMENT_UPDATED);
      }

      foreach (Entities.AssessmentDoctor assessmentDoctor in entity.Doctors)
      {
        if (assessmentDoctor.StatusId != AssessmentDoctorStatus.REMOVED)
        {
          AddUserAssessmentNotification(
            entity,
            assessmentDoctor.DoctorUserId,
            NotificationText.ASSESSMENT_UPDATED);
        }
      }

      await _context.SaveChangesAsync();
      await SendUnsentNotifications(entity.UserAssessmentNotifications);

      model = _context.Assessments
                      .Include(e => e.Details)
                      .Where(e => e.Id == entity.Id)
                      .WhereIsActiveOrActiveOnly(true)
                      .AsNoTracking(true)
                      .Select(AssessmentUpdate.ProjectFromEntity)
                      .Single();
      return model;
    }

    public async Task<AssessmentOutcome> UpdateOutcomeAsync(
      AssessmentOutcome model
    )
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
      else if (entity.Referral.ReferralStatusId != ReferralStatus.ASSESSMENT_SCHEDULED)
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
        entity.Referral.ReferralStatusId = ReferralStatus.AWAITING_REVIEW;

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
              Attended = doctor.StatusId == AssessmentDoctorStatus.ATTENDED,
              Id = doctor.DoctorUserId
            })
            .ToList();
        }

        return model;
      }
    }

    private void AddAssessmentDetail(
       int assessmentDetailTypeId,
       Entities.Assessment entity
     )
    {
      Entities.AssessmentDetail assessmentDetail = new Entities.AssessmentDetail()
      {
        AssessmentDetailTypeId = assessmentDetailTypeId,
        IsActive = true
      };
      UpdateModified(assessmentDetail);
      entity.Details.Add(assessmentDetail);
    }

    private void AddAssessmentDetails(
      IList<int> detailTypeIds,
      Entities.Assessment entity
    )
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

    private bool CheckForChangeOfAmphpUser(AssessmentUpdate model, Entities.Assessment entity) {

      bool amhpChanged = model.AmhpUserId != entity.AmhpUserId;

      if (amhpChanged) {
        AddUserAssessmentNotification(
          entity, model.AmhpUserId, NotificationText.ALLOCATED_TO_ASSESSMENT);

        AddUserAssessmentNotification(
            entity, entity.AmhpUserId, NotificationText.REMOVED_FROM_ASSESSMENT);
      }

      return amhpChanged;
    } 

    private async Task<bool> AddDoctorAvailabilityToAssessmentDoctorIfAvailableAsync(
      Entities.AssessmentDoctor assessmentDoctor,
      Entities.Assessment entity
    )
    {
      IUserAvailability userAvailability = await _userAvailabilityService.GetAtAsync(
        assessmentDoctor.DoctorUserId,
        (entity.ScheduledTime ?? entity.MustBeCompletedBy).Value,
        true,
        true
      );

      if (userAvailability != null &&
          userAvailability.StatusId != UserAvailabilityStatus.UNAVAILABLE)
      {
        assessmentDoctor.Distance = await _distanceCalculationService.CalculateRoadDistanceBetweenPoints(
          entity.Latitude,
          entity.Longitude,
          userAvailability.Location.Latitude,
          userAvailability.Location.Longitude
        );
        assessmentDoctor.Latitude = userAvailability.Location.Latitude;
        assessmentDoctor.Longitude = userAvailability.Location.Longitude;
        return true;
      }

      return false;
    }

    private async Task<bool> AddDoctorBaseContactDetailToAssessmentDoctorAsync(
      Entities.AssessmentDoctor assessmentDoctor,
      Entities.Assessment assessment
    )
    {
      ContactDetail contactDetail = await
       _contactDetailsService.GetBaseContactDetailTypeForUserAsync(
         assessmentDoctor.DoctorUserId,
         true,
         true
      );

      assessmentDoctor.ContactDetailId = contactDetail.Id;
      assessmentDoctor.Distance = await _distanceCalculationService.CalculateRoadDistanceBetweenPoints(
        assessment.Latitude,
        assessment.Longitude,
        contactDetail.Latitude.HasValue ? contactDetail.Latitude.Value : 0,
        contactDetail.Longitude.HasValue ? contactDetail.Longitude.Value : 0
      );
      assessmentDoctor.Latitude = contactDetail.Latitude.HasValue ? contactDetail.Latitude.Value : 0;
      assessmentDoctor.Longitude = contactDetail.Longitude.HasValue ? contactDetail.Longitude.Value : 0;

      return true;
    }

    private async Task<bool> AddLatitudeAndLongitudeAsync(
      string postcode,
      Entities.Assessment entity
    )
    {
      Location postcodeModel = await
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

    private void CheckDoctorsAreSelected(
      Entities.Assessment entity,
      IEnumerable<int> doctorUserIds
    )
    {
      IEnumerable<int> selectedUserIds =
        entity.Doctors
              .Where(d => d.IsActive)
              .Where(d => d.StatusId == AssessmentDoctorStatus.SELECTED)
              .Select(ad => ad.DoctorUserId);

      if (doctorUserIds.Intersect(selectedUserIds).Count() != doctorUserIds.Count())
      {
        throw new ModelStateException("UserIds",
        "Only the following doctors id's are selected " +
        $"[{string.Join(",", selectedUserIds)}], " +
        $"from the requested [{string.Join(",", doctorUserIds)}]");
      }
    }

    private void CheckDoctorsAreSelectedAndHaveAccepted(
      Entities.Assessment entity,
      IEnumerable<int> doctorUserIds
    )
    {
      IEnumerable<int> selectedUserIds =
        entity.Doctors
              .Where(d => d.IsActive)
              .Where(d => d.StatusId == AssessmentDoctorStatus.SELECTED)
              .Where(d => d.HasAccepted ?? false)
              .Select(ad => ad.DoctorUserId);

      if (doctorUserIds.Intersect(selectedUserIds).Count() != doctorUserIds.Count())
      {
        throw new ModelStateException("UserIds",
        "Only the following doctors id's are selected and have accepted " +
        $"[{string.Join(",", selectedUserIds)}], " +
        $"from the requested [{string.Join(",", doctorUserIds)}]");
      }
    }

    private void CheckAssessmentCanBeUpdated(
      Entities.Assessment entity
    )
    {
      if (entity.CompletionConfirmationByUserId != null)
      {
        throw new ModelStateException("Id",
          $"The Assessment with an id of {entity.Id} cannot be updated because its completion " +
           "has been confirmed.");
      }
    }

    private void CheckAssessmentDoesNotAlreadyHaveAnOutcome(
      Entities.Assessment entity
    )
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

    private void CheckAssessmentHasCorrectReferralStatusToAddAllocatedDoctors(
      int id,
      int referralStatusId
    )
    {
      if (
        referralStatusId != ReferralStatus.AWAITING_RESPONSES &&
        referralStatusId != ReferralStatus.RESPONSES_PARTIAL &&
        referralStatusId != ReferralStatus.RESPONSES_COMPLETE)
      {
        throw new ModelStateException("Id",
          $"The Assessment with an id of {id} does not have one of the " +
          $"required referral statuses [" +
          $"{ReferralStatus.AWAITING_RESPONSES}," +
          $"{ReferralStatus.RESPONSES_PARTIAL}," +
          $"{ReferralStatus.RESPONSES_COMPLETE}] " +
          $"it has a referral status of [{referralStatusId}]");
      }
    }

    private void CheckAssessmentHasCorrectReferralStatusToAddAllocatedDoctorDirectly(
      int id,
      int referralStatusId
    )
    {
      if (
        referralStatusId != ReferralStatus.SELECTING_DOCTORS &&
        referralStatusId != ReferralStatus.AWAITING_RESPONSES &&
        referralStatusId != ReferralStatus.RESPONSES_PARTIAL &&
        referralStatusId != ReferralStatus.RESPONSES_COMPLETE)
      {
        throw new ModelStateException("Id",
          $"The Assessment with an id of {id} does not have one of the " +
          $"required referral statuses [" +
          $"{ReferralStatus.SELECTING_DOCTORS}," +
          $"{ReferralStatus.AWAITING_RESPONSES}," +
          $"{ReferralStatus.RESPONSES_PARTIAL}," +
          $"{ReferralStatus.RESPONSES_COMPLETE}] " +
          $"it has a referral status of [{referralStatusId}]");
      }
    }

    private void CheckAssessmentHasCorrectReferralStatusToAddSelectedDoctors(
      int id,
      int referralStatusId
    )
    {
      if (
        referralStatusId != ReferralStatus.SELECTING_DOCTORS &&
        referralStatusId != ReferralStatus.AWAITING_RESPONSES &&
        referralStatusId != ReferralStatus.RESPONSES_PARTIAL &&
        referralStatusId != ReferralStatus.RESPONSES_COMPLETE)
      {
        throw new ModelStateException("Id",
          $"The Assessment with an id of {id} does not have one of the " +
          $"required referral statuses [{ReferralStatus.SELECTING_DOCTORS}," +
          $"{ReferralStatus.AWAITING_RESPONSES}," +
          $"{ReferralStatus.RESPONSES_PARTIAL}," +
          $"{ReferralStatus.RESPONSES_COMPLETE}] " +
          $"it has a referral status of [{referralStatusId}]");
      }
    }

    private void CheckSelectedDoctorsAreAvailable(
      Assessment assessment,
      IEnumerable<int> selectedUserIds
    )
    {
      IEnumerable<int> availableUserIds =
        assessment.AvailableDoctors.Select(ad => ad.UserId);

      if (selectedUserIds.Intersect(availableUserIds).Count() != selectedUserIds.Count())
      {
        throw new ModelStateException("UserIds",
        "Only the following doctors id are available " +
        $"[{string.Join(",", availableUserIds)}], " +
        $"from the requested [{string.Join(",", selectedUserIds)}]");
      }
    }

    private async Task<IEnumerable<UserAssessmentNotification>> SendUnsentNotifications
    (
      IList<Data.Entities.UserAssessmentNotification> notifications
    ) {  
       IEnumerable<UserAssessmentNotification> unsentNotifications =
        notifications.Where(uan => uan.SentAt == null)
        .Select(u => new UserAssessmentNotification(){
          Id = u.Id
        });

      return await _userNotificationService.SendAssessmentNotifications(unsentNotifications);
    }

    private void CheckSelectedDoctorsAreNotAlreadySelected(
      Assessment assessment,
      IEnumerable<int> userIds
    )
    {
      if (assessment.DoctorsSelected != null)
      {
        IEnumerable<int> alreadySelectedIds =
          assessment.DoctorsSelected.Select(user => user.DoctorUserId).Intersect(userIds);

        if (alreadySelectedIds.Count() != 0)
        {
          throw new ModelStateException("UserIds",
          "The following doctor user id's are already selected " +
          $"[{string.Join(",", alreadySelectedIds)}], " +
          $"from the requested [{string.Join(",", userIds)}]");
        }
      }
    }

    private async Task<bool> CheckReferralDoesNotAlreadyHaveACurrentAssessmentAsync(
      AssessmentCreate model
    )
    {
      bool hasCurrentAssessment = await _referralService.HasCurrentAssessment(model.ReferralId);
      if (hasCurrentAssessment)
      {
        throw new ModelStateException("ReferralId",
        $"The Referral with an id of {model.ReferralId} already has a current assessment.");
      }
      return true;
    }

    private async Task<Entities.Assessment> GetEntityByIdAsync(
       int entityId,
       bool asNoTracking,
       bool activeOnly
    )
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

    private async Task<IEnumerable<Assessment>> GetListByAmhpUserIdAsync(
      int amhpUserId,
      int? referralStatusId,
      bool asNoTracking,
      bool activeOnly
    )
    {
      IQueryable<Entities.Assessment> query = _context
        .Assessments
        .Include(a => a.Referral)
          .ThenInclude(r => r.Patient)
        .Where(a => a.AmhpUserId == amhpUserId)
        .WhereIsActiveOrActiveOnly(activeOnly)
        .AsNoTracking(asNoTracking);

      if (referralStatusId.HasValue)
      {
        query = query.Where(a => a.Referral.ReferralStatusId == referralStatusId);
      }

      IEnumerable<Assessment> models = await query
        .Select(a => new Assessment
        {
          Id = a.Id,
          MustBeCompletedBy = a.MustBeCompletedBy,
          Postcode = a.Postcode,
          Referral = new Referral
          {
            ReferralStatusId = a.Referral.ReferralStatusId,
            Patient = new Patient
            {
              AlternativeIdentifier = a.Referral.Patient.AlternativeIdentifier,
              NhsNumber = a.Referral.Patient.NhsNumber
            }
          },
          ScheduledTime = a.ScheduledTime
        })
        .ToListAsync();

      return models;
    }

    private async Task<IEnumerable<Assessment>> GetListByDoctorUserIdAsync(
      int doctorUserId,
      int? doctorStatusId,
      int? referralStatusId,
      bool asNoTracking,
      bool activeOnly
    )
    {

      IQueryable<Entities.Assessment> query = _context
        .Assessments
        .Include(a => a.Doctors)
          .ThenInclude(d => d.DoctorUser)
        .Include(a => a.Referral)
        .Where(a => a.Doctors.Any
          (d => d.DoctorUser.Id == doctorUserId && d.StatusId != AssessmentDoctorStatus.REMOVED))
        .WhereIsActiveOrActiveOnly(activeOnly)
        .AsNoTracking(asNoTracking);

      if (referralStatusId.HasValue)
      {
        query = query.Where(a => a.Referral.ReferralStatusId == referralStatusId);
      }
      if (doctorStatusId.HasValue)
      {
        query = query.Where(a => a.Doctors.Any(d => d.Status.Id == doctorStatusId));
      }

      IEnumerable<Assessment> models = await query
        .Select(a => new Assessment
        {
          Id = a.Id,
          Doctors = a.Doctors
                     .Where(d => d.DoctorUserId == doctorUserId)
                     .Select(d => new AssessmentDoctor
                     {
                       HasAccepted = d.HasAccepted,
                       StatusId = d.StatusId
                     })
                     .ToList(),
          MustBeCompletedBy = a.MustBeCompletedBy,
          Postcode = a.Postcode,
          Referral = new Referral
          {
            ReferralStatusId = a.Referral.ReferralStatusId,
            Patient = new Patient
            {
              AlternativeIdentifier = a.Referral.Patient.AlternativeIdentifier,
              NhsNumber = a.Referral.Patient.NhsNumber
            }
          },
          ScheduledTime = a.ScheduledTime
        })
        .ToListAsync();

      return models;
    }

    private async Task<Referral> GetReferral(int referralId)
    {
      Referral referral = await _referralService.GetAsync(referralId, true, false);
      if (referral == null)
      {
        throw new ModelStateException("ReferralId",
        $"Cannot find an active Referral with an id of {referralId}.");
      }
      return referral;
    }

    private void UpdateAssessmentDetails(
      AssessmentUpdate model, Entities.Assessment entity
    )
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

    private void UpdateDoctorStatuses(
      AssessmentOutcome model, Entities.Assessment entity
    )
    {
      int[] attendingDoctorIds = model.AttendingDoctors.Select(d => d.Id).ToArray();
      Entities.AssessmentDoctor[] allocatedDoctors = entity.Doctors
        .Where(d => d.StatusId == AssessmentDoctorStatus.ALLOCATED)
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
          ? AssessmentDoctorStatus.ATTENDED
          : AssessmentDoctorStatus.NOT_ATTENDED;
        UpdateModified(assessmentDoctor);
      }
    }
  }
}
