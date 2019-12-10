using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Extensions;
using Fmas12d.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Fmas12d.Business.Exceptions;

namespace Fmas12d.Business.Services
{
  public class ReferralService :
    ServiceBase<Entities.Referral>,
    IReferralService
  {
    private readonly IPatientService _patientService;
    private readonly IUserService _userService;
    public ReferralService(
      ApplicationContext context,
      IPatientService patientService,
      IUserClaimsService userClaimsService,
      IUserService userService
    )
      : base(context, userClaimsService)
    {
      _patientService = patientService;
      _userService = userService;
    }

    public async Task<bool> CloseAsync(int id)
    {
      Entities.Referral entity = await GetForCloseAsync(id);

      if (entity.ReferralStatusId != ReferralStatus.AWAITING_REVIEW &&
          entity.ReferralStatusId != ReferralStatus.OPEN)
      {
        throw new ModelStateException(
          "id",
          $"The Referral Id {id} cannot be closed because it has a Status Id of " +
          $"{entity.ReferralStatusId} and requires a Status Id of [" +
          $"{ReferralStatus.AWAITING_REVIEW}, {ReferralStatus.OPEN}]."
        );
      }

      entity.ReferralStatusId = ReferralStatus.CLOSED;
      await _context.SaveChangesAsync();

      return true;
    }

    public async Task<bool> CloseForceAsync(int id)
    {
      Entities.Referral entity = await GetForCloseAsync(id);

      foreach (Entities.Assessment assessment in entity.Assessments
                                                       .Where(a => a.IsSuccessful == null)
                                                       .Where(a => a.IsActive))
      {
        AddUserAssessmentNotification(
          assessment,
          assessment.AmhpUserId,
          NotificationText.ASSESSMENT_CANCELLED
        );

        foreach (Entities.AssessmentDoctor assessmentDoctor in assessment.Doctors
                                                                         .Where(a => a.IsActive))
        {
          AddUserAssessmentNotification(
            assessment,
            assessmentDoctor.DoctorUserId,
            NotificationText.ASSESSMENT_CANCELLED
          );
        }
      }

      entity.ReferralStatusId = ReferralStatus.CLOSED;
      await _context.SaveChangesAsync();

      return true;
    }

    public async Task<Referral> CreateAsync(ReferralCreate model)
    {
      model.CreatedAt = DateTimeOffset.Now;
      return await CreateRetrospectiveAsync(model);
    }

    public async Task<Referral> CreateRetrospectiveAsync(ReferralCreate model)
    {
      await _userService.CheckIsAmhpAsync(model.LeadAmhpUserId, "leadAmhpUserId");
      await _patientService.CheckExists(model.PatientId, "patientId");

      if (model.CreatedAt == default)
      {
        throw new ModelStateException("createdAt",
        $"The createdAt field has an invalid value of {model.CreatedAt}");
      }

      Entities.Referral entity = model.MapToEntity();

      entity.Id = 0;
      entity.IsActive = true;
      entity.ReferralStatusId = ReferralStatus.NEW;

      UpdateModified(entity);
      entity.CreatedByUserId = entity.ModifiedByUserId;

      _context.Add(entity);

      await _context.SaveChangesAsync();

      return await GetAsync(entity.Id);
    }

    public async Task<bool> Exists(int id, bool activeOnly = true)
    {
      return await _context.Referrals
                           .Where(r => r.Id == id)
                           .WhereIsActiveOrActiveOnly(activeOnly)
                           .AnyAsync();
    }

    public async Task<Referral> GetAsync(
      int id,
      bool activeOnly = true,
      bool asNoTracking = true
    )
    {
      Referral referral = await _context.Referrals
                                        .Where(r => r.Id == id)
                                        .WhereIsActiveOrActiveOnly(activeOnly)
                                        .AsNoTracking(asNoTracking)
                                        .Select(Referral.ProjectFromEntity)
                                        .SingleOrDefaultAsync();
      return referral;
    }

    public async Task<int?> GetCcgIdFromReferralPatient(int id)
    {
      int? ccgId = await _context.Referrals
                                 .Include(r => r.Patient)
                                 .Where(r => r.Id == id)
                                 .WhereIsActiveOrActiveOnly(true)
                                 .AsNoTracking(true)
                                 .Select(r => r.Patient.CcgId)
                                 .SingleOrDefaultAsync();

      return ccgId;
    }

    public async Task<Referral> GetEditByIdAsync(
      int id, bool activeOnly = true, bool asNoTracking = true)
    {
      Referral model =
        await _context.Referrals
                      .Include(r => r.LeadAmhpUser)
                      .Include(r => r.Patient)
                        .ThenInclude(p => p.Ccg)
                      .Include(r => r.Patient)
                        .ThenInclude(p => p.GpPractice)
                      .Include(r => r.ReferralStatus)
                      .Where(r => r.Id == id)
                      .WhereIsActiveOrActiveOnly(activeOnly)
                      .AsNoTracking(asNoTracking)
                      .Select(r => new Referral(r, false))
                      .SingleOrDefaultAsync();

      return model;
    }

    public async Task<IEnumerable<Referral>> GetListAsync(
      bool activeOnly = true,
      bool asNoTracking = true
    )
    {
      return await GetListAsync(null, null, activeOnly, asNoTracking);
    }

    public async Task<IEnumerable<Referral>> GetListAsync(
      List<int> excludeStatusIds,
      List<int> includeStatusIds,
      bool activeOnly = true,
      bool asNoTracking = true
    )
    {

      IQueryable<Entities.Referral> query =
        _context.Referrals
                .Include(r => r.Assessments)
                  .ThenInclude(e => e.Speciality)
                .Include(r => r.Assessments)
                  .ThenInclude(e => e.UserAssessmentNotifications)
                .Include(r => r.Assessments)
                  .ThenInclude(e => e.Doctors)
                .Include(r => r.Patient)
                .Include(r => r.ReferralStatus)
                .Include(r => r.LeadAmhpUser)
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking);

      if (includeStatusIds != null && includeStatusIds.Any())
      {
        includeStatusIds.ForEach(includeStatusId =>
          query = query.Where(r => r.ReferralStatusId == includeStatusId)
        );
      }

      if (excludeStatusIds != null && excludeStatusIds.Any())
      {
        excludeStatusIds.ForEach(excludeStatusId =>
          query = query.Where(r => r.ReferralStatusId != excludeStatusId)
        );
      }

      IEnumerable<Referral> models =
        await query
              .Select(r => new Referral(r, false))
              .ToListAsync();

      return models;
    }

    public async Task<Referral> GetViewByIdAsync(
      int id, bool activeOnly = true, bool asNoTracking = true)
    {
      Models.Referral model =
        await _context.Referrals
                      .Include(r => r.Assessments)
                        .ThenInclude(e => e.AmhpUser)
                      .Include(r => r.Assessments)
                        .ThenInclude(e => e.Details)
                          .ThenInclude(d => d.AssessmentDetailType)
                      .Include(r => r.Assessments)
                        .ThenInclude(e => e.Doctors)
                          .ThenInclude(d => d.DoctorUser)
                      .Include(r => r.Assessments)
                        .ThenInclude(e => e.PreferredDoctorGenderType)
                      .Include(r => r.Assessments)
                        .ThenInclude(e => e.Speciality)
                      .Include(r => r.LeadAmhpUser)
                      .Include(r => r.Patient)
                      .Include(r => r.ReferralStatus)
                      .Where(r => r.Id == id)
                      .WhereIsActiveOrActiveOnly(activeOnly)
                      .AsNoTracking(asNoTracking)
                      .Select(r => new Referral(r, false))
                      .SingleOrDefaultAsync();

      return model;
    }

    public async Task<bool> HasCurrentAssessment(int id)
    {
      Models.Referral model =
        await _context.Referrals
                      .Include(r => r.Assessments)
                      .Where(r => r.Id == id)
                      .WhereIsActiveOrActiveOnly(true)
                      .AsNoTracking(true)
                      .Select(r => new Referral(r, true))
                      .SingleOrDefaultAsync();

      return model?.HasCurrentAssessment ?? false;
    }

    public async Task<Referral> UpdateAsync(ReferralUpdate model)
    {
      return await UpdateAsyncInternal(model, false);
    }

    public async Task<Referral> UpdateRetrospectiveAsync(ReferralUpdate model)
    {
      if (model.CreatedAt == default)
      {
        throw new ModelStateException("createdAt",
        $"The createdAt field has an invalid value of {model.CreatedAt}");
      }
      return await UpdateAsyncInternal(model, true);
    }

    private async Task<Entities.Referral> GetForCloseAsync(int id)
    {
      Entities.Referral entity = await _context
        .Referrals
        .Include(r => r.Assessments)
          .ThenInclude(a => a.Doctors)
        .Where(r => r.Id == id)
        .SingleOrDefaultAsync();

      if (entity == null)
      {
        throw new ModelStateException("id", $"Unable to find a referral with an id of {id}");
      }

      if (entity.ReferralStatusId == ReferralStatus.CLOSED)
      {
        throw new ModelStateException(
          "id", 
          $"Unable to close the referral with an id of {id} because it is already closed."
        );
      }      

      return entity;
    }

    private async Task<Referral> UpdateAsyncInternal(ReferralUpdate model, bool isRetrospective)
    {
      await _userService.CheckIsAmhpAsync(model.LeadAmhpUserId, "leadAmhpUserId");

      Entities.Referral entity = await _context
        .Referrals
        .Where(r => r.Id == model.Id)
        .WhereIsActiveOrActiveOnly(true)
        .SingleOrDefaultAsync();

      if (entity == null)
      {
        throw new ModelStateException("id",
        $"Unable to find an active referral with an id of {model.Id}");
      }

      entity.LeadAmhpUserId = model.LeadAmhpUserId;
      if (isRetrospective)
      {
        entity.CreatedAt = model.CreatedAt.Value;
      }
      UpdateModified(entity);

      await _context.SaveChangesAsync();

      return await GetAsync(model.Id);
    }


  }
}
