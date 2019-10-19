using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class ContactDetailTypesSeeder : SeederBase<ContactDetailType>
  {
    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.ContactDetailType.WORK,
        CONTACT_DETAIL_TYPE_NAME_WORK,
        CONTACT_DETAIL_TYPE_DESCRIPTION_WORK
      );
    }
  }
}