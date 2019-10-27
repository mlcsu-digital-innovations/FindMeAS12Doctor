using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mep.Business.Models;
using Entities = Mep.Data.Entities;
using Mep.Business.Extensions;
using Mep.Business.Exceptions;
using System.Linq;

namespace Mep.Business.Services
{
  public class GpPracticeService
    : ServiceBase<GpPractice, Entities.GpPractice>, 
      IGpPracticeService
  {
    public GpPracticeService(
      ApplicationContext context,
      IMapper mapper)
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
      return await GetEntityWithNoIncludesByIdAsync(entityId, asNoTracking, activeOnly);
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
        IQueryable<Data.Entities.GpPractice> query = 
          _context.GpPractices.WhereIsActiveOrActiveOnly(isActiveOrActiveOnly);

        string[] searchParams = criteria.Split(' ');

        foreach (string searchParam in searchParams) {
          query = 
            query.Where(gp => gp.Name.Contains(searchParam) ||
                        gp.Postcode.Contains(searchParam));
        }

        IEnumerable<IdResultText> entities =
          await query.Select(e => new IdResultText(e)).ToListAsync();

        return entities;
      }
    }    
  }
}