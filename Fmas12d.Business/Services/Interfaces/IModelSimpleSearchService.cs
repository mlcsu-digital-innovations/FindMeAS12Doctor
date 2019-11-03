using System.Collections.Generic;
using System.Threading.Tasks;
using Fmas12d.Business.Models;
using Fmas12d.Business.Models.SearchModels;

namespace Fmas12d.Business.Services
{
    public interface IModelSimpleSearchService<T, S> where T : BaseModel where S : BaseSearchModel
    {
        Task<IEnumerable<T>> SearchAsync(S searchModel); 
    }
}