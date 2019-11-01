using System.Collections.Generic;
using System.Threading.Tasks;
using Fmas12d.Business.Models;

namespace Fmas12d.Business.Services
{
  public interface IUserService : IServiceBaseNoAutoMapper
  {
    Task<bool> CheckUserIsAnAmhpById(int id, bool asNoTracking = true, bool activeOnly = true);
    Task<IEnumerable<User>> GetAllAsync(bool activeOnly);
    Task<IEnumerable<Models.User>> GetAllByAmhpName(
      string amhpName, bool asNoTracking = true, bool activeOnly = true);
    Task<IEnumerable<Models.User>> GetAllByDoctorName(
      string doctorName, bool asNoTracking = true, bool activeOnly = true);
  }
}