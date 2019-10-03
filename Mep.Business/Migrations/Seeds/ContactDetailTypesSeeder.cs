using Mep.Data.Entities;
using System.Linq;
using System;

namespace Mep.Business.Migrations.Seeds
{
  internal class ContactDetailTypesSeeder : SeederBase
  {

    internal ContactDetailTypesSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      ContactDetailType contactDetailType;

      if ((contactDetailType = _context.ContactDetailTypes
        .SingleOrDefault(g => g.Name == CONTACT_DETAIL_TYPE_NAME)) == null)
      {
        contactDetailType = new ContactDetailType();
        _context.Add(contactDetailType);
      }
      contactDetailType.IsActive = true;
      contactDetailType.ModifiedAt = _now;
      contactDetailType.ModifiedByUser = GetSystemAdminUser();
      contactDetailType.Name = CONTACT_DETAIL_TYPE_NAME;
      contactDetailType.Description = CONTACT_DETAIL_TYPE_DESCRIPTION;
    }
  }
}