using Fmas12d.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public interface IUserService : IServiceBase
  {
    Task<User> CheckIsAmhpAsync(
      int id, 
      string modelPropertyName,
      bool asNoTracking = true, 
      bool activeOnly = true
    );
    Task<User> CheckIsADoctorAsync(
      int id, 
      string modelPropertyName,
      bool asNoTracking = true, 
      bool activeOnly = true
    );
    Task<IEnumerable<Models.User>> GetAllByAmhpNameAsync(
      string amhpName, bool asNoTracking = true, bool activeOnly = true);
    Task<IEnumerable<Models.User>> GetAllByDoctorNameAsync(
      string doctorName, bool asNoTracking = true, bool activeOnly = true);
    Task<IEnumerable<Models.User>> GetAllByGmcNumberAsync(
      int gmcNumber, bool asNoTracking = true, bool activeOnly = true);
    Task<User> GetByIdentityServerIdentifierAsync(
      string identityServerIdentifier, bool asNoTracking = true, bool activeOnly = true);
    Task<int> GetByProfileTypeIdAsync(int userId, bool asNoTracking, bool activeOnly);
  }
}