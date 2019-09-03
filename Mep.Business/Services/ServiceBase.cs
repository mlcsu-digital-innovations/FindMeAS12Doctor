using System;
using System.Threading.Tasks;
using AutoMapper;
using Mep.Business.Models;
using Mep.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Mep.Business.Models.SearchModels;
using System.Collections.Generic;
using Mep.Business.Exceptions;

namespace Mep.Business.Services
{
  public abstract class ServiceBase<TBusinessModel, TEntity>
    where TBusinessModel : BaseModel
    where TEntity : BaseEntity
  {
    protected readonly ApplicationContext _context;
    protected readonly IMapper _mapper;
    protected readonly string _typeName;

    protected abstract Task<TEntity> GetEntityByIdAsync(
      int entityId,
      bool asNoTracking,
      bool activeOnly);

    protected abstract Task<bool> InternalCreateAsync(
      TBusinessModel model,
      TEntity entity
    );

    protected abstract Task<bool> InternalUpdateAsync(
      TBusinessModel model,
      TEntity entity
    );

    protected abstract Task<TEntity> GetEntityLinkedObjectsAsync(
      TBusinessModel model,
      TEntity entity
    );

    public async Task<TBusinessModel> GetByIdAsync(
      int id,
      bool activeOnly)
    {
      TEntity userEntity = await GetEntityByIdAsync(id, true, activeOnly);
      if (userEntity == null)
      {
        throw new EntityNotFoundException(_typeName, id);
      }
      else
      {
        TBusinessModel userModel = _mapper.Map<TBusinessModel>(userEntity);
        return userModel;
      }
    }

    protected ServiceBase(string typeName, ApplicationContext context, IMapper mapper)
    {
      _typeName = typeName;
      _context = context;
      _mapper = mapper;
    }

    public async Task<TBusinessModel> CreateAsync(
      TBusinessModel model)
    {
      TEntity entity = await GetEntityByIdAsync(model.Id, true, false);

      if (entity == null)
      {
        try
        {
          entity = _mapper.Map<TEntity>(model);
          entity.IsActive = true;

          UpdateModified(entity);
          _context.Add(entity);

          await InternalCreateAsync(model, entity);

          await _context.SaveChangesAsync();

          model = await GetByIdAsync(entity.Id, true);
          return model;
        }
        catch (Exception ex)
        {
          //TODO: catch and create 
          throw new Exception($"Failed to create {_typeName}.", ex);
        }
      }
      else
      {
        //TODO: Create a specific exception
        throw new Exception(
          $"A {(entity.IsActive ? "" : "deleted")} " +
          $"{_typeName} with an id of {model.Id} already exists.");
      }
    }

    public async Task<int> ActivateAsync(int id)
    {
      return await SetActiveStatus(id, true);
    }

    public async Task<int> DeactivateAsync(int id)
    {
      return await SetActiveStatus(id, false);
    }

    public async Task<TBusinessModel> UpdateAsync(
      TBusinessModel model)
    {
      TEntity entity =
        await GetEntityByIdAsync(model.Id, false, false);

      if (entity == null)
      {
        //TODO: Create a specific exception
        throw new Exception($"A {_typeName} with an id of {model.Id} does not exist.");
      }
      else
      {
        _mapper.Map<TBusinessModel, TEntity>(model, entity);
        UpdateModified(entity);

        await GetEntityLinkedObjectsAsync(model, entity);

        await InternalUpdateAsync(model, entity);

        await _context.SaveChangesAsync();

        model = await GetByIdAsync(model.Id, model.IsActive);
        return model;
      }
    }

    protected async Task<T> GetLinkedObjectAsync<T>(DbSet<T> appContext, int propertyId) where T : BaseEntity
    {
      try
      {
        return await appContext.SingleAsync(x => x.Id == propertyId);
      }
      catch
      {
        var model = _context.Model;
        var entityTypes = model.GetEntityTypes();
        var entityType = entityTypes.First(t => t.ClrType == typeof(T));
        var tableNameAnnotation = entityType.GetAnnotations().First(a => a.Name == "Relational:TableName");
        var tableName = tableNameAnnotation.Value.ToString();
        //TODO: Create a specific exception
        throw new Exception($"Table [{tableName}] does not contain a row with an id of {propertyId}");
      }
    }

    protected void UpdateModified(TEntity entity)
    {
      //TODO: Get the current users sub claim
      entity.ModifiedByUserId = 1;
      entity.ModifiedAt = DateTimeOffset.Now;
    }

    public async Task<TBusinessModel> UpdateEntityAsync(TBusinessModel model)
    {
      TEntity entity =
        await GetEntityByIdAsync(model.Id, false, false);

      if (entity == null)
      {
        //TODO: Create a specific exception
        throw new Exception($"{typeof(TEntity).Name} with an id of {model.Id} does not exist.");
      }
      else
      {
        _mapper.Map<TBusinessModel, TEntity>(model, entity);
        UpdateModified(entity);
        await _context.SaveChangesAsync();

        model = await GetByIdAsync(model.Id, model.IsActive);
        return model;
      }
    }

    private async Task<int> SetActiveStatus(int id, bool isActivating)
    {
      TEntity entity = await _context.Set<TEntity>()
                                     .FindAsync(id);

      if (entity == null)
      {
        //TODO: Create a specific exception
        throw new Exception($"{typeof(TEntity).Name} with an id of {id} does not exist.");
      }
      else if (entity.IsActive == isActivating)
      {
        //TODO: Create a specific exception
        throw new Exception(
          $"{typeof(TEntity).Name} with an id of {id} is already {(isActivating ? "active" : "inactive")}.");
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