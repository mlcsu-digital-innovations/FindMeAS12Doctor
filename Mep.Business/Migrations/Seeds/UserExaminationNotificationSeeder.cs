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
      Examination examination6 = _context.Examinations.Single(examination => examination.Address1 == "Examination Address 6");
      Examination examination7 = _context.Examinations.Single(examination => examination.Address1 == "Examination Address 7");

      // notification for referral with current examination and notification responses

      if ((userExaminationNotification =
        _context.UserExaminationNotifications
          .SingleOrDefault(g => g.ExaminationId == examination6.Id)) == null)
      {
        userExaminationNotification = new UserExaminationNotification();
        _context.Add(userExaminationNotification);
      }
      userExaminationNotification.ExaminationId = examination6.Id;
      userExaminationNotification.HasAccepted = true;
      userExaminationNotification.IsActive = true;
      userExaminationNotification.ModifiedAt = _now;
      userExaminationNotification.ModifiedByUser = GetSystemAdminUser();
      userExaminationNotification.NotificationTextId = GetNotificationTextId(NOTIFICATIONTEXT1);
      userExaminationNotification.RespondedAt = _now;
      userExaminationNotification.UserId = GetUserIdByDisplayname(USERDISPLAYNAMEFEMALE);

      // notification for referral with current examination and notification responses and allocated doctors

      if ((userExaminationNotification =
        _context.UserExaminationNotifications
          .SingleOrDefault(g => g.ExaminationId == examination7.Id)) == null)
      {
        userExaminationNotification = new UserExaminationNotification();
        _context.Add(userExaminationNotification);
      }
      userExaminationNotification.ExaminationId = examination7.Id;
      userExaminationNotification.HasAccepted = true;
      userExaminationNotification.IsActive = true;
      userExaminationNotification.ModifiedAt = _now;
      userExaminationNotification.ModifiedByUser = GetSystemAdminUser();
      userExaminationNotification.NotificationTextId = GetNotificationTextId(NOTIFICATIONTEXT2);
      userExaminationNotification.RespondedAt = _now;
      userExaminationNotification.UserId = GetUserIdByDisplayname(USERDISPLAYNAMEMALE);
    }
  }
}