using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class NotificationTextsSeeder : SeederBase<NotificationText>
  {

    #region Constants
    internal const string DESCRIPTION_ALLOCATED_TO_EXAMINATION =
      "Allocated to examination description";
    internal const string DESCRIPTION_ASSIGNED_TO_EXAMINATION =
      "Assigned to examination description";      
    internal const string DESCRIPTION_EXAMINATION_CANCELLED =
      "Examination cancelled description";
    internal const string MESSAGE_TEMPLATE_ALLOCATED_TO_EXAMINATION =
      "Allocated to examination {0} at {1} template";
    internal const string MESSAGE_TEMPLATE_ASSIGNED_TO_EXAMINATION =
      "Assigned to examination {0} at {1} template";      
    internal const string MESSAGE_TEMPLATE_EXAMINATION_CANCELLED =
      "Examination {0} at {1} cancelled template";
    internal const string NAME_ALLOCATED_TO_EXAMINATION =
      "Allocated to examination";
    internal const string NAME_ASSIGNED_TO_EXAMINATION =
      "Assigned to examination";
    internal const string NAME_EXAMINATION_CANCELLED = "Examination Cancelled";
    #endregion

    internal void SeedData()
    {
      AddOrUpdate(
        Models.NotificationText.ASSIGNED_TO_EXAMINATION,
        NAME_ASSIGNED_TO_EXAMINATION,
        DESCRIPTION_ASSIGNED_TO_EXAMINATION,
        MESSAGE_TEMPLATE_ASSIGNED_TO_EXAMINATION
      );
            
      AddOrUpdate(
        Models.NotificationText.ALLOCATED_TO_EXAMINATION,
        NAME_ALLOCATED_TO_EXAMINATION,
        DESCRIPTION_ALLOCATED_TO_EXAMINATION,
        MESSAGE_TEMPLATE_ALLOCATED_TO_EXAMINATION
      );

      AddOrUpdate(
        Models.NotificationText.EXAMINATION_CANCELLED,
        NAME_EXAMINATION_CANCELLED,
        DESCRIPTION_EXAMINATION_CANCELLED,
        MESSAGE_TEMPLATE_EXAMINATION_CANCELLED
      );
      
    }

    private void AddOrUpdate(int id, string name, string description, string template)
    {
      NotificationText notificationText;

      if ((notificationText = Context.NotificationTexts
          .SingleOrDefault(g => g.Id == id)) == null)
      {
        notificationText = new NotificationText();
        Context.Add(notificationText);
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