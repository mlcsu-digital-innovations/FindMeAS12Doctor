using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mep.Business.Models;
using Entities = Mep.Data.Entities;
using Mep.Business.Extensions;

namespace Mep.Business.Services
{
  public class UserService
    : ServiceBase<User, Entities.User>, IModelService<User>
  {
    public UserService(ApplicationContext context, IMapper mapper)
      :base("User", context, mapper)
    {
    }

    public async Task<IEnumerable<Models.User>> GetAllAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.User> entities = 
        await _context.Users
                      .Include(u => u.GenderType)
                      .WhereIsActiveOrActiveOnly(activeOnly)
                      .ToListAsync();

      IEnumerable<Models.User> models = 
        _mapper.Map<IEnumerable<Models.User>>(entities);

      return models;
    }

    protected override async Task<Entities.User> GetEntityByIdAsync(
      int entityId, 
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.User entity = await 
        _context.Users
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(user => user.Id == entityId);

      return entity;  
    }

    protected override Task<Entities.User> GetEntityLinkedObjectsAsync(User model, Entities.User entity)
    {
      return Task.FromResult(entity);
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