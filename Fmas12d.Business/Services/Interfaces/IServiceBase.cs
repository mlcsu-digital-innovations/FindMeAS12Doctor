using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public interface IServiceBase
  {
    Task<int> ActivateAsync(int id);
    Task<int> DeactivateAsync(int id);
  }
}