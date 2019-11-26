using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Fmas12d.Business.Models;
using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Extensions;
using System.Linq;
using Fmas12d.Business.Exceptions;

namespace Fmas12d.Business.Services
{
  public class UserService
: ServiceBase<User, Entities.User>, IModelService<User>, IUserService
  {
    public UserService(ApplicationContext context, IMapper mapper)
      : base("User", context, mapper)
    {
    }

    private async Task<User> CheckUserIs(
      int id,
      string modelPropertyName,
      bool asNoTracking = true,
      bool activeOnly = true)
    {
      User user = await _context.Users
                                .Include(u => u.ProfileType)
                                .Where(u => u.Id == id)
                                .WhereIsActiveOrActiveOnly(activeOnly)
                                .AsNoTracking(asNoTracking)
                                .Select(User.ProjectFromEntity)
                                .SingleOrDefaultAsync();
      if (user == null)
      {
        throw new ModelStateException(
          modelPropertyName, 
          $"A{(activeOnly ? "n active" : "")} User with an Id of {id} does not exist."
        );
      }

      return user;
    }    

    public async Task<bool> CheckIsAmhp(
      int id,
      string modelPropertyName,
      bool asNoTracking = true,
      bool activeOnly = true)
    {
      User user = await CheckUserIs(id, modelPropertyName, asNoTracking, activeOnly);
      if (!user.IsAmhp)
      {
        throw new ModelStateException(
          modelPropertyName, 
          $"The User with an Id of {id} must be an AMHP but is a {user.ProfileType.Name}.");
      }
      return true;
    }

    public async Task<bool> CheckIsADoctor(
      int id,
      string modelPropertyName,
      bool asNoTracking = true, 
      bool activeOnly = true)
    {
      User user = await CheckUserIs(id, modelPropertyName, asNoTracking, activeOnly);
      if (!user.IsDoctor)
      {
        throw new ModelStateException(
          modelPropertyName, 
          $"The User with an Id of {id} must be a Doctor but is a {user.ProfileType.Name}.");
      }
      return true;      
    }

    public async Task<IEnumerable<Models.User>> GetAllAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.User> entities =
        await _context.Users
                      .Include(u => u.GenderType)
                      .Include(u => u.ProfileType)
                      .WhereIsActiveOrActiveOnly(activeOnly)
                      .ToListAsync();

      IEnumerable<Models.User> models =
        _mapper.Map<IEnumerable<Models.User>>(entities);

      return models;
    }

    public async Task<IEnumerable<Models.User>> GetAllByAmhpName(
      string amhpName,
      bool asNoTracking = true,
      bool activeOnly = true)
    {
      return await GetAllByNameAndProfileTypeId(
        amhpName, Models.ProfileType.AMHP, asNoTracking, activeOnly);
    }

    public async Task<IEnumerable<Models.User>> GetAllByDoctorName(
      string doctorName,
      bool asNoTracking = true,
      bool activeOnly = true)
    {
      return await GetAllByNameAndProfileTypeId(
        doctorName, Models.ProfileType.DOCTOR, asNoTracking, activeOnly);
    }

    private async Task<IEnumerable<Models.User>> GetAllByNameAndProfileTypeId(
      string name,
      int profileTypeId,
      bool asNoTracking = true,
      bool activeOnly = true)
    {
       IEnumerable<Models.User> models = await _context.Users
        .WhereIsActiveOrActiveOnly(activeOnly)
        .Where(u => u.DisplayName.Contains(name))
        .Where(u => u.ProfileTypeId == profileTypeId)
        .AsNoTracking(asNoTracking)
        .Select(User.ProjectFromEntity)
        .ToListAsync();

      return models;
    }

    protected override async Task<Entities.User> GetEntityByIdAsync(
      int entityId,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.User entity = await
        _context.Users
                .Include(u => u.GenderType)
                .Include(u => u.ProfileType)
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(user => user.Id == entityId);

      return entity;
    }

    protected override async Task<Entities.User> GetEntityWithNoIncludesByIdAsync(
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
    
  }
}