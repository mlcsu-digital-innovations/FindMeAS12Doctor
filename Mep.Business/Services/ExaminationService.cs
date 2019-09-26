using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mep.Business.Models;
using Entities = Mep.Data.Entities;
using Mep.Business.Extensions;

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

    protected override async Task<Entities.Examination> GetEntityLinkedObjectsAsync(Examination model, Entities.Examination entity) {
        entity.CreatedByUser =await GetLinkedObjectAsync<Entities.User>(_context.Users, model.CreatedByUserId);
        return entity;
    }

    protected override async Task<Entities.Examination> GetEntityByIdAsync(
      int entityId,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.Examination entity = await
        _context.Examinations
                .Include(r => r.CreatedByUser)                
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(u => u.Id == entityId);

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
