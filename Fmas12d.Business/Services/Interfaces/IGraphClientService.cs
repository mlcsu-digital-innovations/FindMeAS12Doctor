using System.Threading.Tasks;
using Microsoft.Graph;

namespace Fmas12d.Business.Services
{
  public interface IGraphClientService
  {

    Task<GraphServiceClient> GetGraphServiceClient();

    Task<DelegateAuthenticationProvider> GetAuthProvider();
  }
}