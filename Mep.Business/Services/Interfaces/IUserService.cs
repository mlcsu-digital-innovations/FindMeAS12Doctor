using System.Collections.Generic;
using System.Threading.Tasks;
using Mep.Business.Models;

namespace Mep.Business.Services
{
  public interface IUserService : IServiceBaseNoAutoMapper
  {
    Task<IEnumerable<User>> GetAllAsync(bool activeOnly);
    Task<IEnumerable<Models.User>> GetAllByAmhpName(
      string amhpName, bool asNoTracking = true, bool activeOnly = true);
    Task<IEnumerable<Models.User>> GetAllByDoctorName(
      string doctorName, bool asNoTracking = true, bool activeOnly = true);
  }
}