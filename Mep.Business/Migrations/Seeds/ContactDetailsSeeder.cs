using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class ContactDetailsSeeder : SeederBase
  {
    internal void SeedData()
    {
      ContactDetail contactDetail;

      if ((contactDetail = _context
        .ContactDetails
          .SingleOrDefault(g => g.Address1 ==
            CONTACT_DETAIL_ADDRESS_1)) == null)
      {
        contactDetail = new ContactDetail();
        _context.Add(contactDetail);
      }
      contactDetail.Address1 = CONTACT_DETAIL_ADDRESS_1;
      contactDetail.CcgId = GetFirstCcg();
      contactDetail.ContactDetailTypeId =
        GetContactDetailTypeIdByContactDetailTypeName(CONTACT_DETAIL_TYPE_NAME);
      contactDetail.EmailAddress = EMAIL_ADDRESS;
      contactDetail.Latitude = LATITUDE;
      contactDetail.Longitude = LONGITUDE;
      contactDetail.Postcode = POSTCODE;
      contactDetail.TelephoneNumber = TELEPHONE_NUMBER;
      contactDetail.Town = TOWN;
      contactDetail.User = GetSystemAdminUser();
      PopulateActiveAndModifiedWithSystemUser(contactDetail);
    }
  }
}