using System;
using System.Linq;
using Mep.Data.Entities;

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
      DateTimeOffset now = DateTimeOffset.Now;

      if ((notificationText =
        _context.NotificationTexts
          .SingleOrDefault(g => g.Name == "Notification Text 1")) == null)
      {
        notificationText = new NotificationText();
        _context.Add(notificationText);
      }
      notificationText.Description = "Notification Text 1";
      notificationText.IsActive = true;
      notificationText.MessageTemplate = null;
      notificationText.ModifiedAt = now;
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
      notificationText.MessageTemplate = null;
      notificationText.ModifiedAt = now;
      notificationText.ModifiedByUser = GetSystemAdminUser();
      notificationText.Name = "Notification Text 2";
    }
  }
}