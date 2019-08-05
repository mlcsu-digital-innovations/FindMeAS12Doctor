using Mep.Business.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Mep.Business.Models.SearchModels;

namespace Mep.Business.Services
{
    public interface IModelSearchService<T, S> : IModelService<T> where T : BaseModel where S : BaseSearchModel
    {
        // Task<IEnumerable<T>> SearchAsync<S>(S searchModel); 
        Task<IEnumerable<T>> SearchAsync(S searchModel); 
    }
}