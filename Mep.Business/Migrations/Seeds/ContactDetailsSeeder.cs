using Mep.Data.Entities;
using System.Linq;
using System;

namespace Mep.Business.Migrations.Seeds
{
  internal class ContactDetailsSeeder : SeederBase
  {

    internal ContactDetailsSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      ContactDetail contactDetail;

      if ((contactDetail = _context.ContactDetails
        .SingleOrDefault(g => g.Address1 == CONTACT_DETAIL_ADDRESS_1)) == null)
      {
        contactDetail = new ContactDetail();
        _context.Add(contactDetail);
      }
      contactDetail.Address1 = CONTACT_DETAIL_ADDRESS_1;
      contactDetail.CcgId = GetFirstCcg();
      contactDetail.ContactDetailTypeId = 2;
      contactDetail.EmailAddress = EMAIL_ADDRESS;
      contactDetail.IsActive = true;
      contactDetail.Latitude = LATITUDE;
      contactDetail.Longitude = LONGITUDE ;
      contactDetail.ModifiedAt = _now;
      contactDetail.ModifiedByUser = GetSystemAdminUser();
      contactDetail.Postcode = POSTCODE;
      contactDetail.TelephoneNumber = TELEPHONE_NUMBER;
      contactDetail.Town = TOWN;
      contactDetail.User = GetSystemAdminUser();
    }
  }
}