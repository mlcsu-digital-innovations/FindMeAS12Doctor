using System;
using System.Threading.Tasks;
using AutoMapper;
using Mep.Business.Exceptions;
using Mep.Business.Models;
using Mep.Data.Entities;

namespace Mep.Business.Services
{
  public abstract class ServiceBase<TBusinessModel, TEntity>
    where TBusinessModel : BaseModel
    where TEntity : BaseEntity
  {
    protected readonly ApplicationContext _context;
    protected readonly IMapper _mapper;
    protected readonly string _typeName;

    protected ServiceBase(string typeName, ApplicationContext context, IMapper mapper)
    {
      _typeName = typeName;
      _context = context;
      _mapper = mapper;
    }

    public async Task<int> ActivateAsync(int id)
    {
      return await SetActiveStatus(id, true);
    }

    public virtual async Task<TBusinessModel> CreateAsync(
      TBusinessModel model)
    {
      TEntity entity = _mapper.Map<TEntity>(model);
      entity.Id = 0;
      entity.IsActive = true;

      UpdateModified(entity);
      _context.Add(entity);

      await InternalCreateAsync(model, entity);

      await _context.SaveChangesAsync();

      model = await GetByIdAsync(entity.Id, true);
      return model;
    }

    public async Task<int> DeactivateAsync(int id)
    {
      return await SetActiveStatus(id, false);
    }

    public virtual async Task<TBusinessModel> GetByIdAsync(
      int id,
      bool activeOnly)
    {
      TBusinessModel model = null;
      TEntity entity = await GetEntityByIdAsync(id, true, activeOnly);
      if (entity != null)
      {
        model = _mapper.Map<TBusinessModel>(entity);
      }
      return model;
    }

    public async Task<TBusinessModel> UpdateAsync(
      TBusinessModel model)
    {
      TEntity entity =
        await GetEntityByIdAsync(model.Id, false, false);

      if (entity == null)
      {
        throw new EntityNotFoundException(_typeName, model.Id);
      }
      else
      {
        entity.IsActive = model.IsActive;
        UpdateModified(entity);

        await InternalUpdateAsync(model, entity);

        await _context.SaveChangesAsync();

        model = await GetByIdAsync(model.Id, model.IsActive);

        return model;
      }
    }

    protected abstract Task<TEntity> GetEntityByIdAsync(
      int entityId,
      bool asNoTracking,
      bool activeOnly);

    protected abstract Task<TEntity> GetEntityWithNoIncludesByIdAsync(
      int entityId,
      bool asNoTracking,
      bool activeOnly);

    protected virtual Task<bool> InternalCreateAsync(
      TBusinessModel model, TEntity entity)
    {
      return Task.FromResult<bool>(true);
    }

    protected virtual Task<bool> InternalUpdateAsync(
      TBusinessModel model, TEntity entity)
    {
      return Task.FromResult<bool>(true);
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