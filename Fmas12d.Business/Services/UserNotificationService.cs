using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Extensions;
using Fmas12d.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public class UserNotificationService
   : ServiceBaseNoAutoMapper<Entities.UserAssessmentNotification>, IUserNotificationService
  {    
    public UserNotificationService(
      ApplicationContext context,
      IUserClaimsService userClaimsService)
      : base(context, userClaimsService)
    {
      
    }

    public async Task<IEnumerable<UserAssessmentNotification>> Get(
      int userId, bool asNoTracking, bool activeOnly)
    {
      IEnumerable<UserAssessmentNotification> notifications = await _context
        .UserAssessmentNotifications
        .Where(uan => uan.UserId == userId)
        .WhereIsActiveOrActiveOnly(activeOnly)
        .AsNoTracking(asNoTracking)
        .Select(UserAssessmentNotification.ProjectFromEntity)
        .ToListAsync();

      return notifications;
    }
  }
}