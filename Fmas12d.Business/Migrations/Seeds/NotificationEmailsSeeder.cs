using Fmas12d.Data.Entities;
using System.Linq;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class NotificationEmailsSeeder : SeederBase<NotificationEmail>
  {
    #region Constants
    internal const string DESCRIPTION_MISSING_VSR_NUMBER =
      "Missing VSR number template";
    internal const string SUBJECT_MISSING_VSR_NUMBER =
      "Allocated to assessment description";
    internal const string MESSAGE_TEMPLATE_MISSING_VSR_NUMBER =
      "<html><head><title>Missing VSR number</title></head><body>Missing VSR number for CCG {0}</body></html>";
    internal const string NAME_MISSING_VSR_NUMBER = "Missing VSR number";
    #endregion

    internal void SeedData()
    {
      AddOrUpdate(
        Models.NotificationEmail.MISSING_VSR_NUMBER,
        NAME_MISSING_VSR_NUMBER,
        DESCRIPTION_MISSING_VSR_NUMBER,
        MESSAGE_TEMPLATE_MISSING_VSR_NUMBER,
        SUBJECT_MISSING_VSR_NUMBER
      );

      SaveChangesWithIdentity();
    }

    private void AddOrUpdate(int id, string name, string description, string template, string subject)
    {
      NotificationEmail notificationEmail;

      if ((notificationEmail = Context.NotificationEmails
          .SingleOrDefault(g => g.Id == id)) == null)
      {
        notificationEmail = new NotificationEmail
        {
          Id = id
        };
        Context.Add(notificationEmail);
      }
      notificationEmail.MessageTemplate = template;
      notificationEmail.SubjectTemplate = subject;

      PopulateNameDescriptionAndActiveAndModifiedWithSystemUser(
        notificationEmail,
        name,
        description
      );
    }
  }
}