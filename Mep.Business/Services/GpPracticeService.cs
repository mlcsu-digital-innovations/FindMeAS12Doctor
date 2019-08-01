using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mep.Business.Models;
using Entities = Mep.Data.Entities;
using Mep.Business.Extensions;

namespace Mep.Business.Services
{
  //TODO - include CCG details in query, will report self referencing loop if mapping not corrected

  public class GpPracticeService
    : ServiceBase<GpPractice, Entities.GpPractice>, IModelService<GpPractice>
  {
    public GpPracticeService(ApplicationContext context, IMapper mapper)
      : base("GpPractice", context, mapper)
    {
    }

    public async Task<IEnumerable<Models.GpPractice>> GetAllAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.GpPractice> entities =
        await _context.GpPractices
                      .WhereIsActiveOrActiveOnly(activeOnly)
                      .ToListAsync();

      IEnumerable<Models.GpPractice> models =
        _mapper.Map<IEnumerable<Models.GpPractice>>(entities);

      return models;
    }

    protected override async Task<Entities.GpPractice> GetEntityByIdAsync(
      int id,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.GpPractice entity = await
        _context.GpPractices
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(u => u.Id == id);

      return entity;
    }

    protected override Task<Entities.GpPractice> GetEntityLinkedObjectsAsync(GpPractice model, Entities.GpPractice entity)
    {
      return Task.FromResult(entity);
    }

    protected override Task<bool> InternalCreateAsync(GpPractice model, Entities.GpPractice entity)
    {
      return Task.FromResult<bool>(true);
    }

    protected override Task<bool> InternalUpdateAsync(GpPractice model, Entities.GpPractice entity)
    {
      return Task.FromResult<bool>(true);
    }
  }
}