using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class NotificationTextsSeeder : SeederBase<NotificationText>
  {
    internal void SeedData()
    {
      NotificationText notificationText;

      if ((notificationText = _context
        .NotificationTexts
          .SingleOrDefault(g => g.Name ==
            NOTIFICATION_TEXT_NAME_ALLOCATED_TO_EXAMINATION)) == null)
      {
        notificationText = new NotificationText();
        _context.Add(notificationText);
      }
      notificationText.Description =
        NOTIFICATION_TEXT_DESCRIPTION_ALLOCATED_TO_EXAMINATION;
      notificationText.IsActive = true;
      notificationText.MessageTemplate =
        NOTIFICATION_TEXT_MESSAGE_TEMPLATE_ALLOCATED_TO_EXAMINATION;
      notificationText.ModifiedAt = _now;
      notificationText.ModifiedByUser = GetSystemAdminUser();
      notificationText.Name = NOTIFICATION_TEXT_NAME_ALLOCATED_TO_EXAMINATION;

      if ((notificationText = _context
        .NotificationTexts
          .SingleOrDefault(g => g.Name ==
            NOTIFICATION_TEXT_NAME_EXAMINATION_CANCELLED)) == null)
      {
        notificationText = new NotificationText();
        _context.Add(notificationText);
      }
      notificationText.Description =
        NOTIFICATION_TEXT_DESCRIPTION_EXAMINATION_CANCELLED;
      notificationText.IsActive = true;
      notificationText.MessageTemplate =
        NOTIFICATION_TEXT_MESSAGE_TEMPLATE_EXAMINATION_CANCELLED;
      notificationText.ModifiedAt = _now;
      notificationText.ModifiedByUser = GetSystemAdminUser();
      notificationText.Name = NOTIFICATION_TEXT_NAME_EXAMINATION_CANCELLED;
    }
  }
}