using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Models;
using Fmas12d.Business.Extensions;
using Fmas12d.Business.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public class GpPracticeService : 
      ServiceBase<Entities.GpPractice>, 
      IGpPracticeService,
      ISearchService
  {
    public GpPracticeService(
      ApplicationContext context,
      IUserClaimsService userClaimsService)
      : base(context, userClaimsService)
    {
    }

    public async Task<int?> GetCcgIdById(int id)
    {

      GpPractice gpPractice = await _context
        .GpPractices
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