using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class ContactDetailTypesSeeder : SeederBase<ContactDetailType>
  {
    #region Constants      
    protected const string NAME_WORK = "Work";
    protected const string DESCRIPTION_WORK = "Work Description";
    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.ContactDetailType.WORK,
        NAME_WORK,
        DESCRIPTION_WORK
      );

      SaveChangesWithIdentity();
    }
  }
}