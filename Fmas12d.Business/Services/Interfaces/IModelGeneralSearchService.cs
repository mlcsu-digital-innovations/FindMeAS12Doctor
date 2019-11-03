using System.Collections.Generic;
using System.Threading.Tasks;
using Fmas12d.Business.Models.SearchModels;
using Fmas12d.Business.Models;

namespace Fmas12d.Business.Services
{
  public interface IModelGeneralSearchService<T> where T : BaseModel
  {
    Task<IEnumerable<GeneralSearchResult>> SearchAsync(string searchString);
  }
}