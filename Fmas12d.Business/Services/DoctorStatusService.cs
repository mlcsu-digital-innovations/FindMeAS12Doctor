using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Fmas12d.Business.Models;
using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Extensions;

namespace Fmas12d.Business.Services
{
  public class DoctorStatusService
    : ServiceBase<DoctorStatus, Entities.DoctorStatus>, IModelService<DoctorStatus>
  {
    public DoctorStatusService(ApplicationContext context, IMapper mapper)
      : base("DoctorStatus", context, mapper)
    {
    }

    public async Task<IEnumerable<Models.DoctorStatus>> GetAllAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.DoctorStatus> entities =
        await _context.DoctorStatuses
                .WhereIsActiveOrActiveOnly(activeOnly)
                .ToListAsync();

      IEnumerable<Models.DoctorStatus> models =
        _mapper.Map<IEnumerable<Models.DoctorStatus>>(entities);

      return models;
    }

    protected override async Task<Entities.DoctorStatus> GetEntityByIdAsync(
      int entityId,
      bool asNoTracking,
      bool activeOnly)
    {
      return await GetEntityWithNoIncludesByIdAsync(entityId, asNoTracking, activeOnly);
    }

    protected override async Task<Entities.DoctorStatus> GetEntityWithNoIncludesByIdAsync(
      int entityId,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.DoctorStatus entity = await
        _context.DoctorStatuses
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(doctorStatus => doctorStatus.Id == entityId);

      return entity;
    }
  }
}