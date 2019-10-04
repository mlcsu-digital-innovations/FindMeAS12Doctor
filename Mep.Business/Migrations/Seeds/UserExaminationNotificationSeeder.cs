using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class UserExaminationNotificationSeeder : SeederBase
  {

    internal UserExaminationNotificationSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      UserExaminationNotification userExaminationNotification;

      // notification for referral with current examination and notification responses

      if ((userExaminationNotification = _context
        .UserExaminationNotifications
          .SingleOrDefault(g => g.ExaminationId == GetExaminationIdByExaminationAddress(EXAMINATION_ADDRESS_6)))
            == null)
      {
        userExaminationNotification = new UserExaminationNotification();
        _context.Add(userExaminationNotification);
      }
      userExaminationNotification.ExaminationId = GetExaminationIdByExaminationAddress(EXAMINATION_ADDRESS_6);
      userExaminationNotification.HasAccepted = true;
      userExaminationNotification.IsActive = true;
      userExaminationNotification.ModifiedAt = _now;
      userExaminationNotification.ModifiedByUser = GetSystemAdminUser();
      userExaminationNotification.NotificationTextId = GetNotificationTextId(NOTIFICATION_TEXT_NAME_1);
      userExaminationNotification.RespondedAt = _now;
      userExaminationNotification.UserId = GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_FEMALE);

      // notification for referral with current examination and notification responses and allocated doctors

      if ((userExaminationNotification = _context
        .UserExaminationNotifications
          .SingleOrDefault(g => g.ExaminationId == GetExaminationIdByExaminationAddress(EXAMINATION_ADDRESS_7)))
            == null)
      {
        userExaminationNotification = new UserExaminationNotification();
        _context.Add(userExaminationNotification);
      }
      userExaminationNotification.ExaminationId = GetExaminationIdByExaminationAddress(EXAMINATION_ADDRESS_7);
      userExaminationNotification.HasAccepted = true;
      userExaminationNotification.IsActive = true;
      userExaminationNotification.ModifiedAt = _now;
      userExaminationNotification.ModifiedByUser = GetSystemAdminUser();
      userExaminationNotification.NotificationTextId = GetNotificationTextId(NOTIFICATION_TEXT_NAME_2);
      userExaminationNotification.RespondedAt = _now;
      userExaminationNotification.UserId = GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_MALE);
    }
  }
}