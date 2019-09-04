using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mep.Business.Models;
using Entities = Mep.Data.Entities;
using Mep.Business.Extensions;

namespace Mep.Business.Services
{
  public class GenderTypeService
    : ServiceBase<GenderType, Entities.GenderType>, IModelService<GenderType>
  {
    public GenderTypeService(ApplicationContext context, IMapper mapper)
      :base("GenderType", context, mapper)
    {
    }

    public async Task<IEnumerable<Models.GenderType>> GetAllAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.GenderType> entities = 
        await _context.GenderTypes
                      .WhereIsActiveOrActiveOnly(activeOnly)
                      .ToListAsync();

      IEnumerable<Models.GenderType> models = 
        _mapper.Map<IEnumerable<Models.GenderType>>(entities);

      return models;
    }

    protected override async Task<Entities.GenderType> GetEntityByIdAsync(
      int entityId, 
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.GenderType entity = await 
        _context.GenderTypes
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(genderType => genderType.Id == entityId);

      return entity;  
    }

    protected override Task<Entities.GenderType> GetEntityLinkedObjectsAsync(GenderType model, Entities.GenderType entity)
    {
      return Task.FromResult(entity);
    }

    protected override Task<bool> InternalCreateAsync(GenderType model, Entities.GenderType entity)
    {
      return Task.FromResult<bool>(true);
    }

    protected override Task<bool> InternalUpdateAsync(GenderType model, Entities.GenderType entity)
    {
      return Task.FromResult<bool>(true);
    }
  }
}