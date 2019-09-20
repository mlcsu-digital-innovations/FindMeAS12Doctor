using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mep.Business.Extensions;
using Mep.Business.Models.SearchModels;
using System.Linq;
using Mep.Business.Exceptions;
using Mep.Business.Models;
using System.Linq.Expressions;

namespace Mep.Business.Services
{
  public class GpPracticeSearchService : GeneralSearchServiceBase, IModelGeneralSearchService<GpPractice>
  {
    public GpPracticeSearchService(ApplicationContext context, IMapper mapper)
      : base("GpPractice", context, mapper)
    {
    }

    public override async Task<IEnumerable<GeneralSearchResult>> SearchAsync(string searchString)
    {
      if (string.IsNullOrWhiteSpace(searchString))
      {
        throw new MissingSearchParameterException();
      }
      else
      {

        IQueryable<Data.Entities.GpPractice> query = _context.GpPractices.WhereIsActiveOrActiveOnly(true);

        string[] searchParams = searchString.Split(' ');

        foreach (string searchParam in searchParams) {
          query = query.Where(gp => gp.Name.Contains(searchParam) || gp.Postcode.Contains(searchParam));
        }

        IEnumerable<GeneralSearchResult> entities =
          await query.Select(gp => new GeneralSearchResult()
          {
            Id = gp.Id,
            ResultText = $"{gp.Name}, {gp.Postcode}"
          })
          .ToListAsync();

        return entities;
      }
    }
  }
}