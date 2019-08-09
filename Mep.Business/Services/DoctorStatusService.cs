using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mep.Business.Models;
using Entities = Mep.Data.Entities;
using Mep.Business.Extensions;
using Mep.Business.Models.SearchModels;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace Mep.Business.Services
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
      int id,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.DoctorStatus entity = await
        _context.DoctorStatuses
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(u => u.Id == id);

      return entity;
    }

    protected override Task<Entities.DoctorStatus> GetEntityLinkedObjectsAsync(DoctorStatus model, Entities.DoctorStatus entity)
    {
      return Task.FromResult(entity);
    }

    protected override Task<bool> InternalCreateAsync(DoctorStatus model, Entities.DoctorStatus entity)
    {
      return Task.FromResult<bool>(true);
    }

    protected override Task<bool> InternalUpdateAsync(DoctorStatus model, Entities.DoctorStatus entity)
    {
      return Task.FromResult<bool>(true);
    }
  }
}