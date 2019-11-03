using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fmas12d.Business.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Fmas12d.Business.Services
{
  public abstract class NameDescriptionBaseService<TEntity> : 
    ServiceBaseNoAutoMapper<TEntity>, INameDescriptionBaseService 
      where TEntity : Data.Entities.NameDescription
  {
    protected NameDescriptionBaseService(ApplicationContext context)
      : base(context)
    { }

    public async Task<IEnumerable<Models.NameDescription>> GetNameDescriptions(
      bool asNoTracking = true,
      bool activeOnly = true)
    {
      IEnumerable<Models.NameDescription> models = await _context
        .Set<TEntity>()
        .WhereIsActiveOrActiveOnly(activeOnly)
        .AsNoTracking(asNoTracking)
        .Select(Models.NameDescription.ProjectFromEntity)
        .ToListAsync();

      return models;
    }

  }
}