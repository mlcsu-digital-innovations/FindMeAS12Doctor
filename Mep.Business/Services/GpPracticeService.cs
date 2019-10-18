using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mep.Business.Models;
using Entities = Mep.Data.Entities;
using Mep.Business.Extensions;

namespace Mep.Business.Services
{
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
      int entityId,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.GpPractice entity = await
        _context.GpPractices
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(gpPractice => gpPractice.Id == entityId);

      return entity;
    }

    protected override async Task<Entities.GpPractice> GetEntityWithNoIncludesByIdAsync(
      int entityId,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.GpPractice entity = await
        _context.GpPractices
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(gpPractice => gpPractice.Id == entityId);

      return entity;
    }
  }
}