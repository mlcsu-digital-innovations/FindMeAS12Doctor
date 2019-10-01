using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class NotificationTextsSeeder : SeederBase
  {
    internal NotificationTextsSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      NotificationText notificationText;

      if ((notificationText = _context.NotificationTexts.SingleOrDefault(g => g.Name == NOTIFICATION_TEXT_NAME_1)) == null)
      {
        notificationText = new NotificationText();
        _context.Add(notificationText);
      }
      notificationText.Description = NOTIFICATION_TEXT_DESCRIPTION_1;
      notificationText.IsActive = true;
      notificationText.MessageTemplate = NOTIFICATION_TEXT_MESSAGE_TEMPLATE_1;
      notificationText.ModifiedAt = _now;
      notificationText.ModifiedByUser = GetSystemAdminUser();
      notificationText.Name = NOTIFICATION_TEXT_NAME_1;

      if ((notificationText = _context.NotificationTexts.SingleOrDefault(g => g.Name == NOTIFICATION_TEXT_NAME_2)) == null)
      {
        notificationText = new NotificationText();
        _context.Add(notificationText);
      }
      notificationText.Description = NOTIFICATION_TEXT_DESCRIPTION_2;
      notificationText.IsActive = true;
      notificationText.MessageTemplate = NOTIFICATION_TEXT_MESSAGE_TEMPLATE_2;
      notificationText.ModifiedAt = _now;
      notificationText.ModifiedByUser = GetSystemAdminUser();
      notificationText.Name = NOTIFICATION_TEXT_NAME_2;
    }
  }
}