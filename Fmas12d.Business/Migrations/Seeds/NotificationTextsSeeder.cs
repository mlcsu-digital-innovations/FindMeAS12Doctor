using Fmas12d.Data.Entities;
using System.Linq;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class NotificationTextsSeeder : SeederBase<NotificationText>
  {

    #region Constants
    internal const string DESCRIPTION_ALLOCATED_TO_ASSESSMENT =
      "Allocated to assessment description";
    internal const string DESCRIPTION_SELECTED_FOR_ASSESSMENT =
      "Selected for assessment description";      
    internal const string DESCRIPTION_ASSESSMENT_CANCELLED =
      "Assessment cancelled description";
    internal const string MESSAGE_TEMPLATE_ALLOCATED_TO_ASSESSMENT =
      "Allocated to assessment {0} at {1} template";
    internal const string MESSAGE_TEMPLATE_SELECTED_FOR_ASSESSMENT =
      "Selected for assessment {0} at {1} template";      
    internal const string MESSAGE_TEMPLATE_ASSESSMENT_CANCELLED =
      "Assessment {0} at {1} cancelled template";
    internal const string NAME_ALLOCATED_TO_ASSESSMENT =
      "Allocated to assessment";
    internal const string NAME_SELECTED_FOR_ASSESSMENT =
      "Selected for assessment";
    internal const string NAME_ASSESSMENT_CANCELLED = "Assessment Cancelled";
    #endregion

    internal void SeedData()
    {
      AddOrUpdate(
        Models.NotificationText.SELECTED_FOR_ASSESSMENT,
        NAME_SELECTED_FOR_ASSESSMENT,
        DESCRIPTION_SELECTED_FOR_ASSESSMENT,
        MESSAGE_TEMPLATE_SELECTED_FOR_ASSESSMENT
      );
            
      AddOrUpdate(
        Models.NotificationText.ALLOCATED_TO_ASSESSMENT,
        NAME_ALLOCATED_TO_ASSESSMENT,
        DESCRIPTION_ALLOCATED_TO_ASSESSMENT,
        MESSAGE_TEMPLATE_ALLOCATED_TO_ASSESSMENT
      );

      AddOrUpdate(
        Models.NotificationText.ASSESSMENT_CANCELLED,
        NAME_ASSESSMENT_CANCELLED,
        DESCRIPTION_ASSESSMENT_CANCELLED,
        MESSAGE_TEMPLATE_ASSESSMENT_CANCELLED
      );

      SaveChangesWithIdentity();
    }

    private void AddOrUpdate(int id, string name, string description, string template)
    {
      NotificationText notificationText;

      if ((notificationText = Context.NotificationTexts
          .SingleOrDefault(g => g.Id == id)) == null)
      {
        notificationText = new NotificationText
        {
          Id = id
        };
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