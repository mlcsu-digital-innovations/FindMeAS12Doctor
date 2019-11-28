using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Fmas12d.Business.Models;
using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Extensions;

// TODO CONVERT TO NO AUTOMAPPER

namespace Fmas12d.Business.Services
{
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
      int entityId,
      bool asNoTracking,
      bool activeOnly)
    {
      return await GetEntityWithNoIncludesByIdAsync(entityId, asNoTracking, activeOnly);
    }
    protected override async Task<Entities.ReferralStatus> GetEntityWithNoIncludesByIdAsync(
      int entityId,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.ReferralStatus entity = await
        _context.ReferralStatuses
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(referralStatus => referralStatus.Id == entityId);

      return entity;
    }
  }
}