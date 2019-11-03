using Fmas12d.Data.Entities;
using System;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class UserAssessmentNotificationSeeder : SeederBase<UserAssessmentNotification>
  {
    internal UserAssessmentNotification Create(
      int notificationTextId,
      string userName,
      bool? hasAccepted = null,
      DateTimeOffset? respondedAt = null
    )
    {
      UserAssessmentNotification userAssessmentNotification = new UserAssessmentNotification
      {
        HasAccepted = hasAccepted == null ? null : hasAccepted,
        NotificationTextId = notificationTextId,
        RespondedAt = respondedAt == null ? null : respondedAt,
        UserId = GetUserByDisplayName(userName).Id
      };
      PopulateActiveAndModifiedWithSystemUser(userAssessmentNotification);

      return userAssessmentNotification;
    }
  }
}
