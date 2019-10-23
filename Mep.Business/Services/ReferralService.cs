using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mep.Business.Models;
using Entities = Mep.Data.Entities;
using Mep.Business.Extensions;

namespace Mep.Business.Services
{
  public class ReferralService
    : ServiceBase<Referral, Entities.Referral>, IModelService<Referral>
  {
    public ReferralService(ApplicationContext context, IMapper mapper)
      : base("Referral", context, mapper)
    {
    }

    public async Task<IEnumerable<Models.Referral>> GetAllAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.Referral> entities =
        await _context.Referrals
                .Include(r => r.CreatedByUser)
                .Include(r => r.Examinations)                  
                  .ThenInclude(e => e.UserExaminationNotifications)
                    .ThenInclude(u => u.User)
                      .ThenInclude(u => u.ProfileType)
                .Include(r => r.Examinations)
                  .ThenInclude(e => e.PreferredDoctorGenderType)
                .Include(r => r.Examinations)
                  .ThenInclude(e => e.Speciality)                  
                .Include(r => r.Examinations)
                  .ThenInclude(e => e.UnsuccessfulExaminationType)
                .Include(r => r.Patient)
                .Include(r => r.ReferralStatus)
                .Include(r => r.LeadAmhpUser)
                .WhereIsActiveOrActiveOnly(activeOnly)
                .ToListAsync();

      IEnumerable<Models.Referral> models =
        _mapper.Map<IEnumerable<Models.Referral>>(entities);

      return models;
    }

    protected override async Task<Entities.Referral> GetEntityByIdAsync(
      int entityId,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.Referral entity = await
        _context.Referrals
                .Include(r => r.CreatedByUser)
                .Include(r => r.Examinations)                  
                  .ThenInclude(e => e.UserExaminationNotifications)
                    .ThenInclude(u => u.User)
                      .ThenInclude(u => u.ProfileType)
                .Include(r => r.Examinations)
                  .ThenInclude(e => e.Details)
                    .ThenInclude(d => d.ExaminationDetailType)
                .Include(r => r.Examinations)
                  .ThenInclude(e => e.PreferredDoctorGenderType)
                .Include(r => r.Examinations)
                  .ThenInclude(e => e.Speciality)                  
                .Include(r => r.Examinations)
                  .ThenInclude(e => e.UnsuccessfulExaminationType)
                .Include(r => r.Patient)
                  .ThenInclude(p => p.Ccg)
                .Include(r => r.Patient)
                  .ThenInclude(p => p.GpPractice)
                .Include(r => r.ReferralStatus)
                .Include(r => r.LeadAmhpUser)
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(referral => referral.Id == entityId);

      return entity;
    }

    protected override async Task<Entities.Referral> GetEntityWithNoIncludesByIdAsync(
      int entityId,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.Referral entity = await
        _context.Referrals
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(referral => referral.Id == entityId);

      return entity;
    }    
  }
}
