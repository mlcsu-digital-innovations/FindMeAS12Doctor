using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mep.Business.Models;
using Entities = Mep.Data.Entities;
using Mep.Business.Extensions;

namespace Mep.Business.Services
{
  public class SpecialityService
    : ServiceBase<Speciality, Entities.Speciality>, IModelService<Speciality>
  {
    public SpecialityService(ApplicationContext context, IMapper mapper)
      : base("Speciality", context, mapper)
    {
    }

    public async Task<IEnumerable<Models.Speciality>> GetAllAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.Speciality> entities =
        await _context.Specialities
                .WhereIsActiveOrActiveOnly(activeOnly)
                .ToListAsync();

      IEnumerable<Models.Speciality> models =
        _mapper.Map<IEnumerable<Models.Speciality>>(entities);

      return models;
    }

    protected override async Task<Entities.Speciality> GetEntityByIdAsync(
      int id,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.Speciality entity = await
        _context.Specialities
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(u => u.Id == id);

      return entity;
    }

    protected override Task<Entities.Speciality> GetEntityLinkedObjectsAsync(Speciality model, Entities.Speciality entity)
    {
      return Task.FromResult(entity);
    }

    protected override Task<bool> InternalCreateAsync(Speciality model, Entities.Speciality entity)
    {
      return Task.FromResult<bool>(true);
    }

    protected override Task<bool> InternalUpdateAsync(Speciality model, Entities.Speciality entity)
    {
      return Task.FromResult<bool>(true);
    }
  }
}