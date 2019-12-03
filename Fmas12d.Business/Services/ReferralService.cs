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
    ServiceBaseNoAutoMapper<Entities.Referral>, 
    IReferralService
  {
    private readonly IPatientService _patientService;
    private readonly IUserService _userService;
    public ReferralService(
      ApplicationContext context, 
      IUserClaimsService userClaimsService,
      IPatientService patientService,
      IUserService userService
    )
      : base(context, userClaimsService)
    {
      _patientService = patientService;     
      _userService = userService;      
    }

    public async Task<Referral> CreateAsync(ReferralCreate model)
    {
      model.CreatedAt = DateTimeOffset.Now;
      return await CreateRetrospectiveAsync(model);
    }

    public async Task<Referral> CreateRetrospectiveAsync(ReferralCreate model)
    {
      await _userService.CheckIsAmhp(model.LeadAmhpUserId, "leadAmhpUserId");
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

    public async Task<Referral> GetAsync(int id, bool activeOnly = true, bool asNoTracking = true)
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
      bool activeOnly = true, bool asNoTracking = true)
    {
      IEnumerable<Referral> models =
        await _context.Referrals
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
                      .AsNoTracking(asNoTracking)
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

    private async Task<Referral> UpdateAsyncInternal(ReferralUpdate model, bool isRetrospective)
    {
      await _userService.CheckIsAmhp(model.LeadAmhpUserId, "leadAmhpUserId");

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

    public async Task<Referral> UpdateRetrospectiveAsync(ReferralUpdate model)
    {
      if (model.CreatedAt == default)
      {
        throw new ModelStateException("createdAt",
        $"The createdAt field has an invalid value of {model.CreatedAt}");
      }      
      return await UpdateAsyncInternal(model, true);
    }

  }
}
