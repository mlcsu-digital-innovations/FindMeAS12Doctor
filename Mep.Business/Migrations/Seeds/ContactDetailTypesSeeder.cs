using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class ContactDetailTypesSeeder : SeederBase
  {
    internal void SeedData()
    {
      ContactDetailType contactDetailType;

      if ((contactDetailType = _context
        .ContactDetailTypes
          .SingleOrDefault(g => g.Name ==
            CONTACT_DETAIL_TYPE_NAME)) == null)
      {
        contactDetailType = new ContactDetailType();
        _context.Add(contactDetailType);
      }

      PopulateNameDescriptionActiveAndModifiedWithSystemUser(
        contactDetailType,
        CONTACT_DETAIL_TYPE_NAME,
        CONTACT_DETAIL_TYPE_DESCRIPTION);
    }
  }
}