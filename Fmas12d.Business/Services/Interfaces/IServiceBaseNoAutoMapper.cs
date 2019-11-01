using System.Threading.Tasks;

namespace Mep.Business.Services
{
  public interface IServiceBaseNoAutoMapper
  {
    Task<int> ActivateAsync(int id);
    Task<int> DeactivateAsync(int id);
  }
}