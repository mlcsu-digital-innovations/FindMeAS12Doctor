using Fmas12d.Business.Models;

namespace Fmas12d.Business.Services
{
  public interface ISection12LiveRegisterService: IServiceBase
  {
    Section12LiveRegisterEtl PerformEtl(string filePath);
  }
}