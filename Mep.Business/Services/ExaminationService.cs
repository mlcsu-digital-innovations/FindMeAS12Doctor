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
    private readonly IModelService<Referral> _referralService;
    private readonly IModelService<User> _userService;

    public ExaminationService(
      ApplicationContext context,
      IMapper mapper,
      IModelService<Referral> referralService,
      IModelService<User> userService)
      : base("Examination", context, mapper)
    {
      _referralService = referralService;
      _userService = userService;
    }

    public async Task<IEnumerable<Models.Examination>> GetAllAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.Examination> entities =
        await _context.Examinations
                .Include(e => e.CreatedByUser)
                .Include(e => e.Details)
                  .ThenInclude(d => d.ExaminationDetailType)
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

      IEnumerable<Entities.Examination> entities =
        await _context.Examinations
                .Include(e => e.UserExaminationNotifications)
                  .ThenInclude(u => u.User)
                    .ThenInclude(u => u.ProfileType)
                .Where(e => e.UserExaminationNotifications.Any(u => u.UserId == amhpUserId))
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .ToListAsync();

      if (entities.Any())
      {
        Entities.ProfileType amhpProfileType =
          entities.SelectMany(e => e.UserExaminationNotifications)
                  .First(u => u.UserId == amhpUserId)
                  .User
                  .ProfileType;

        if (amhpProfileType.Id != Models.ProfileType.AMHP)
        {
          throw new ModelStateException("AmhpUserId",
            $"UserId {amhpUserId} must have a ProfileType " +
            $"of AMHP but is a {amhpProfileType.Name}.");
        }

      }

      IEnumerable<Models.Examination> models =
        _mapper.Map<IEnumerable<Models.Examination>>(entities);

      return models;
    }

    protected override async Task<Entities.Examination> GetEntityByIdAsync(
      int entityId,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.Examination entity = await
        _context.Examinations
                .Include(e => e.CreatedByUser)
                .Include(e => e.Details)
                  .ThenInclude(d => d.ExaminationDetailType)
                .Include(e => e.UserExaminationNotifications)
                  .ThenInclude(u => u.User)
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(u => u.Id == entityId);

      return entity;
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

    /// <summary>
    /// Sets the examination entity's properties from the provided business model
    /// The CCG id is set from the referral patient's ccg, it's duplicated on the examination so that
    /// the patient's CCG is fixed at the time of the examination so if they change their CCG
    /// after the examination has taken place to won't change the CCG of the claim 
    /// </summary>
    protected override async Task<bool> InternalCreateAsync(Examination model, Entities.Examination entity)
    {
      Serilog.Log.Verbose("Examination Create model: {@model}", model);
      Serilog.Log.Verbose("Examination Create entity: {@entity}", entity);

      entity.CcgId = await GetCcgIdFromReferralPatient(model);
      entity.CompletedByUserId = null;
      entity.CompletedTime = null;
      entity.CompletionConfirmationByUserId = null;
      entity.CreatedByUserId = entity.ModifiedByUserId;
      entity.IsSuccessful = null;
      entity.NonPaymentLocationId = null;
      entity.UnsuccessfulExaminationTypeId = null;
      AddExaminationDetails(model, entity);
      await AddAmhpToUserExaminationNotifications(model, entity);

      return true;
    }

    protected override async Task<bool> InternalUpdateAsync(Examination model, Entities.Examination entity)
    {
      Serilog.Log.Verbose("Examination Update model: {@model}", model);
      Serilog.Log.Verbose("Examination Update entity: {@entity}", entity);

      entity.Address1 = model.Address1;
      entity.Address2 = model.Address2;
      entity.Address3 = model.Address3;
      entity.Address4 = model.Address4;
      entity.CcgId = await GetCcgIdFromReferralPatient(model);
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

    private async Task<bool> AddAmhpToUserExaminationNotifications(Examination model, Entities.Examination entity)
    {
      await CheckUserIdIsAnAmhp(model.AmhpUserId);

      entity.UserExaminationNotifications = new List<Entities.UserExaminationNotification>(1);
      Entities.UserExaminationNotification userExaminationNotification =
        new Entities.UserExaminationNotification
        {
          NotificationTextId = NotificationText.ASSIGNED_TO_EXAMINATION,
          UserId = model.AmhpUserId
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

    private void AddExaminationDetails(Examination model, Entities.Examination entity)
    {
      if (model.HasDetailTypeIds)
      {
        entity.Details = new List<Entities.ExaminationDetail>(model.DetailTypeIds.Count);
        foreach (int examinationDetailTypeId in model.DetailTypeIds)
        {
          AddExaminationDetail(examinationDetailTypeId, entity);
        }
      }
    }

    private async Task<int?> GetCcgIdFromReferralPatient(Examination model)
    {
      Referral referral = await _referralService.GetByIdAsync(model.ReferralId, true);

      if (referral == null)
      {
        throw new ModelStateException(
          "ReferralId", $"An active ReferralId of {model.ReferralId} does not exist.");
      }

      if (referral.Patient == null)
      {
        throw new EntityNotLoadedException(
          "Referral", referral.Id, "Patient", referral.PatientId);
      }

      return referral.Patient.CcgId;
    }

    private async Task<bool> CheckUserIdIsAnAmhp(int amhpUserId)
    {
      User user = await _userService.GetByIdAsync(amhpUserId, true);
      if (user == null)
      {
        throw new ModelStateException(
          "AmhpUserId", $"An active UserId of {amhpUserId} does not exist.");
      }
      if (!user.IsAmhp)
      {
        throw new ModelStateException(
          "AmhpUserId", $"UserId {amhpUserId} must be an AMHP but is a {user.ProfileType.Name}.");
      }
      return true;
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
          AddExaminationDetails(model, entity);
        }
      }
    }

    private async Task<bool> UpdateAmhpToUserExaminationNotifications(Examination model, Entities.Examination entity)
    {
      await CheckUserIdIsAnAmhp(model.AmhpUserId);

      if (entity.HasUserExaminationNotifications)
      {
        Entities.UserExaminationNotification amhpUserExaminationNotification =
          entity.UserExaminationNotifications.SingleOrDefault(u => u.User.ProfileTypeId == ProfileType.AMHP);

        if (amhpUserExaminationNotification == null)
        {
          throw new EntityNotLoadedException(
            "Examination", model.Id, "UserExaminationNotification.User.ProfileType of AMHP", ProfileType.AMHP);
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
          "Examination", model.Id, "UserExaminationNotification.User.ProfileType of AMHP", ProfileType.AMHP);
      }
      return true;
    }
  }
}
