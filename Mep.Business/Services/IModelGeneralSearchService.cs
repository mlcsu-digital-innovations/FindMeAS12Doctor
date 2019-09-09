using System.Collections.Generic;
using System.Threading.Tasks;
using Mep.Business.Models.SearchModels;
using Mep.Business.Models;

namespace Mep.Business.Services
{
    public interface IModelGeneralSearchService<T> where T : BaseModel
    {
        Task<IEnumerable<GeneralSearchResult>> SearchAsync(string searchString); 
    }
}