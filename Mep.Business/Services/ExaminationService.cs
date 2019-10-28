using AutoMapper;
using Entities = Mep.Data.Entities;
using Mep.Business.Exceptions;
using Mep.Business.Extensions;
using Mep.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Mep.Business.Services
{
  public class ExaminationService
    : ServiceBase<Examination, Entities.Examination>, IModelService<Examination>
  {
    private readonly ReferralService _referralService;
    private readonly UserService _userService;

    public ExaminationService(
      ApplicationContext context,
      IMapper mapper,
      IModelService<Referral> referralService,
      IModelService<User> userService)
      : base("Examination", context, mapper)
    {
      _referralService = referralService as ReferralService;
      _userService = userService as UserService;
    }

    private async Task<bool> AddAmhpToUserExaminationNotifications(
      int amhpUserId, Entities.Examination entity)
    {
      await CheckUserIdIsAnAmhp(amhpUserId);

      if (entity.UserExaminationNotifications == null)
      {
        entity.UserExaminationNotifications = new List<Entities.UserExaminationNotification>();
      }

      Entities.UserExaminationNotification userExaminationNotification =
        new Entities.UserExaminationNotification
        {
          NotificationTextId = NotificationText.SELECTED_FOR_EXAMINATION,
          UserId = amhpUserId
        };
      UpdateModified(userExaminationNotification);
      entity.UserExaminationNotifications.Add(userExaminationNotification);

      return true;
    }

    private void AddExaminationDetail(int examinationDetailTypeId, Entities.Examination entity)
    {
      Entities.ExaminationDetail examinationDetail = new Entities.ExaminationDetail()
      {
        ExaminationDetailTypeId = examinationDetailTypeId,
        IsActive = true
      };
      UpdateModified(examinationDetail);
      entity.Details.Add(examinationDetail);
    }

    private void AddExaminationDetails(IList<int> detailTypeIds, Entities.Examination entity)
    {
      if (detailTypeIds != null && detailTypeIds.Any())
      {
        if (entity.Details == null)
        {
          entity.Details = new List<Entities.ExaminationDetail>();
        }
        foreach (int examinationDetailTypeId in detailTypeIds)
        {
          AddExaminationDetail(examinationDetailTypeId, entity);
        }
      }
    }

    private void CheckExaminationDoesNotAlreadyHaveAnOutcome(Entities.Examination entity)
    {
      if (entity.IsSuccessful.HasValue ||
          entity.CompletedTime.HasValue)
      {
        throw new ExaminationAlreadyHasOutcomeException(
          entity.Id,
          entity.IsSuccessful,
          entity.CompletedTime,
          entity?.CompletedByUser?.DisplayName
        );
      }
    }

    private async Task<bool> CheckUserIdIsAnAmhp(int amhpUserId)
    {
      User user = await _userService.GetByIdAsync(amhpUserId, true);
      if (user == null)
      {
        throw new ModelStateException(
          "AmhpUserId", $"An active User with an Id of {amhpUserId} does not exist.");
      }
      if (!user.IsAmhp)
      {
        throw new ModelStateException(
          "AmhpUserId", 
          $"The User with an Id of {amhpUserId} must be an AMHP but is a {user.ProfileType.Name}.");
      }
      return true;
    }

    /// <summary>
    /// Sets the examination entity's properties from the provided business model
    /// The CCG id is set from the referral patient's ccg, it's duplicated on the examination so 
    /// that the patient's CCG is fixed at the time of the examination so if they change their CCG
    /// after the examination has taken place to won't change the CCG of the claim 
    /// </summary>
    public virtual async Task<ExaminationCreate> CreateAsync(ExaminationCreate model)
    {
      Entities.Examination entity = model.MapToEntity();

      entity.Id = 0;
      entity.IsActive = true;

      UpdateModified(entity);      
      entity.CcgId = await _referralService.GetCcgIdFromReferralPatient(model.ReferralId);
      entity.CreatedByUserId = entity.ModifiedByUserId;
      AddExaminationDetails(model.DetailTypeIds, entity);
      await AddAmhpToUserExaminationNotifications(model.AmhpUserId, entity);
      _context.Add(entity);

      await _context.SaveChangesAsync();

      model = _context.Examinations
                      .Include(e => e.Details)
                      .Where(e => e.Id == entity.Id)                      
                      .WhereIsActiveOrActiveOnly(true)
                      .AsNoTracking(true)
                      .Select(ExaminationCreate.ProjectFromEntity)
                      .Single();
      return model;
    }    

    public async Task<IEnumerable<Models.Examination>> GetAllAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.Examination> entities =
        await _context.Examinations

          .Include(e => e.AmhpUser)
          .Include(e => e.CompletedByUser)
          .Include(e => e.CreatedByUser)
          .Include(e => e.Details)
            .ThenInclude(d => d.ExaminationDetailType)
          .Include(e => e.Doctors)
            .ThenInclude(d => d.DoctorUser)
          .Include(e => e.UserExaminationNotifications)
            .ThenInclude(u => u.User)
          .WhereIsActiveOrActiveOnly(activeOnly)
          .ToListAsync();

      IEnumerable<Models.Examination> models =
        _mapper.Map<IEnumerable<Models.Examination>>(entities);

      return models;
    }

    public async Task<IEnumerable<Models.Examination>> GetAllFilterByAmhpUserIdAsync(
      int amhpUserId,
      bool asNoTracking,
      bool activeOnly)
    {

      List<Entities.Examination> entities =
        await _context.Examinations
                      .Where(e => e.AmhpUserId == amhpUserId)
                      .WhereIsActiveOrActiveOnly(activeOnly)
                      .AsNoTracking(asNoTracking)
                      .ToListAsync();

      IEnumerable<Models.Examination> models = 
        entities.Select(e => new Examination(e)).ToList();

      return models;
    }

    protected override async Task<Entities.Examination> GetEntityByIdAsync(
       int entityId,
       bool asNoTracking,
       bool activeOnly)
    {
      Entities.Examination entity = await
        _context.Examinations
                .Include(e => e.AmhpUser)
                .Include(e => e.CompletedByUser)
                .Include(e => e.CreatedByUser)
                .Include(e => e.Details)
                  .ThenInclude(d => d.ExaminationDetailType)
                .Include(e => e.Doctors)
                  .ThenInclude(d => d.DoctorUser)
                .Include(e => e.Referral)
                  .ThenInclude(r => r.Patient)
                .Include(e => e.Speciality)
                .Include(e => e.UserExaminationNotifications)
                  .ThenInclude(u => u.User)
                    .ThenInclude(u => u.ProfileType)
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(u => u.Id == entityId);

      return entity;
    }

    public override async Task<Examination> GetByIdAsync(
      int id,
      bool activeOnly)
    {
      Entities.Examination entity = await
        _context.Examinations
                .Include(e => e.AmhpUser)
                .Include(e => e.CompletedByUser)
                .Include(e => e.CreatedByUser)
                .Include(e => e.Details)
                  .ThenInclude(d => d.ExaminationDetailType)
                .Include(e => e.Doctors)
                  .ThenInclude(d => d.DoctorUser)
                .Include(e => e.Referral)
                  .ThenInclude(r => r.Patient)
                .Include(e => e.UserExaminationNotifications)
                  .ThenInclude(u => u.User)
                    .ThenInclude(u => u.ProfileType)
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(true)
                .SingleOrDefaultAsync(u => u.Id == id);

      Models.Examination model = new Examination(entity);

      return model;
    }


    protected override async Task<Entities.Examination> GetEntityWithNoIncludesByIdAsync(
      int entityId,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.Examination entity = await
        _context.Examinations
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(u => u.Id == entityId);

      return entity;
    }

    protected override async Task<bool> InternalUpdateAsync(
      Examination model, Entities.Examination entity)
    {
      Serilog.Log.Verbose("Examination Update model: {@model}", model);
      Serilog.Log.Verbose("Examination Update entity: {@entity}", entity);

      entity.Address1 = model.Address1;
      entity.Address2 = model.Address2;
      entity.Address3 = model.Address3;
      entity.Address4 = model.Address4;
      entity.CcgId = await _referralService.GetCcgIdFromReferralPatient(model.ReferralId);
      entity.CompletedByUserId = model.CompletedByUserId;
      entity.CompletedTime = model.CompletedTime;
      entity.CompletionConfirmationByUserId = model.CompletionConfirmationByUserId;
      entity.IsSuccessful = model.IsSuccessful;
      entity.MeetingArrangementComment = model.MeetingArrangementComment;
      entity.MustBeCompletedBy = model.MustBeCompletedBy;
      entity.NonPaymentLocationId = model.NonPaymentLocationId;
      entity.Postcode = model.Postcode;
      entity.PreferredDoctorGenderTypeId = model.PreferredDoctorGenderTypeId;
      entity.ReferralId = model.ReferralId;
      entity.ScheduledTime = model.ScheduledTime;
      entity.SpecialityId = model.SpecialityId;
      entity.UnsuccessfulExaminationTypeId = model.UnsuccessfulExaminationTypeId;
      UpdateExaminationDetails(model, entity);
      await UpdateAmhpToUserExaminationNotifications(model, entity);

      return true;
    }

    private async Task<bool> UpdateAmhpToUserExaminationNotifications(
      Examination model, Entities.Examination entity)
    {
      await CheckUserIdIsAnAmhp(model.AmhpUserId);

      if (entity.HasUserExaminationNotifications)
      {
        Entities.UserExaminationNotification amhpUserExaminationNotification =
          entity.UserExaminationNotifications.SingleOrDefault(
            u => u.User.ProfileTypeId == ProfileType.AMHP);

        if (amhpUserExaminationNotification == null)
        {
          throw new EntityNotLoadedException(
            "Examination",
            model.Id,
            "UserExaminationNotification.User.ProfileType of AMHP",
            ProfileType.AMHP);
        }
        else
        {
          UpdateModified(amhpUserExaminationNotification);
          amhpUserExaminationNotification.IsActive = true;
        }
      }
      else
      {
        throw new EntityNotLoadedException(
          "Examination",
          model.Id,
          "UserExaminationNotification.User.ProfileType of AMHP",
          ProfileType.AMHP);
      }
      return true;
    }

    /// <summary>
    /// Update the doctor statuses checking that the doctors are those expected
    /// </summary>
    private void UpdateDoctorStatuses(ExaminationOutcome model, Entities.Examination entity)
    {
      int[] attendingDoctorIds = model.AttendingDoctors.Select(d => d.Id).ToArray();
      Entities.ExaminationDoctor[] allocatedDoctors = entity.Doctors
        .Where(d => d.StatusId == ExaminationDoctorStatus.ALLOCATED)
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

      foreach (Entities.ExaminationDoctor examinationDoctor in allocatedDoctors)
      {
        ExaminationOutcomeDoctor examinationOutcomeDoctor =
          model.AttendingDoctors.Single(d => d.Id == examinationDoctor.DoctorUserId);

        examinationDoctor.AttendanceConfirmedByUserId = entity.ModifiedByUserId;
        examinationDoctor.StatusId = ExaminationDoctorStatus.ATTENDED;
        UpdateModified(examinationDoctor);
      }
    }

    private void UpdateExaminationDetails(Examination model, Entities.Examination entity)
    {
      if (entity.HasDetails)
      {
        foreach (Entities.ExaminationDetail examinationDetail in entity.Details)
        {
          UpdateModified(examinationDetail);
          examinationDetail.IsActive = false;
        }
      }

      if (model.HasDetailTypeIds)
      {
        if (entity.HasDetails)
        {
          foreach (int detailTypeId in model.DetailTypeIds)
          {
            Entities.ExaminationDetail examinationDetail =
              entity.Details.SingleOrDefault(d => d.ExaminationDetailTypeId == detailTypeId);
            if (examinationDetail == null)
            {
              AddExaminationDetail(detailTypeId, entity);
            }
            else
            {
              examinationDetail.IsActive = true;
            }
          }
        }
        else
        {
          AddExaminationDetails(model.DetailTypeIds, entity);
        }
      }
    }
    public async Task<ExaminationOutcome> UpdateOutcomeAsync(ExaminationOutcome model)
    {
      if (!model.IsSuccessful && !model.UnsuccessfulExaminationTypeId.HasValue)
      {
        throw new ModelStateException("UnsuccessfulExaminationTypeId",
          "The field UnsuccessfulExaminationTypeId must have a value when the field " +
          "IsSuccessful is true.");
      }

      Entities.Examination entity = await GetEntityByIdAsync(model.Id, false, true);

      Serilog.Log.Verbose("Examination Update model: {@model}", model);
      Serilog.Log.Verbose("Examination Update entity: {@entity}", entity);

      if (entity == null)
      {
        throw new EntityNotFoundException(_typeName, model.Id);
      }
      else
      {

        CheckExaminationDoesNotAlreadyHaveAnOutcome(entity);

        UpdateModified(entity);

        UpdateDoctorStatuses(model, entity);

        entity.CompletedByUserId = entity.ModifiedByUserId;
        entity.CompletedTime = model.CompletedTime;
        entity.IsSuccessful = model.IsSuccessful;
        entity.UnsuccessfulExaminationTypeId = model.UnsuccessfulExaminationTypeId;

        await _context.SaveChangesAsync();

        entity = await GetEntityByIdAsync(model.Id, false, false);

        model.CompletedTime = entity.CompletedTime ?? default;
        model.IsSuccessful = entity.IsSuccessful ?? default;
        model.UnsuccessfulExaminationTypeId = entity.UnsuccessfulExaminationTypeId ?? default;

        if (entity.Doctors.Any())
        {
          model.AttendingDoctors = entity.Doctors
            .Select(doctor => new ExaminationOutcomeDoctor
            {
              Attended = doctor.StatusId == ExaminationDoctorStatus.ATTENDED,
              Id = doctor.DoctorUserId
            })
            .ToList();
        }

        Serilog.Log.Verbose("Examination Updated entity: {@entity}", entity);
        Serilog.Log.Verbose("Examination Updated model: {@model}", model);

        return model;
      }
    }
  }
}
