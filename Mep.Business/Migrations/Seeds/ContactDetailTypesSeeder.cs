using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class ContactDetailTypesSeeder : SeederBase<ContactDetailType>
  {
    internal void SeedData()
    {
      ContactDetailType contactDetailType;

      if ((contactDetailType = _context.ContactDetailTypes.Find(Models.ContactDetailType.WORK)) == null)
      {
        contactDetailType = new ContactDetailType();
        _context.Add(contactDetailType);
      }

      PopulateNameDescriptionActiveAndModifiedWithSystemUser(
        contactDetailType,
        CONTACT_DETAIL_TYPE_NAME_WORK,
        CONTACT_DETAIL_TYPE_DESCRIPTION_WORK);
    }
  }
}