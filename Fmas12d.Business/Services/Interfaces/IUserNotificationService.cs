using Fmas12d.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public interface IUserNotificationService : IServiceBaseNoAutoMapper
  {
    Task<IEnumerable<UserAssessmentNotification>> Get(
      int userId, bool asNoTracking, bool activeOnly);
  }
}