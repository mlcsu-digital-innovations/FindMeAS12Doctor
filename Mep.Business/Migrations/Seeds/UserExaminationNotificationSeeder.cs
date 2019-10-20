using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class UserExaminationNotificationSeeder : SeederBase<UserExaminationNotification>
  {

    // internal void SeedData()
    // {
    //   UserExaminationNotification userExaminationNotification;

    //   // notification for referral with current examination and notification responses

    //   if ((userExaminationNotification = _context
    //     .UserExaminationNotifications
    //       .SingleOrDefault(g => g.ExaminationId ==
    //         GetExaminationIdByExaminationAddress(EXAMINATION_ADDRESS_6)))
    //           == null)
    //   {
    //     userExaminationNotification = new UserExaminationNotification();
    //     _context.Add(userExaminationNotification);
    //   }
    //   userExaminationNotification.ExaminationId =
    //     GetExaminationIdByExaminationAddress(EXAMINATION_ADDRESS_6);
    //   userExaminationNotification.HasAccepted = true;
    //   userExaminationNotification.IsActive = true;
    //   userExaminationNotification.ModifiedAt = _now;
    //   userExaminationNotification.ModifiedByUser = GetSystemAdminUser();
    //   userExaminationNotification.NotificationTextId =
    //     GetNotificationTextId(NotificationTextsSeeder.NAME_ALLOCATED_TO_EXAMINATION);
    //   userExaminationNotification.RespondedAt = _now;
    //   userExaminationNotification.UserId =
    //     GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_FEMALE).Id;

    //   // notification for referral with current examination and notification responses and allocated doctors

    //   if ((userExaminationNotification = _context
    //     .UserExaminationNotifications
    //       .SingleOrDefault(g => g.ExaminationId ==
    //         GetExaminationIdByExaminationAddress(EXAMINATION_ADDRESS_7)))
    //           == null)
    //   {
    //     userExaminationNotification = new UserExaminationNotification();
    //     _context.Add(userExaminationNotification);
    //   }
    //   userExaminationNotification.ExaminationId =
    //     GetExaminationIdByExaminationAddress(EXAMINATION_ADDRESS_7);
    //   userExaminationNotification.HasAccepted = true;
    //   userExaminationNotification.IsActive = true;
    //   userExaminationNotification.ModifiedAt = _now;
    //   userExaminationNotification.ModifiedByUser = GetSystemAdminUser();
    //   userExaminationNotification.NotificationTextId =
    //     GetNotificationTextId(NotificationTextsSeeder.NAME_EXAMINATION_CANCELLED);
    //   userExaminationNotification.RespondedAt = _now;
    //   userExaminationNotification.UserId =
    //     GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_MALE).Id;
    // }
  }
}