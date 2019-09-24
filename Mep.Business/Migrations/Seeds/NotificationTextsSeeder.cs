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

      if ((notificationText =
        _context.NotificationTexts
          .SingleOrDefault(g => g.Name == "Notification Text 1")) == null)
      {
        notificationText = new NotificationText();
        _context.Add(notificationText);
      }
      notificationText.Description = "Notification Text 1";
      notificationText.IsActive = true;
      notificationText.MessageTemplate = "Notification Text 1";
      notificationText.ModifiedAt = _now;
      notificationText.ModifiedByUser = GetSystemAdminUser();
      notificationText.Name = "Notification Text 1";

      if ((notificationText =
        _context.NotificationTexts
          .SingleOrDefault(g => g.Name == "Notification Text 2")) == null)
      {
        notificationText = new NotificationText();
        _context.Add(notificationText);
      }
      notificationText.Description = "Notification Text 2";
      notificationText.IsActive = true;
      notificationText.MessageTemplate = "Notification Text 2";
      notificationText.ModifiedAt = _now;
      notificationText.ModifiedByUser = GetSystemAdminUser();
      notificationText.Name = "Notification Text 2";
    }
  }
}