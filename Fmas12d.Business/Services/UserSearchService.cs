using AutoMapper;
using Fmas12d.Business.Exceptions;
using Fmas12d.Business.Extensions;
using Fmas12d.Business.Models.SearchModels;
using Fmas12d.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public abstract class UserSearchService : 
    GeneralSearchServiceBase, IModelGeneralSearchService<User>
  {
    private readonly int _profileType;

    public UserSearchService(ApplicationContext context, IMapper mapper, int profileType)
      : base("User", context, mapper)
    {
      _profileType = profileType;
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
                  .Where(user => user.ProfileTypeId == _profileType)
                  .Select(user => new GeneralSearchResult()
                  {
                    Id = user.Id,
                    ResultText = user.DisplayName
                  })
                  .ToListAsync();
                  
        return entities;
      }
    }
  }
}