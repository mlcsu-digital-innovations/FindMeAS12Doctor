using Fmas12d.Data.Entities;
using System.Linq;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class NotificationTextsSeeder : SeederBase<NotificationText>
  {

    #region Constants
    internal const string DESCRIPTION_ALLOCATED_TO_ASSESSMENT =
      "Allocated to assessment description";
    internal const string DESCRIPTION_ASSESSMENT_CANCELLED =
      "Assessment cancelled description";      
    internal const string DESCRIPTION_ASSESSMENT_UPDATED =
      "Assessment updated description";
    internal const string DESCRIPTION_NOT_ALLOCATED_TO_ASSESSMENT =
      "Not allocated to assessment description";
    internal const string DESCRIPTION_REMOVED_FROM_ASSESSMENT =
      "Removed from assessment description";          
    internal const string DESCRIPTION_SELECTED_FOR_ASSESSMENT =
      "Selected for assessment description";    

    internal const string MESSAGE_TEMPLATE_ALLOCATED_TO_ASSESSMENT =
      "Allocated to an assessment in {0} {1}";
    internal const string MESSAGE_TEMPLATE_ASSESSMENT_CANCELLED =
      "Assessment in {0} {1} has been cancelled";
    internal const string MESSAGE_TEMPLATE_ASSESSMENT_UPDATED =
      "Assessment in {0} {1} has been updated";
    internal const string MESSAGE_TEMPLATE_NOT_ALLOCATED_TO_ASSESSMENT =
      "Not allocated to an assessment in {0} {1}";
    internal const string MESSAGE_TEMPLATE_REMOVED_FROM_ASSESSMENT =
      "Removed from assessment in {0} {1}";
    internal const string MESSAGE_TEMPLATE_SELECTED_FOR_ASSESSMENT =
      "Selected for an assessment in {0} {1}";      

    internal const string NAME_ALLOCATED_TO_ASSESSMENT = "Allocated to assessment";
    internal const string NAME_ASSESSMENT_CANCELLED = "Assessment Cancelled";      
    internal const string NAME_ASSESSMENT_UPDATED = "Assessment Updated";
    internal const string NAME_NOT_ALLOCATED_TO_ASSESSMENT = "Not allocated to assessment";
    internal const string NAME_REMOVED_FROM_ASSESSMENT = "Removed from assessment";
    internal const string NAME_SELECTED_FOR_ASSESSMENT = "Selected for assessment";    
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

      AddOrUpdate(
        Models.NotificationText.ASSESSMENT_UPDATED,
        NAME_ASSESSMENT_UPDATED,
        DESCRIPTION_ASSESSMENT_UPDATED,
        MESSAGE_TEMPLATE_ASSESSMENT_UPDATED
      );      

      AddOrUpdate(
        Models.NotificationText.NOT_ALLOCATED_TO_ASSESSMENT,
        NAME_NOT_ALLOCATED_TO_ASSESSMENT,
        DESCRIPTION_NOT_ALLOCATED_TO_ASSESSMENT,
        MESSAGE_TEMPLATE_NOT_ALLOCATED_TO_ASSESSMENT
      );  

      AddOrUpdate(
        Models.NotificationText.REMOVED_FROM_ASSESSMENT,
        NAME_REMOVED_FROM_ASSESSMENT,
        DESCRIPTION_REMOVED_FROM_ASSESSMENT,
        MESSAGE_TEMPLATE_REMOVED_FROM_ASSESSMENT
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