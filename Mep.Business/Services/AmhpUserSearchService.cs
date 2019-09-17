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
  public class AmhpUserSearchService : GeneralSearchServiceBase, IModelGeneralSearchService<User>
  {
    public AmhpUserSearchService(ApplicationContext context, IMapper mapper)
      : base("User", context, mapper)
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
                  await _context.Users
                  .Include(user => user.ProfileType)
                  .WhereIsActiveOrActiveOnly(true)
                  .Where(user => user.DisplayName.Contains(searchString))
                  .Where(user => user.ProfileType.Name == "AMHP")
                  .Select(user => new GeneralSearchResult()
                  {
                    Id = user.Id,
                    ResultText = $"{user.DisplayName}"
                  })
                  .ToListAsync();

        return entities;
      }
    }
  }
}