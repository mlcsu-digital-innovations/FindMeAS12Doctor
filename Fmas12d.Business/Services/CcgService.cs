using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Exceptions;
using Fmas12d.Business.Extensions;
using Fmas12d.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Fmas12d.Business.Services
{
  public class CcgService :
    ServiceBase<Entities.Ccg>,
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

    public async Task<int> GetIdFromShortCode(string shortCode)
    {
      if (string.IsNullOrWhiteSpace(shortCode))
      {
        throw new ArgumentException("shortCode is null or white space");
      }

      int? ccgId = await _context.Ccgs
        .Where(c => c.ShortCode == shortCode)
        .Select(c => c.Id)
        .SingleOrDefaultAsync();

      if (ccgId == null)
      {
        throw new EntityNotFoundException("Ccg", "ShortCode", shortCode);
      }
      else
      {
        return ccgId.Value;
      }
    }
  }
}