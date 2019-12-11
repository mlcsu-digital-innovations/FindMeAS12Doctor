using System.Collections.Generic;
using System.Threading.Tasks;
using Fmas12d.Business.Models;

namespace Fmas12d.Business.Services
{
  public interface ISearchService : IServiceBase
  {
    Task<IEnumerable<IdResultText>> SearchAsync(
      string criteria, 
      bool isActiveOrActiveOnly = true);
  }
}