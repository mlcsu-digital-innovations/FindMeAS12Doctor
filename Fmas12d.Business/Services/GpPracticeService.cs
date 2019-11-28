using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Fmas12d.Business.Models;
using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Extensions;
using Fmas12d.Business.Exceptions;
using System.Linq;

// TODO CONVERT TO NO AUTOMAPPER

namespace Fmas12d.Business.Services
{
  public class GpPracticeService : 
      ServiceBase<GpPractice, Entities.GpPractice>, 
      IGpPracticeService,
      ISearchService
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

    public async Task<int?> GetCcgIdById(int id)
    {

      GpPractice gpPractice = await _context.GpPractices
                                            .Where(g => g.Id == id)
                                            .WhereIsActiveOrActiveOnly(true)
                                            .AsNoTracking(true)
                                            .Select(g => new GpPractice{
                                              CcgId = g.CcgId
                                            })
                                            .SingleOrDefaultAsync();

      if (gpPractice == null)
      {
        throw new ModelStateException("GpPracticeId",
          $"An active GP Practice with an Id of {id} does not exist.");
      }                      

      return gpPractice.CcgId;
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