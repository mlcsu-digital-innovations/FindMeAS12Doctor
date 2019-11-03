using Fmas12d.Business.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Fmas12d.Business.Models.SearchModels;

namespace Fmas12d.Business.Services
{
  public interface IModelSearchService<T, S> : IModelService<T> where T : BaseModel where S : BaseSearchModel
  {
    Task<IEnumerable<T>> SearchAsync(S searchModel);
  }
}