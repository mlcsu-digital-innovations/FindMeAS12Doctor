using AutoMapper;
using Mep.Business.Exceptions;
using Mep.Business.Extensions;
using Mep.Business.Models.SearchModels;
using Mep.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mep.Business.Services
{
  public class CcgSearchService : GeneralSearchServiceBase, IModelGeneralSearchService<Ccg>
  {
    public CcgSearchService(ApplicationContext context, IMapper mapper)
      : base("Ccg", context, mapper)
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
        IEnumerable<GeneralSearchResult> entities =
                  await _context.Ccgs
                  .WhereIsActiveOrActiveOnly(true)
                  .Where(ccg => ccg.Name.Contains(searchString))
                  .Select(ccg => new GeneralSearchResult()
                  {
                    Id = ccg.Id,
                    ResultText = ccg.Name
                  })
                  .ToListAsync();

        return entities;
      }
    }
  }
}