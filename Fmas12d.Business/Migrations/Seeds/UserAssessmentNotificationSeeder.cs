using Fmas12d.Data.Entities;
using System;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class UserExaminationNotificationSeeder : SeederBase<UserExaminationNotification>
  {
    internal UserExaminationNotification Create(
      int notificationTextId,
      string userName,
      bool? hasAccepted = null,
      DateTimeOffset? respondedAt = null
    )
    {
      UserExaminationNotification userExaminationNotification = new UserExaminationNotification
      {
        HasAccepted = hasAccepted == null ? null : hasAccepted,
        NotificationTextId = notificationTextId,
        RespondedAt = respondedAt == null ? null : respondedAt,
        UserId = GetUserByDisplayName(userName).Id
      };
      PopulateActiveAndModifiedWithSystemUser(userExaminationNotification);

      return userExaminationNotification;
    }
  }
}
