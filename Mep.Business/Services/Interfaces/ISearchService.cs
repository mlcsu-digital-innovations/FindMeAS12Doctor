using System.Collections.Generic;
using System.Threading.Tasks;
using Mep.Business.Models;

namespace Mep.Business.Services
{
  public interface ISearchService : IServiceBaseNoAutoMapper
  {
    Task<IEnumerable<IdResultText>> SearchAsync(
      string criteria, 
      bool isActiveOrActiveOnly = true);
  }
}