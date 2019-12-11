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

    public AssessmentService(
      ApplicationContext context,
      IContactDetailsService contactDetailsService,
      ILocationDetailService locationDetailService,
      IReferralService referralService,
      IUserService userService,
      IUserAvailabilityService userAvailabilityService,
      IUserClaimsService userClaimsService
    )
      : base(context, userClaimsService)
    {
      _contactDetailsService = contactDetailsService;
      _locationDetailService = locationDetailService;
      _referralService = referralService;
      _userService = userService;
      _userAvailabilityService = userAvailabilityService;
    }

    public async Task<IAssessmentDoctorsUpdate> AddAllocatedDoctorDirectAsync(
      int id,
      int userId
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

      Entities.AssessmentDoctor existingAssessmentDoctor =
        entity.Doctors.SingleOrDefault(d => d.DoctorUserId == userId);

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

      Entities.AssessmentDoctor assessmentDoctor = new Entities.AssessmentDoctor()
      {
        DoctorUserId = userId,
        IsActive = true,
        StatusId = Models.AssessmentDoctorStatus.ALLOCATED,
      };
      UpdateModified(assessmentDoctor);

      if (!await AddDoctorAvailabilityToAssessmentDoctorIfAvailableAsync(assessmentDoctor, entity))
      {
        await AddDoctorBaseContactDetailToAssessmentDoctorForAssessmentCcgAsync(
          assessmentDoctor, 
          entity
        );
      }

      entity.Doctors.Add(assessmentDoctor);

      AddUserAssessmentNotification(
        entity, userId, Models.NotificationText.ALLOCATED_TO_ASSESSMENT);


      await _context.SaveChangesAsync();

      return new AssessmentDoctorsUpdate()
      {
        Id = entity.Id,
        UserIds = entity.Doctors.Where(d => d.StatusId == Models.AssessmentDoctorStatus.ALLOCATED)
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
        updateModel.Id, entity.Referral.ReferralStatusId);
      CheckDoctorsAreSelected(entity, updateModel.UserIds);
      CheckDoctorsAreSelectedAndHaveAccepted(entity, updateModel.UserIds);

      UpdateModified(entity);

      foreach (int userId in updateModel.UserIds)
      {
        Entities.AssessmentDoctor assessmentDoctor =
          entity.Doctors.Single(d => d.DoctorUserId == userId);
        assessmentDoctor.StatusId = Models.AssessmentDoctorStatus.ALLOCATED;
        UpdateModified(assessmentDoctor);

        AddUserAssessmentNotification(
          entity, userId, Models.NotificationText.ALLOCATED_TO_ASSESSMENT);
      }

      await _context.SaveChangesAsync();

      return new AssessmentDoctorsUpdate()
      {
        Id = entity.Id,
        UserIds = entity.Doctors.Where(d => d.StatusId == Models.AssessmentDoctorStatus.ALLOCATED)
                                .Select(d => d.DoctorUserId)
                                .ToList()
      };
    }

    public async Task<IAssessmentDoctorsUpdate> AddSelectedDoctorsAsync(
      IAssessmentDoctorsUpdate updateModel
    )
    {
      Models.Assessment model = await GetAvailableDoctorsAsync(updateModel.Id, false, true);

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

        Entities.AssessmentDoctor assessmentDoctor = new Entities.AssessmentDoctor()
        {
          ContactDetailId = availabilityDoctor.Location.ContactDetailId,
          Distance = availabilityDoctor.Distance,
          DoctorUserId = userId,
          IsActive = true,
          Latitude = availabilityDoctor.Location.Latitude,
          Longitude = availabilityDoctor.Location.Longitude,
          Postcode = availabilityDoctor.Location.Postcode,
          StatusId = Models.AssessmentDoctorStatus.SELECTED,
        };
        UpdateModified(assessmentDoctor);
        entity.Doctors.Add(assessmentDoctor);

        AddUserAssessmentNotification(
          entity, userId, Models.NotificationText.SELECTED_FOR_ASSESSMENT);
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

      return new AssessmentDoctorsUpdate()
      {
        Id = entity.Id,
        UserIds = entity.Doctors.Where(d => d.StatusId == Models.AssessmentDoctorStatus.SELECTED)
                                .Select(d => d.DoctorUserId)
                                .ToList()
      };
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
        Models.NotificationText.ALLOCATED_TO_ASSESSMENT
      );
      await AddLatitudeAndLongitudeAsync(model.Postcode, entity);
      _context.Add(entity);

      Entities.Referral referral = _context.Referrals.Find(model.ReferralId);
      referral.ReferralStatusId = ReferralStatus.SELECTING_DOCTORS;

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
        Models.Assessment model = new Models.Assessment(entity);

        model.AvailableDoctors = await _userAvailabilityService.GetAvailableDoctorsAsync(
          model.DateTime, true, true);

        foreach (IUserAvailabilityDoctor availabilityDoctor in model.AvailableDoctors)
        {
          availabilityDoctor.Distance = Distance.CalculateDistanceAsCrowFlies(
            entity.Latitude,
            entity.Longitude,
            availabilityDoctor.Location.Latitude,
            availabilityDoctor.Location.Longitude
          );
        }

        return model;
      }
    }

    public async Task<IEnumerable<Models.Assessment>> GetListByUserIdAsync(
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

        Models.ProfileType.AMHP => await GetListByAmhpUserIdAsync(
          userId, referralStatusId, asNoTracking, activeOnly),

        Models.ProfileType.DOCTOR => await GetListByDoctorUserIdAsync(
          userId, doctorStatusId, referralStatusId, asNoTracking, activeOnly),

        _ => throw new ModelStateException("userId",
             "Assessments cannot be associated with a User that has a ProfileType of " +
              $"{userProfileTypeId}."),
      };
    }

    public async Task<Models.Assessment> GetByIdAsync(
      int id,
      bool activeOnly,
      bool asNoTracking
    )
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
                .Include(e => e.Doctors)
                  .ThenInclude(d => d.ContactDetail)
                .Include(e => e.Referral)
                  .ThenInclude(r => r.Patient)
                .Include(e => e.Speciality)
                .Include(e => e.UserAssessmentNotifications)
                  .ThenInclude(u => u.User)
                    .ThenInclude(u => u.ProfileType)
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(u => u.Id == id);

      Assessment model = new Assessment(entity);

      return model;
    }

    public async Task<Models.Assessment> GetByIdForUserAsync(
      int id,
      int userId,
      bool asNoTracking,
      bool activeOnly
    )
    {
      Models.Assessment model = await GetByIdAsync(id, activeOnly, asNoTracking);
      // TODO Refactor if too slow
      model.Doctors = model.Doctors.Where(d => d.DoctorUserId == userId).ToList();

      return model;
    }

    public async Task<Models.Assessment> GetSelectedDoctorsAsync(
      int id,
      bool asNoTracking,
      bool activeOnly
    )
    {
      Models.Assessment model =
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
                .Select(a => new Models.Assessment()
                {
                  AmhpUser = new Models.User()
                  {
                    DisplayName = a.AmhpUser.DisplayName
                  },
                  Doctors = a.Doctors.Select(d => new Models.AssessmentDoctor()
                  {
                    DoctorUser = new Models.User()
                    {
                      DisplayName = d.DoctorUser.DisplayName,
                      GenderType = new Models.GenderType()
                      {
                        Name = d.DoctorUser.GenderType.Name
                      },
                      GmcNumber = d.DoctorUser.GmcNumber,
                      Id = d.DoctorUserId,
                      IsActive = d.DoctorUser.IsActive,
                      ProfileType = new Models.ProfileType()
                      {
                        Name = d.DoctorUser.ProfileType.Name
                      },
                      UserSpecialities = d.DoctorUser
                                          .UserSpecialities
                                          .Select(us => new Models.UserSpeciality()
                                          {
                                            Speciality = new Models.Speciality()
                                            {
                                              Name = us.Speciality.Name
                                            }
                                          }).ToList()
                    },
                    DoctorUserId = d.DoctorUserId,
                    HasAccepted = d.HasAccepted,
                    IsActive = d.IsActive,
                    StatusId = d.StatusId
                  }).ToList(),
                  Id = a.Id,
                  IsActive = a.IsActive,
                  Latitude = a.Latitude,
                  Longitude = a.Longitude,
                  MustBeCompletedBy = a.MustBeCompletedBy,
                  Postcode = a.Postcode,
                  PreferredDoctorGenderType = new Models.GenderType()
                  {
                    Name = a.PreferredDoctorGenderType.Name
                  },
                  Referral = new Models.Referral()
                  {
                    Id = a.Referral.Id,
                    LeadAmhpUser = new Models.User()
                    {
                      DisplayName = a.Referral.LeadAmhpUser.DisplayName
                    },
                    Patient = new Models.Patient()
                    {
                      AlternativeIdentifier = a.Referral.Patient.AlternativeIdentifier,
                      Id = a.Referral.Patient.Id,
                      NhsNumber = a.Referral.Patient.NhsNumber
                    }
                  },
                  ScheduledTime = a.ScheduledTime,
                  Speciality = new Models.Speciality()
                  {
                    Name = a.Speciality.Name
                  }
                })
                .SingleOrDefaultAsync(u => u.Id == id);

      if (model == null)
      {
        return null;
      }
      else
      {
        Dictionary<int, Location> doctorPostcodes =
          await _userAvailabilityService.GetDoctorsPostcodeAtAsync(
            model.DoctorsSelected.Select(d => d.Id).ToList(),
            model.DateTime,
            true,
            true
          );

        foreach (Models.AssessmentDoctor assessmentDoctor in model.Doctors.Where(d => d.IsSelected))
        {
          Location doctorPostcode = doctorPostcodes.GetValueOrDefault(assessmentDoctor.DoctorUserId);
          if (doctorPostcode == null)
          {
            assessmentDoctor.Distance = null;
            assessmentDoctor.IsAvailable = false;
          }
          else
          {
            assessmentDoctor.Distance = Distance.CalculateDistanceAsCrowFlies(
              model.Latitude,
              model.Longitude,
              doctorPostcode.Latitude,
              doctorPostcode.Longitude
            );
            assessmentDoctor.IsAvailable = true;
          }
        }
        return model;
      }
    }

    public async Task<bool> Schedule(
      int id
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

      if (entity.Referral.ReferralStatusId != ReferralStatus.AWAITING_RESCHEDULING &&
          entity.Referral.ReferralStatusId != ReferralStatus.AWAITING_RESPONSES &&
          entity.Referral.ReferralStatusId != ReferralStatus.RESPONSES_PARTIAL &&
          entity.Referral.ReferralStatusId != ReferralStatus.RESPONSES_COMPLETE)
      {
        throw new ModelStateException("id",
          $"An active Assessment with an id of {id} cannot be scheduled because " +
          $"its Referral Status is {entity.Referral.ReferralStatusId} when it needs to be in [" +
          $"{ReferralStatus.AWAITING_RESCHEDULING}," +
          $"{ReferralStatus.AWAITING_RESPONSES}," +
          $"{ReferralStatus.RESPONSES_PARTIAL}," +
          $"{ReferralStatus.RESPONSES_COMPLETE}]."
        );
      }

      if (!entity.Doctors.Any(d => d.StatusId == Models.AssessmentDoctorStatus.ALLOCATED))
      {
        throw new ModelStateException("id",
          $"An active Assessment with an id of {id} cannot be scheduled because it needs " +
           "to have at least one allocated doctor");
      }

      foreach(Entities.AssessmentDoctor assessmentDoctor in entity.Doctors)
      {
        if (assessmentDoctor.StatusId != AssessmentDoctorStatus.ALLOCATED)
        {
          AddUserAssessmentNotification(
            entity,
            assessmentDoctor.DoctorUserId,
            NotificationText.NOT_ALLOCATED_TO_ASSESSMENT
          );
        }
      }

      entity.Referral.ReferralStatusId = ReferralStatus.ASSESSMENT_SCHEDULED;
      UpdateModified(entity.Referral);
      await _context.SaveChangesAsync();

      return true;
    }

    public async Task<Models.AssessmentDoctor> UpdateAssessmentDoctorAcceptance(
      Models.AssessmentDoctor model
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

      if (doctor.StatusId != Models.AssessmentDoctorStatus.SELECTED &&
          doctor.StatusId != Models.AssessmentDoctorStatus.ALLOCATED)
      {
        throw new ModelStateException("doctorId",
          $"The Doctor with an id of {model.DoctorUserId} associated with the Assessment with " +
          $"an id of {model.AssessmentId} has an invalid status of {doctor.Status.Name}");
      }

      doctor.HasAccepted = model.HasAccepted;
      doctor.RespondedAt = DateTimeOffset.Now;

      if (model.ContactDetailId.HasValue)
      {
        Models.ContactDetail contactDetail = await _contactDetailsService.GetByIdAndUserIdAsync(
          model.ContactDetailId.Value, model.DoctorUserId, true, true);

        if (contactDetail == null)
        {
          throw new ModelStateException("contactDetailId",
            $"Unable to find the Contact Details with an id of {model.ContactDetailId} " +
            $"for a User with an id of {model.DoctorUserId}");
        }
        doctor.ContactDetailId = contactDetail.Id;
        doctor.Latitude = contactDetail.Latitude;
        doctor.Longitude = contactDetail.Longitude;
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

      doctor.Distance = Distance.CalculateDistanceAsCrowFlies(
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

      if (entity == null)
      {
        throw new ModelStateException("Id",
          $"An active Assessment with an id of {model.Id} could not be found.");
      }

      UpdateAssessmentDetails(model, entity);
      model.MapToEntity(entity);
      UpdateModified(entity);

      AddUserAssessmentNotification(
        entity, model.AmhpUserId, Models.NotificationText.ASSESSMENT_UPDATED);

      foreach (Entities.AssessmentDoctor assessmentDoctor in entity.Doctors)
      {
        AddUserAssessmentNotification(
          entity,
          assessmentDoctor.DoctorUserId,
          Models.NotificationText.ASSESSMENT_UPDATED);
      }

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
              Attended = doctor.StatusId == Models.AssessmentDoctorStatus.ATTENDED,
              Id = doctor.DoctorUserId
            })
            .ToList();
        }

        return model;
      }
    }

    private async Task<IAssessmentDoctorsUpdate> AddAllocatedDoctorsInternalAsync(
      IAssessmentDoctorsUpdate updateModel,
      bool performDoctorsSelectedChecks
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
        updateModel.Id, entity.Referral.ReferralStatusId);

      if (performDoctorsSelectedChecks)
      {
        CheckDoctorsAreSelected(entity, updateModel.UserIds);
        CheckDoctorsAreSelectedAndHaveAccepted(entity, updateModel.UserIds);
      }

      UpdateModified(entity);

      foreach (int userId in updateModel.UserIds)
      {
        Entities.AssessmentDoctor assessmentDoctor =
          entity.Doctors.Single(d => d.DoctorUserId == userId);
        assessmentDoctor.StatusId = Models.AssessmentDoctorStatus.ALLOCATED;
        UpdateModified(assessmentDoctor);

        AddUserAssessmentNotification(
          entity, userId, Models.NotificationText.ALLOCATED_TO_ASSESSMENT);
      }

      await _context.SaveChangesAsync();

      return new AssessmentDoctorsUpdate()
      {
        Id = entity.Id,
        UserIds = entity.Doctors.Where(d => d.StatusId == Models.AssessmentDoctorStatus.ALLOCATED)
                                .Select(d => d.DoctorUserId)
                                .ToList()
      };
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

    private async Task<bool> AddDoctorAvailabilityToAssessmentDoctorIfAvailableAsync(
      Entities.AssessmentDoctor assessmentDoctor,
      Entities.Assessment entity
    )
    {
      IUserAvailability userAvailability = await _userAvailabilityService.GetAtAsync(
        assessmentDoctor.DoctorUserId,
        (entity.MustBeCompletedBy ?? entity.ScheduledTime).Value,
        true,
        true
      );
      
      if (userAvailability != null &&
          userAvailability.StatusId != UserAvailabilityStatus.UNAVAILABLE)
      {
        assessmentDoctor.Distance = Distance.CalculateDistanceAsCrowFlies(
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

    private async Task<bool> AddDoctorBaseContactDetailToAssessmentDoctorForAssessmentCcgAsync(
      Entities.AssessmentDoctor assessmentDoctor,
      Entities.Assessment assessment
    )
    {
      ContactDetail contactDetail = await 
       _contactDetailsService.GetBaseContactDetailTypeForCcgUserAsync(
         assessment.CcgId.Value,
         assessmentDoctor.DoctorUserId,
         true,
         true
      );

      assessmentDoctor.ContactDetailId = contactDetail.Id;
      assessmentDoctor.Distance = Distance.CalculateDistanceAsCrowFlies(
        assessment.Latitude,
        assessment.Longitude,
        contactDetail.Latitude,
        contactDetail.Longitude
      );
      assessmentDoctor.Latitude = contactDetail.Latitude;
      assessmentDoctor.Longitude = contactDetail.Longitude;        

      return true;
    }

    private async Task<bool> AddLatitudeAndLongitudeAsync(
      string postcode,
      Entities.Assessment entity
    )
    {
      Models.Location postcodeModel = await
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

    private void AddUserAssessmentNotification(
      Entities.Assessment entity,
      int userId,
      int notificationTextId)
    {

      if (entity.UserAssessmentNotifications == null)
      {
        entity.UserAssessmentNotifications = new List<Entities.UserAssessmentNotification>();
      }

      Entities.UserAssessmentNotification userAssessmentNotification =
        new Entities.UserAssessmentNotification
        {
          IsActive = true,
          NotificationTextId = notificationTextId,
          UserId = userId
        };

      UpdateModified(userAssessmentNotification);
      entity.UserAssessmentNotifications.Add(userAssessmentNotification);
    }

    private void CheckDoctorsAreSelected(
      Entities.Assessment entity,
      IEnumerable<int> doctorUserIds
    )
    {
      IEnumerable<int> selectedUserIds =
        entity.Doctors
              .Where(d => d.IsActive)
              .Where(d => d.StatusId == Models.AssessmentDoctorStatus.SELECTED)
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
              .Where(d => d.StatusId == Models.AssessmentDoctorStatus.SELECTED)
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
      Models.Assessment assessment,
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

    private void CheckSelectedDoctorsAreNotAlreadySelected(
      Models.Assessment assessment,
      IEnumerable<int> userIds
    )
    {
      if (assessment.DoctorsSelected != null)
      {
        IEnumerable<int> alreadySelectedIds =
          assessment.DoctorsSelected.Select(user => user.Id).Intersect(userIds);

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

    private async Task<IEnumerable<Models.Assessment>> GetListByAmhpUserIdAsync(
      int amhpUserId,
      int? referralStatusId,
      bool asNoTracking,
      bool activeOnly
    )
    {
      IQueryable<Entities.Assessment> query = _context
        .Assessments
        .Include(a => a.Referral)
        .Where(a => a.AmhpUserId == amhpUserId)
        .WhereIsActiveOrActiveOnly(activeOnly)
        .AsNoTracking(asNoTracking);

      if (referralStatusId.HasValue)
      {
        query = query.Where(a => a.Referral.ReferralStatusId == referralStatusId);
      }

      IEnumerable<Models.Assessment> models = await query
        .Select(a => new Models.Assessment
        {
          Id = a.Id,
          MustBeCompletedBy = a.MustBeCompletedBy,
          Postcode = a.Postcode,
          Referral = new Models.Referral
          {
            ReferralStatusId = a.Referral.ReferralStatusId
          },
          ScheduledTime = a.ScheduledTime
        })
        .ToListAsync();

      return models;
    }

    private async Task<IEnumerable<Models.Assessment>> GetListByDoctorUserIdAsync(
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
        .Where(a => a.Doctors.Any(d => d.DoctorUser.Id == doctorUserId))
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

      IEnumerable<Models.Assessment> models = await query
        .Select(a => new Models.Assessment
        {
          Id = a.Id,
          Doctors = a.Doctors
                     .Where(d => d.DoctorUserId == doctorUserId)
                     .Select(d => new Models.AssessmentDoctor
                     {
                       HasAccepted = d.HasAccepted,
                       StatusId = d.StatusId
                     })
                     .ToList(),
          MustBeCompletedBy = a.MustBeCompletedBy,
          Postcode = a.Postcode,
          Referral = new Models.Referral
          {
            ReferralStatusId = a.Referral.ReferralStatusId
          },
          ScheduledTime = a.ScheduledTime
        })
        .ToListAsync();

      return models;
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
  }
}
