using Fmas12d.Business.Models;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public interface IPatientService : IServiceBaseNoAutoMapper
  {
    Task<Patient> CreateAsync(Patient model);
    Task<Patient> GetByAlternativeIdentifier(
      string alternativeIdentifier, 
      bool asNoTracking = false, 
      bool activeOnly = false,
      bool onlyCurrentReferral = true);
    Task<Patient> GetByNhsNumber(
      long nhsNumber, 
      bool asNoTracking = false, 
      bool activeOnly = false,
      bool onlyCurrentReferral = true);
  }
}