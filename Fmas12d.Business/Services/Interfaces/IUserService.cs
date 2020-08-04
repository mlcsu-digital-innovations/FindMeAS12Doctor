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
    Task<User> CreateAsync(User model);
    Task<IEnumerable<User>> GetAllByAmhpNameAsync(
      string amhpName, 
      bool asNoTracking = true, 
      bool activeOnly = true
    );
    Task<IEnumerable<User>> GetAllByDoctorNameAsync(
      string doctorName, 
      bool asNoTracking = true, 
      bool activeOnly = true,
      bool includeUnregisteredDoctors = false
    );
    Task<IEnumerable<User>> GetAllByGmcNumberAsync(
      int gmcNumber, 
      bool asNoTracking = true, 
      bool activeOnly = true,
      bool includeUnregisteredDoctors = false
    );
    Task<User> GetAsync(int id, bool asNoTracking, bool activeOnly);      
    Task<User> GetByIdentityServerIdentifierAsync(
      string identityServerIdentifier, bool asNoTracking = true, bool activeOnly = true);
    Task<int> GetByProfileTypeIdAsync(int userId, bool asNoTracking, bool activeOnly);
    Task<User> GetS12Async(int id, bool asNoTracking, bool activeOnly);     

    Task<bool> RefreshFcmToken(int userId, string token);

    Task<User> UpdateAsync(IUserProfileUpdate model);

    Task<User> UpdateVsrNumberAsync(VsrNumberUpdate model);

    Task<string> InviteUserToGroup(string UserEmailAddress);
  }
}