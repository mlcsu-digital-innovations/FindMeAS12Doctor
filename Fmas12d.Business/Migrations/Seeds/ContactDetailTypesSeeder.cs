using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class ContactDetailTypesSeeder : SeederBase<ContactDetailType>
  {
    #region Constants
    protected const string DESCRIPTION_BASE = "Base Description";
    protected const string DESCRIPTION_HOME = "Home Description";
    protected const string NAME_BASE = "Base";
    protected const string NAME_HOME = "Home";
    
    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.ContactDetailType.BASE,
        NAME_BASE,
        DESCRIPTION_BASE
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ContactDetailType.HOME,
        NAME_HOME,
        DESCRIPTION_HOME
      );

      SaveChangesWithIdentity();
    }
  }
}