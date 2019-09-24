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
          .SingleOrDefault(g => g.Name == NOTIFICATIONTEXT1)) == null)
      {
        notificationText = new NotificationText();
        _context.Add(notificationText);
      }
      notificationText.Description = NOTIFICATIONTEXT1;
      notificationText.IsActive = true;
      notificationText.MessageTemplate = NOTIFICATIONTEXT1;
      notificationText.ModifiedAt = _now;
      notificationText.ModifiedByUser = GetSystemAdminUser();
      notificationText.Name = NOTIFICATIONTEXT1;

      if ((notificationText =
        _context.NotificationTexts
          .SingleOrDefault(g => g.Name == NOTIFICATIONTEXT2)) == null)
      {
        notificationText = new NotificationText();
        _context.Add(notificationText);
      }
      notificationText.Description = NOTIFICATIONTEXT2;
      notificationText.IsActive = true;
      notificationText.MessageTemplate = NOTIFICATIONTEXT2;
      notificationText.ModifiedAt = _now;
      notificationText.ModifiedByUser = GetSystemAdminUser();
      notificationText.Name = NOTIFICATIONTEXT2;
    }
  }
}