using AutoMapper;
using Entities = Mep.Data.Entities;
using Mep.Business.Extensions;
using Mep.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mep.Business.Exceptions;
using System.Linq;

namespace Mep.Business.Services
{
  public class CcgService :
    ServiceBase<Ccg, Entities.Ccg>,
    ICcgService,
    ISearchService
  {
    public CcgService(ApplicationContext context, IMapper mapper)
      : base("Ccg", context, mapper)
    {
    }

    public async Task<IEnumerable<Models.Ccg>> GetAllAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.Ccg> entities =
        await _context.Ccgs
                      .WhereIsActiveOrActiveOnly(activeOnly)
                      .ToListAsync();

      IEnumerable<Models.Ccg> models =
        _mapper.Map<IEnumerable<Models.Ccg>>(entities);

      return models;
    }

    protected override async Task<Entities.Ccg> GetEntityByIdAsync(
      int entityId,
      bool asNoTracking,
      bool activeOnly)
    {
      return await GetEntityWithNoIncludesByIdAsync(entityId, asNoTracking, activeOnly);
    }

    protected override async Task<Entities.Ccg> GetEntityWithNoIncludesByIdAsync(
      int entityId,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.Ccg entity = await
        _context.Ccgs
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(ccg => ccg.Id == entityId);

      return entity;
    }

    public async Task<IEnumerable<IdResultText>> SearchAsync(
      string criteria,
      bool isActiveOrActiveOnly = true)
    {
      if (string.IsNullOrWhiteSpace(criteria))
      {
        throw new MissingSearchParameterException();
      }
      else
      {
        IQueryable<Data.Entities.Ccg> query = 
          _context.Ccgs.WhereIsActiveOrActiveOnly(isActiveOrActiveOnly);

        string[] searchParams = criteria.Split(' ');

        foreach (string searchParam in searchParams) {
          query = 
            query.Where(gp => gp.Name.Contains(searchParam));
        }

        IEnumerable<IdResultText> entities =
          await query.Select(e => new IdResultText(e)).ToListAsync();

        return entities;
      }
    }     
  }
}