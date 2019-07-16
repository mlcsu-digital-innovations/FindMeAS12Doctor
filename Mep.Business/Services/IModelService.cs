using System.Collections.Generic;
using System.Threading.Tasks;
using Mep.Business.Models;

namespace Mep.Business.Services
{
    public interface IModelService<T> where T : BaseModel
    {
        Task<T> CreateAsync(T model);
        Task<int> DeactivateAsync(int Id);
        Task<T> GetByIdAsync(int Id, bool activeOnly);
        Task<IEnumerable<T>> GetAllAsync(bool activeOnly);
        Task<int> ActivateAsync(int Id);
        Task<T> UpdateAsync(T model);        
    }
}