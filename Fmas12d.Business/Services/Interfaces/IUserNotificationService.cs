using Fmas12d.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public interface IUserNotificationService : IServiceBase
  {
    Task<IEnumerable<UserAssessmentNotification>> Get(
      int userId, bool asNoTracking, bool activeOnly);

    Task<IEnumerable<UserAssessmentNotification>> SendAssessmentNotifications
    (
      IEnumerable<UserAssessmentNotification> notifications      
    );
    
    Task<UserAssessmentNotification> SendClaimNotification (
      Data.Entities.UserAssessmentClaim claim
    );

    Task<IEnumerable<UserAssessmentNotification>> SendClaimUpdateNotifications(
      IEnumerable<Data.Entities.UserAssessmentClaim> claims
    );
  }
}