using Mep.Business.Models;
using System.Threading.Tasks;

namespace Mep.Business.Services
{
  public interface IPatientService : IServiceBaseNoAutoMapper
  {
    Task<Patient> CreateAsync(Patient model);
    Task<Patient> GetByAlternativeIdentifier(
      string alternativeIdentifier, 
      bool asNoTracking = false, 
      bool activeOnly = false);
    Task<Patient> GetByNhsNumber(
      long nhsNumber, 
      bool asNoTracking = false, 
      bool activeOnly = false);
  }
}