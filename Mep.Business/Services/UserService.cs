using AutoMapper;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Mep.Business.Models;
using Entities = Mep.Data.Entities;

namespace Mep.Business.Services
{
  public class UserService
    : ServiceBase<User, Entities.User>, IModelService<User>
  {
    public UserService(ApplicationContext context, IMapper mapper)
      :base(context, mapper)
    {
    }

    public async Task<User> CreateAsync(
      User userModel)
    {
      Entities.User userEntity = await GetEntityByIdAsync(userModel.Id, true, false);

      if (userEntity == null)
      {      
        try
        {
          Entities.User newUserEntity = _mapper.Map<Entities.User>(userModel);
          newUserEntity.IsActive = true;

          UpdateModified(newUserEntity);
          _context.Add(newUserEntity);
          await _context.SaveChangesAsync();

          userModel = await GetByIdAsync(newUserEntity.Id, true);
          return userModel;
        }
        catch (Exception ex)
        {
          //TODO: catch and create 
          throw new Exception("Failed to create User.", ex);
        }        
      }
      else
      {
        //TODO: Create a specific exception
        throw new Exception(
          $"A {(userEntity.IsActive ? "" : "deleted")} " +
          $"User with an id of {userModel.Id} already exists.");
      }      
    }

    public override async Task<Models.User> GetByIdAsync(
      int id,
      bool activeOnly)
    {
      Entities.User userEntity = await GetEntityByIdAsync(id, true, activeOnly);
      if (userEntity == null)
      {
        //TODO: Create a specific exception
        throw new Exception($"User with an id of {id} does not exist.");
      }
      else
      {
        Models.User userModel = _mapper.Map<Models.User>(userEntity);
        return userModel;
      }
    }

    public async Task<IEnumerable<Models.User>> GetAllAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.User> userEntities = 
        await _context.Users
                .Where(u => u.IsActive && activeOnly || !activeOnly)
                .ToListAsync();

      IEnumerable<Models.User> userModels = 
        _mapper.Map<IEnumerable<Models.User>>(userEntities);

      return userModels;
    }

    public async Task<User> UpdateAsync(
      User userModel)
    {
      Entities.User userEntity = 
        await GetEntityByIdAsync(userModel.Id, false, false);

      if (userEntity == null)
      {
        //TODO: Create a specific exception
        throw new Exception($"User with an id of {userModel.Id} does not exist.");
      }
      else
      {
        _mapper.Map<User, Entities.User>(userModel, userEntity);
        UpdateModified(userEntity);
        await _context.SaveChangesAsync();

        userModel = await GetByIdAsync(userModel.Id, userModel.IsActive);
        return userModel;
      }
    }

    protected override async Task<Entities.User> GetEntityByIdAsync(
      int id, 
      bool asNoTracking,
      bool activeOnly)
    {
      IQueryable<Entities.User> query = 
        _context.Users
            .Where(u => u.IsActive && activeOnly || !activeOnly);

      if (asNoTracking) {
        query = query.AsNoTracking();
      }

      Entities.User userEntity = await 
        query.SingleOrDefaultAsync(u => u.Id == id);

      return userEntity;  
    }
    
  }
}