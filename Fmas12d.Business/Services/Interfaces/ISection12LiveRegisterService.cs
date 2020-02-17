using System.Threading.Tasks;
using Fmas12d.Business.Models;

namespace Fmas12d.Business.Services
{
  public interface ISection12LiveRegisterService: IServiceBase
  {
    Task<Section12LiveRegister> GetByGmcNumber(
      int gmcNumber,
      bool activeOnly,
      bool asNoTracking
    );
    Task<Section12LiveRegisterEtl> PerformEtlAsync(string filePath);
  }
}