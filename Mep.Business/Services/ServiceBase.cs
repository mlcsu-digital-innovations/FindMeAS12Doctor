using System;
using System.Threading.Tasks;
using AutoMapper;
using Mep.Business.Exceptions;
using Mep.Business.Models;
using Mep.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

    public async Task<TBusinessModel> CreateAsync(
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

    public async Task<TBusinessModel> GetByIdAsync(
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
      Serilog.Log.Verbose("Entry Model: {@model}", model);

      TEntity entity =
        await GetEntityByIdAsync(model.Id, false, false);

      Serilog.Log.Verbose("Get Entity: {@entity}", entity);

      if (entity == null)
      {
        throw new EntityNotFoundException(_typeName, model.Id);
      }
      else
      {
        entity.IsActive = model.IsActive;
        Serilog.Log.Verbose("Map Entity: {@entity}", entity);
        UpdateModified(entity);
        Serilog.Log.Verbose("Update Mod Entity: {@entity}", entity);

        await InternalUpdateAsync(model, entity);
        Serilog.Log.Debug("Internal Update Entity: {@entity}", entity);

        await _context.SaveChangesAsync();

        model = await GetByIdAsync(model.Id, model.IsActive);
        Serilog.Log.Debug("After Save Model: {@model}", model);
        return model;
      }
    }

    protected abstract Task<TEntity> GetEntityByIdAsync(
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

    protected void UpdateModified(BaseEntity entity)
    {
      //TODO: Get the current users sub claim
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