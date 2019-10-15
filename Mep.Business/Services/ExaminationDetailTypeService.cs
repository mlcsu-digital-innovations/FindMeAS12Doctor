using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mep.Business.Models;
using Entities = Mep.Data.Entities;
using Mep.Business.Extensions;

namespace Mep.Business.Services
{
  public class ExaminationDetailTypeService
    : ServiceBase<ExaminationDetailType, Entities.ExaminationDetailType>, IModelService<ExaminationDetailType>
  {
    public ExaminationDetailTypeService(ApplicationContext context, IMapper mapper)
      : base("ExaminationDetailType", context, mapper)
    {
    }

    public async Task<IEnumerable<Models.ExaminationDetailType>> GetAllAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.ExaminationDetailType> entities =
        await _context.ExaminationDetailTypes
                      .WhereIsActiveOrActiveOnly(activeOnly)
                      .ToListAsync();

      IEnumerable<Models.ExaminationDetailType> models =
        _mapper.Map<IEnumerable<Models.ExaminationDetailType>>(entities);

      return models;
    }

    protected override async Task<Entities.ExaminationDetailType> GetEntityByIdAsync(
      int entityId,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.ExaminationDetailType entity = await
        _context.ExaminationDetailTypes
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(ExaminationDetailType => ExaminationDetailType.Id == entityId);

      return entity;
    }

    protected override Task<Entities.ExaminationDetailType> GetEntityLinkedObjectsAsync(ExaminationDetailType model, Entities.ExaminationDetailType entity)
    {
      return Task.FromResult(entity);
    }

    protected override Task<bool> InternalCreateAsync(ExaminationDetailType model, Entities.ExaminationDetailType entity)
    {
      return Task.FromResult<bool>(true);
    }

    protected override Task<bool> InternalUpdateAsync(ExaminationDetailType model, Entities.ExaminationDetailType entity)
    {
      return Task.FromResult<bool>(true);
    }
  }
}