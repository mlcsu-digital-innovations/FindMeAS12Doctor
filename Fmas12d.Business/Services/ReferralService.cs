using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Extensions;
using Fmas12d.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public class ReferralService : ServiceBaseNoAutoMapper<Entities.Referral>, IReferralService
  {
    private readonly IUserService _userService;
    public ReferralService(ApplicationContext context, IUserService userService)
      : base(context)
    {
      this._userService = userService;
    }

    public async Task<Referral> CreateAsync(ReferralCreate model)
    {
      await _userService.CheckUserIsAnAmhpById(model.LeadAmhpUserId);

      Entities.Referral entity = model.MapToEntity();

      entity.Id = 0;
      entity.IsActive = true;
      entity.ReferralStatusId = ReferralStatus.NEW;

      UpdateModified(entity);

      _context.Add(entity);

      await _context.SaveChangesAsync();

      Referral createdModel = _context.Referrals
                      .Where(e => e.Id == entity.Id)
                      .WhereIsActiveOrActiveOnly(true)
                      .AsNoTracking(true)
                      .Select(Referral.ProjectFromEntity)
                      .Single();
      return createdModel;
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
      Models.Referral model =
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
      IEnumerable<Models.Referral> models =
        await _context.Referrals
                      .Include(r => r.Examinations)
                        .ThenInclude(e => e.Speciality)
                      .Include(r => r.Examinations)
                        .ThenInclude(e => e.UserExaminationNotifications)
                      .Include(r => r.Examinations)
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
                      .Include(r => r.Examinations)
                        .ThenInclude(e => e.AmhpUser)
                      .Include(r => r.Examinations)
                        .ThenInclude(e => e.Details)
                          .ThenInclude(d => d.ExaminationDetailType)
                      .Include(r => r.Examinations)
                        .ThenInclude(e => e.Doctors)
                          .ThenInclude(d => d.DoctorUser)
                      .Include(r => r.Examinations)
                        .ThenInclude(e => e.PreferredDoctorGenderType)
                      .Include(r => r.Examinations)
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
  }
}
