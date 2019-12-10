using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Exceptions;
using Fmas12d.Business.Extensions;
using Fmas12d.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Fmas12d.Business.Services
{
  public class CcgService :
    ServiceBaseNoAutoMapper<Entities.Ccg>,
    ICcgService,
    ISearchService
  {
    public CcgService(ApplicationContext context,
      IUserClaimsService userClaimsService)
      : base(context, userClaimsService)
    {
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
        IQueryable<Entities.Ccg> query = 
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