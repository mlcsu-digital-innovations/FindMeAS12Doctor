using System.Collections.Generic;
using System.Threading.Tasks;
using Mep.Business.Models;

namespace Mep.Business.Services
{
    public interface ISpecialityService
    {
        Task<Speciality> CreateSpecialityAsync(Speciality SpecialityBusinessModel);
        Task<int> DeactivateAsync(int Id);
        Task<Models.Speciality> GetModelByIdAsync(int Id, bool activeOnly);
        Task<IEnumerable<Models.Speciality>> GetSpecialitiesAsync(bool activeOnly);
        Task<int> UndeleteSpecialityAsync(int Id);
        Task<Speciality> UpdateSpecialityAsync(Speciality SpecialityBusinessModel);        
    }
}