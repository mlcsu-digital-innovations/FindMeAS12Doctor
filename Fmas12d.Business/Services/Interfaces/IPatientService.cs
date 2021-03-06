using Fmas12d.Business.Models;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public interface IPatientService : IServiceBase
  {
    Task<bool> CheckExists(
      int id, 
      string modelPropertyName,
      bool asNoTracking = true, 
      bool activeOnly = true
    );    
    Task<Patient> CreateAsync(Patient model);
    Task<Patient> GetByAlternativeIdentifierAsync(
      string alternativeIdentifier, 
      bool asNoTracking = false, 
      bool activeOnly = false,
      bool onlyCurrentReferral = true);
    Task<Patient> GetByNhsNumberAsync(
      long nhsNumber, 
      bool asNoTracking = false, 
      bool activeOnly = false,
      bool onlyCurrentReferral = true);
    Task<Patient> UpdateAsync(Patient model);
  }
}