using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mep.Business.Models;
using Entities = Mep.Data.Entities;
using Mep.Business.Extensions;

namespace Mep.Business.Services
{
  //TODO - include CCG details in query, will report self referencing loop if mapping not corrected

  public class ReferralStatusService
    : ServiceBase<ReferralStatus, Entities.ReferralStatus>, IModelService<ReferralStatus>
  {
    public ReferralStatusService(ApplicationContext context, IMapper mapper)
      : base("ReferralStatus", context, mapper)
    {
    }

    public async Task<IEnumerable<Models.ReferralStatus>> GetAllAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.ReferralStatus> entities =
        await _context.ReferralStatuses
                      .WhereIsActiveOrActiveOnly(activeOnly)
                      .ToListAsync();

      IEnumerable<Models.ReferralStatus> models =
        _mapper.Map<IEnumerable<Models.ReferralStatus>>(entities);

      return models;
    }

    protected override async Task<Entities.ReferralStatus> GetEntityByIdAsync(
      int id,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.ReferralStatus entity = await
        _context.ReferralStatuses
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(u => u.Id == id);

      return entity;
    }

    protected override Task<Entities.ReferralStatus> GetEntityLinkedObjectsAsync(ReferralStatus model, Entities.ReferralStatus entity)
    {
      return Task.FromResult(entity);
    }

    protected override Task<bool> InternalCreateAsync(ReferralStatus model, Entities.ReferralStatus entity)
    {
      return Task.FromResult<bool>(true);
    }

    protected override Task<bool> InternalUpdateAsync(ReferralStatus model, Entities.ReferralStatus entity)
    {
      return Task.FromResult<bool>(true);
    }
  }
}