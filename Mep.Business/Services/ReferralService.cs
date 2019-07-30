using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mep.Business.Models;
using Entities = Mep.Data.Entities;
using Mep.Business.Extensions;
using System;

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
                .Include(r => r.Patient)
                .Include(r => r.ReferralStatus)
                .WhereIsActiveOrActiveOnly(activeOnly)
                .ToListAsync();

      IEnumerable<Models.Referral> models =
        _mapper.Map<IEnumerable<Models.Referral>>(entities);

      return models;
    }

    public async Task<IEnumerable<Models.Referral>> GetAllDetailsAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.Referral> entities =
        await _context.Referrals
                .Include(r => r.CreatedByUser)
                .Include(r => r.Examinations)
                .Include(r => r.Patient)
                .Include(r => r.ReferralStatus)
                .WhereIsActiveOrActiveOnly(activeOnly)
                .ToListAsync();

      IEnumerable<Models.Referral> models =
        _mapper.Map<IEnumerable<Models.Referral>>(entities);

      return models;
    }


    protected override async Task<Entities.Referral> GetEntityLinkedObjectsAsync(Referral model, Entities.Referral entity) {

        entity.Patient = await _context.Patients.SingleAsync(x => x.Id == model.PatientId);
        entity.CreatedByUser = await _context.Users.SingleAsync(x => x.Id == model.CreatedByUserId);
        entity.ReferralStatus = await _context.ReferralStatuses.SingleAsync(x => x.Id == model.ReferralStatusId);
                
        return entity;
    }


    protected override async Task<Entities.Referral> GetEntityByIdAsync(
      int id,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.Referral entity = await
        _context.Referrals
                .Include(r => r.CreatedByUser)
                .Include(r => r.Examinations)
                .Include(r => r.Patient)
                .Include(r => r.ReferralStatus)
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(u => u.Id == id);

      return entity;
    }

    protected override Task<bool> InternalCreateAsync(Referral model, Entities.Referral entity)
    {
      return Task.FromResult<bool>(true);
    }

    protected override Task<bool> InternalUpdateAsync(Referral model, Entities.Referral entity)
    {
      return Task.FromResult<bool>(true);
    }
  }
}
