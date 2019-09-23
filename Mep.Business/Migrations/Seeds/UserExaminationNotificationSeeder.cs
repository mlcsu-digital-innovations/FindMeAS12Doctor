using System.Linq;
using Mep.Data.Entities;

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
      Examination examination6 = _context.Examinations.Where(examination => examination.Address1 == "Examination Address 6").FirstOrDefault();
      Examination examination7 = _context.Examinations.Where(examination => examination.Address1 == "Examination Address 7").FirstOrDefault();
      NotificationText notificationText1 = _context.NotificationTexts.Where(notificationText => notificationText.Name == "Notification Text 1").FirstOrDefault();
      NotificationText notificationText2 = _context.NotificationTexts.Where(notificationText => notificationText.Name == "Notification Text 2").FirstOrDefault();

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
      userExaminationNotification.NotificationTextId = notificationText1.Id;
      userExaminationNotification.RespondedAt = _now;
      userExaminationNotification.UserId = 3;

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
      userExaminationNotification.NotificationTextId = notificationText2.Id;
      userExaminationNotification.RespondedAt = _now;
      userExaminationNotification.UserId = 2;
    }
  }
}