using Fmas12d.Business.Extensions;
using Fmas12d.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public abstract class NameDescriptionBaseService<TEntity> : 
    ServiceBaseNoAutoMapper<TEntity>, INameDescriptionBaseService 
      where TEntity : Data.Entities.NameDescription
  {
    protected NameDescriptionBaseService(
      ApplicationContext context,
      IUserClaimsService userClaimsService)
      : base(context, userClaimsService)
    { }

    public async Task<IEnumerable<NameDescription>> GetNameDescriptions(
      bool asNoTracking = true,
      bool activeOnly = true)
    {
      IEnumerable<NameDescription> models = await _context
        .Set<TEntity>()
        .WhereIsActiveOrActiveOnly(activeOnly)
        .AsNoTracking(asNoTracking)
        .Select(NameDescription.ProjectFromEntity)
        .ToListAsync();

      return models;
    }

  }
}