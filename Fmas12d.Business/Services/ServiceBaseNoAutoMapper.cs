using System;
using System.Threading.Tasks;
using Fmas12d.Business.Exceptions;
using Fmas12d.Business.Models;
using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Services
{
  public abstract class ServiceBaseNoAutoMapper<TEntity> : IServiceBaseNoAutoMapper
    where TEntity : BaseEntity
  {
    protected readonly ApplicationContext _context;
    protected readonly IUserClaimsService _userClaimsService;

    protected ServiceBaseNoAutoMapper(
      ApplicationContext context,
      IUserClaimsService userClaimsService
    )
    {
      _userClaimsService = userClaimsService;
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

    protected void UpdateModified(IBaseEntity entity)
    {
      entity.ModifiedByUserId = _userClaimsService.GetUserId();
      entity.ModifiedAt = DateTimeOffset.Now;
    }

    private async Task<int> SetActiveStatus(int id, bool isActivating)
    {
      TEntity entity = await _context.Set<TEntity>()
                                     .FindAsync(id);

      if (entity == null)
      {
        throw new ModelStateException("Id",
          $"A {typeof(TEntity).Name} with an id of {id} was not found.");
      }
      else if (entity.IsActive == isActivating)
      {
        throw new ModelStateException("Id",
          $"{typeof(TEntity).Name} with an id of {id} is already " + 
          $"{(isActivating ? "active" : "inactive")}.");
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