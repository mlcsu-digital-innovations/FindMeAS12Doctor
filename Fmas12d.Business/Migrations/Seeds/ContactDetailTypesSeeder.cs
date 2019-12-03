using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class ContactDetailTypesSeeder : SeederBase<ContactDetailType>
  {
    #region Constants
    protected const string DESCRIPTION_HOME = "Home Description";
    protected const string DESCRIPTION_WORK = "Work Description";    
    protected const string NAME_HOME = "Home";
    protected const string NAME_WORK = "Work";
    
    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.ContactDetailType.HOME,
        NAME_HOME,
        DESCRIPTION_HOME
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ContactDetailType.WORK,
        NAME_WORK,
        DESCRIPTION_WORK
      );

      SaveChangesWithIdentity();
    }
  }
}