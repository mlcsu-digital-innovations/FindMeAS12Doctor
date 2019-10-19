using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class NotificationTextsSeeder : SeederBase<NotificationText>
  {

    #region Constants
    internal const string DESCRIPTION_ALLOCATED_TO_EXAMINATION =
      "Allocated to examination description";
    internal const string DESCRIPTION_EXAMINATION_CANCELLED =
      "Examination cancelled description";
    internal const string MESSAGE_TEMPLATE_ALLOCATED_TO_EXAMINATION =
      "Allocated to examination {0} at {1} template";
    internal const string MESSAGE_TEMPLATE_EXAMINATION_CANCELLED =
      "Examination {0} at {1} cancelled template";
    internal const string NAME_ALLOCATED_TO_EXAMINATION =
      "Allocated to examination";
    internal const string NAME_EXAMINATION_CANCELLED = "Examination Cancelled";
    #endregion

    internal void SeedData()
    {
      AddOrUpdate(
        NAME_ALLOCATED_TO_EXAMINATION,
        DESCRIPTION_ALLOCATED_TO_EXAMINATION,
        MESSAGE_TEMPLATE_ALLOCATED_TO_EXAMINATION
      );

      AddOrUpdate(
        NAME_EXAMINATION_CANCELLED,
        DESCRIPTION_EXAMINATION_CANCELLED,
        MESSAGE_TEMPLATE_EXAMINATION_CANCELLED
      );
    }

    private void AddOrUpdate(string name, string description, string template)
    {
      NotificationText notificationText;

      if ((notificationText = _context.NotificationTexts
          .SingleOrDefault(g => g.Name == name)) == null)
      {
        notificationText = new NotificationText();
        _context.Add(notificationText);
      }
      notificationText.MessageTemplate = template;
      PopulateNameDescriptionAndActiveAndModifiedWithSystemUser(
        notificationText,
        name,
        description
      );
    }
  }
}