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
  public class SpecialityService
    : ServiceBase<Speciality, Entities.Speciality>, IModelService<Speciality>
  {
    public SpecialityService(ApplicationContext context, IMapper mapper)
      :base("Speciality", context, mapper)
    {
    }

    public override async Task<Models.Speciality> GetByIdAsync(
      int id,
      bool activeOnly)
    {
      Entities.Speciality entity = await GetEntityByIdAsync(id, true, activeOnly);
      if (entity == null)
      {
        //TODO: Create a specific exception
        throw new Exception($"Speciality with an id of {id} does not exist.");
      }
      else
      {
        Models.Speciality model = _mapper.Map<Models.Speciality>(entity);
        return model;
      }
    }

    public async Task<IEnumerable<Models.Speciality>> GetAllAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.Speciality> entities = 
        await _context.Specialities
                .Where(u => u.IsActive && activeOnly || !activeOnly) 
                .ToListAsync();

      IEnumerable<Models.Speciality> models = 
        _mapper.Map<IEnumerable<Models.Speciality>>(entities);

      return models;
    }



    protected override async Task<Entities.Speciality> GetEntityByIdAsync(
      int id, 
      bool asNoTracking,
      bool activeOnly)
    {
      IQueryable<Entities.Speciality> query = 
        _context.Specialities
            .Include(u => u.UserSpecialities)
            .ThenInclude(s => s.Speciality)
            .Where(u => u.IsActive && activeOnly || !activeOnly);

      if (asNoTracking) {
        query = query.AsNoTracking();
      }

      Entities.Speciality specialityEntity = await 
        query.SingleOrDefaultAsync(u => u.Id == id);

      return specialityEntity;  
    }

    protected override Task<bool> InternalCreateAsync(Speciality model, Entities.Speciality entity)
    {
      return Task.FromResult<bool>(true);
    }

    protected override Task<bool> InternalUpdateAsync(Speciality model, Entities.Speciality entity)
    {
      return Task.FromResult<bool>(true);
    }
  }
}