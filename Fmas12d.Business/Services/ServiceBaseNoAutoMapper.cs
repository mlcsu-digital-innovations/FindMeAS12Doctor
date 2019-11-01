using System;
using System.Threading.Tasks;
using Mep.Business.Exceptions;
using Mep.Data.Entities;

namespace Mep.Business.Services
{
  public abstract class ServiceBaseNoAutoMapper<TEntity> : IServiceBaseNoAutoMapper
    where TEntity : BaseEntity
  {
    protected readonly ApplicationContext _context;

    protected ServiceBaseNoAutoMapper(ApplicationContext context)
    {
      _context = context;
    }

    public async Task<int> ActivateAsync(int id)
    {
      return await SetActiveStatus(id, true);
    }
    public async Task<int> DeactivateAsync(int id)
    {
      return await SetActiveStatus(id, false);
    }

    /// <summary>
    /// //TODO: Get the current users sub claim
    /// </summary>
    protected void UpdateModified(BaseEntity entity)
    {
      entity.ModifiedByUserId = 1;
      entity.ModifiedAt = DateTimeOffset.Now;
    }

    private async Task<int> SetActiveStatus(int id, bool isActivating)
    {
      TEntity entity = await _context.Set<TEntity>()
                                     .FindAsync(id);

      if (entity == null)
      {
        throw new EntityNotFoundException(typeof(TEntity).Name, id);
      }
      else if (entity.IsActive == isActivating)
      {
        throw new EntityAlreadyActiveException(isActivating, typeof(TEntity).Name, id);
      }
      else
      {
        entity.IsActive = isActivating;
        UpdateModified(entity);
        return await _context.SaveChangesAsync();
      }
    }

  }
}