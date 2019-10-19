using Mep.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class ContactDetailsSeeder : SeederBase
  {
    internal void SeedData()
    {
      ContactDetail contactDetail;

      if ((contactDetail = _context.ContactDetails
            .Include(c => c.User)
            .SingleOrDefault(g => g.User.DisplayName == USER_DISPLAY_NAME_DOCTOR_FEMALE)) == null)
      {
        contactDetail = new ContactDetail();
        _context.Add(contactDetail);
      }

      contactDetail.Address1 = CONTACT_DETAIL_DOCTOR_FEMALE_ADDRESS_1;
      contactDetail.Address2 = CONTACT_DETAIL_DOCTOR_FEMALE_ADDRESS_2;
      contactDetail.Address3 = CONTACT_DETAIL_DOCTOR_FEMALE_ADDRESS_3;
      contactDetail.CcgId = GetFirstCcg().Id;
      contactDetail.ContactDetailTypeId = GetContactDetailTypeWork().Id;
      contactDetail.EmailAddress = CONTACT_DETAIL_DOCTOR_FEMALE_EMAIL_ADDRESS;
      contactDetail.Latitude = CONTACT_DETAIL_DOCTOR_FEMALE_LATITUDE;
      contactDetail.Longitude = CONTACT_DETAIL_DOCTOR_FEMALE_LONGITUDE;
      contactDetail.Postcode = CONTACT_DETAIL_DOCTOR_FEMALE_POSTCODE;
      contactDetail.TelephoneNumber = CONTACT_DETAIL_DOCTOR_FEMALE_TELEPHONE_NUMBER;
      contactDetail.Town = CONTACT_DETAIL_DOCTOR_FEMALE_TOWN;
      contactDetail.UserId = GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_FEMALE).Id;
      PopulateActiveAndModifiedWithSystemUser(contactDetail);
    }
  }
}