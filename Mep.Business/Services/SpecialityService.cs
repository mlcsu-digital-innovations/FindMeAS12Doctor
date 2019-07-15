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
    :ServiceBase<Speciality, Entities.Speciality>, ISpecialityService
  {
    public SpecialityService(ApplicationContext context, IMapper mapper)
      :base(context, mapper)
    {
    }

    public async Task<Speciality> CreateSpecialityAsync(
      Speciality specialityModel)
    {
      Entities.Speciality specialityEntity = await GetEntityByIdAsync(specialityModel.Id, true, false);

      if (specialityEntity == null)
      {      
        try
        {
          Entities.Speciality newSpecialityEntity = _mapper.Map<Entities.Speciality>(specialityModel);
          newSpecialityEntity.IsActive = true;

          // Speciality specialities is many-to-many and has to be mapped manually
          if (newSpecialityEntity.UserSpecialities != null)
          {
            foreach (Entities.UserSpeciality userSpeciality in newSpecialityEntity.UserSpecialities)
            {
              userSpeciality.Speciality = newSpecialityEntity;
            }
          }

          UpdateModified(newSpecialityEntity);
          _context.Add(newSpecialityEntity);
          await _context.SaveChangesAsync();

          specialityModel = await GetModelByIdAsync(newSpecialityEntity.Id, true);
          return specialityModel;
        }
        catch (Exception ex)
        {
          //TODO: catch and create 
          throw new Exception("Failed to create Speciality.", ex);
        }        
      }
      else
      {
        //TODO: Create a specific exception
        throw new Exception(
          $"A {(specialityEntity.IsActive ? "" : "deleted")} " +
          $"Speciality with an id of {specialityModel.Id} already exists.");
      }      
    }

    public override async Task<Models.Speciality> GetModelByIdAsync(
      int id,
      bool activeOnly)
    {
      Entities.Speciality specialityEntity = await GetEntityByIdAsync(id, true, activeOnly);
      if (specialityEntity == null)
      {
        //TODO: Create a specific exception
        throw new Exception($"Speciality with an id of {id} does not exist.");
      }
      else
      {
        Models.Speciality specialityModel = _mapper.Map<Models.Speciality>(specialityEntity);
        return specialityModel;
      }
    }

    public async Task<IEnumerable<Models.Speciality>> GetSpecialitiesAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.Speciality> specialityEntities = 
        await _context.Specialities
                .Where(u => u.IsActive && activeOnly || !activeOnly)
                .Include(u => u.UserSpecialities)
                .ThenInclude(s => s.Speciality)        
                .ToListAsync();

      IEnumerable<Models.Speciality> specialityModels = 
        _mapper.Map<IEnumerable<Models.Speciality>>(specialityEntities);

      return specialityModels;
    }

    public async Task<int> UndeleteSpecialityAsync(
      int id)
    {
      Entities.Speciality specialityEntity = await GetEntityByIdAsync(id, false, false);

      if (specialityEntity == null)
      {
        //TODO: Create a specific exception
        throw new Exception($"Speciality with an id of {id} does not exist.");
      }
      else
      {
        if (specialityEntity.IsActive)
        {
          //TODO: Create a specific exception
          throw new Exception($"Speciality with an id of {id} is not deleted.");
        }
        else
        {
          specialityEntity.IsActive = true;
          UpdateModified(specialityEntity);

          return await _context.SaveChangesAsync();
        }
      }    
    }     

    public async Task<Speciality> UpdateSpecialityAsync(
      Speciality specialityModel)
    {
      Entities.Speciality specialityEntity = 
        await GetEntityByIdAsync(specialityModel.Id, false, false);

      if (specialityEntity == null)
      {
        //TODO: Create a specific exception
        throw new Exception($"Speciality with an id of {specialityModel.Id} does not exist.");
      }
      else
      {
        _mapper.Map<Speciality, Entities.Speciality>(specialityModel, specialityEntity);
        UpdateModified(specialityEntity);
        await _context.SaveChangesAsync();

        specialityModel = await GetModelByIdAsync(specialityModel.Id, specialityModel.IsActive);
        return specialityModel;
      }
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
    
  }
}