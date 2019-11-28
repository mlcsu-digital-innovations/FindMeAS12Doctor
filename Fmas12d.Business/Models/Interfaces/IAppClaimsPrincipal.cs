using System.Threading.Tasks;

namespace Fmas12d.Business.Models
{
  public interface IAppClaimsPrincipal
  {
    Task<User> GetCurrentUserAsync();
  }
}