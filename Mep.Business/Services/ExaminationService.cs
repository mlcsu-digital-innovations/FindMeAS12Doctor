using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mep.Business.Models;
using Entities = Mep.Data.Entities;
using Mep.Business.Extensions;
using System;

namespace Mep.Business.Services
{
  public class ExaminationService
    : ServiceBase<Examination, Entities.Examination>, IModelService<Examination>
  {
    public ExaminationService(ApplicationContext context, IMapper mapper)
      : base("Examination", context, mapper)
    {
    }

    public async Task<IEnumerable<Models.Examination>> GetAllAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.Examination> entities =
        await _context.Examinations
                .Include(r => r.CreatedByUser)

                .WhereIsActiveOrActiveOnly(activeOnly)
                .ToListAsync();

      IEnumerable<Models.Examination> models =
        _mapper.Map<IEnumerable<Models.Examination>>(entities);

      return models;
    }

    public async Task<IEnumerable<Models.Examination>> GetAllDetailsAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.Examination> entities =
        await _context.Examinations
                .Include(r => r.CreatedByUser)
                
                .WhereIsActiveOrActiveOnly(activeOnly)
                .ToListAsync();

      IEnumerable<Models.Examination> models =
        _mapper.Map<IEnumerable<Models.Examination>>(entities);

      return models;
    }


    protected override async Task<Entities.Examination> GetEntityLinkedObjectsAsync(Examination model, Entities.Examination entity) {

        //entity.Patient = await GetLinkedObjectAsync<Entities.Patient>(_context.Patients, model.PatientId);
        entity.CreatedByUser =await GetLinkedObjectAsync<Entities.User>(_context.Users, model.CreatedByUserId);
        //entity.ExaminationStatus =await GetLinkedObjectAsync<Entities.ExaminationStatus>(_context.ExaminationStatuses, model.ExaminationStatusId);
               
        return entity;
    }


    protected override async Task<Entities.Examination> GetEntityByIdAsync(
      int id,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.Examination entity = await
        _context.Examinations
                .Include(r => r.CreatedByUser)
                
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(u => u.Id == id);

      return entity;
    }

    protected override Task<bool> InternalCreateAsync(Examination model, Entities.Examination entity)
    {
      return Task.FromResult<bool>(true);
    }

    protected override Task<bool> InternalUpdateAsync(Examination model, Entities.Examination entity)
    {
      return Task.FromResult<bool>(true);
    }
  }
}
