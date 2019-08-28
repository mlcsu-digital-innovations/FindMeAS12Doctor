using System.Collections.Generic;
using System.Threading.Tasks;
using Mep.Business.Models;
using Mep.Business.Models.SearchModels;

namespace Mep.Business.Services
{
    public interface IModelSimpleSearchService<T, S> where T : BaseModel where S : BaseSearchModel
    {
        Task<IEnumerable<T>> SearchAsync(S searchModel); 
    }
}