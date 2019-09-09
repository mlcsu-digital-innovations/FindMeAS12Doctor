using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mep.Business.Extensions;
using Mep.Business.Models.SearchModels;
using System.Linq;
using Mep.Business.Exceptions;
using Mep.Business.Models;

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
      if (searchString != "")
      {
        IEnumerable<GeneralSearchResult> entities =
          await _context.GpPractices
          .WhereIsActiveOrActiveOnly(true)
          .Where(gp => gp.Name.Contains(searchString) || gp.Postcode.Contains(searchString))
          .Select(gp => new GeneralSearchResult()
          {
            Id = gp.Id,
            ResultText = $"{gp.Name}, {gp.Postcode}"
          })
          .ToListAsync();

        return entities;
      }
      else
      {
        throw new MissingSearchParameterException();
      }
    }
  }
}