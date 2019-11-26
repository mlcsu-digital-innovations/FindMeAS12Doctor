using Fmas12d.Data.Entities;
using System;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class UserAssessmentNotificationSeeder : SeederBase<UserAssessmentNotification>
  {
    internal UserAssessmentNotification Create(
      int notificationTextId,
      string userName,
      DateTimeOffset? sentAt = null
    )
    {
      UserAssessmentNotification userAssessmentNotification = new UserAssessmentNotification
      {
        NotificationTextId = notificationTextId,
        SentAt = sentAt == null ? null : sentAt,
        UserId = GetUserByDisplayName(userName).Id
      };
      PopulateActiveAndModifiedWithSystemUser(userAssessmentNotification);

      return userAssessmentNotification;
    }
  }
}
