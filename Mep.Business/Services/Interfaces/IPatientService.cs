using System.Collections.Generic;
using System.Threading.Tasks;
using Mep.Business.Models;

namespace Mep.Business.Services
{
  public interface IPatientService : IModelService<Models.Patient>
  {
    Task<Patient> GetByAlternativeIdentifier(
      string alternativeIdentifier, 
      bool asNoTracking, 
      bool activeOnly);
    Task<Patient> GetByNhsNumber(long nhsNumber, bool asNoTracking, bool activeOnly);
  }
}