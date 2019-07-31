using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mep.Business.Models;
using Entities = Mep.Data.Entities;
using Mep.Business.Extensions;

namespace Mep.Business.Services
{
  public class CcgService
    : ServiceBase<Ccg, Entities.Ccg>, IModelService<Ccg>
  {
    public CcgService(ApplicationContext context, IMapper mapper)
      :base("Ccg", context, mapper)
    {
    }

    public async Task<IEnumerable<Models.Ccg>> GetAllAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.Ccg> entities = 
        await _context.Ccgs
                      .WhereIsActiveOrActiveOnly(activeOnly)
                      .ToListAsync();

      IEnumerable<Models.Ccg> models = 
        _mapper.Map<IEnumerable<Models.Ccg>>(entities);

      return models;
    }

    protected override async Task<Entities.Ccg> GetEntityByIdAsync(
      int id, 
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.Ccg entity = await 
        _context.Ccgs
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(u => u.Id == id);

      return entity;  
    }

    protected override Task<Entities.Ccg> GetEntityLinkedObjectsAsync(Ccg model, Entities.Ccg entity)
    {
      return Task.FromResult(entity);
    }

    protected override Task<bool> InternalCreateAsync(Ccg model, Entities.Ccg entity)
    {
      return Task.FromResult<bool>(true);
    }

    protected override Task<bool> InternalUpdateAsync(Ccg model, Entities.Ccg entity)
    {
      return Task.FromResult<bool>(true);
    }
  }
}