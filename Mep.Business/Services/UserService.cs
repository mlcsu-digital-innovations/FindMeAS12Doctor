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
      :base("User", context, mapper)
    {
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

    protected override Task<bool> InternalCreateAsync(User model, Entities.User entity)
    {
      return Task.FromResult<bool>(true);
    }

    protected override Task<bool> InternalUpdateAsync(User model, Entities.User entity)
    {
      return Task.FromResult<bool>(true);
    }
  }
}