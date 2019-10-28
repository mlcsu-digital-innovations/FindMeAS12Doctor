using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mep.Business.Models;
using Entities = Mep.Data.Entities;
using Mep.Business.Extensions;
using System.Linq;

namespace Mep.Business.Services
{
  public class UserService
: ServiceBase<User, Entities.User>, IModelService<User>, IUserService
  {
    public UserService(ApplicationContext context, IMapper mapper)
      : base("User", context, mapper)
    {
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