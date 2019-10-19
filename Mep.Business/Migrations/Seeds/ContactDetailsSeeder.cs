using Mep.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class ContactDetailsSeeder : SeederBase<ContactDetail>
  {
    internal void SeedData()
    {
      ContactDetail contactDetail;

      if ((contactDetail = _context.ContactDetails
            .Where(c => c.CcgId == GetCcgByName(CCG_NAME_NORTH_STAFFORDSHIRE).Id)
            .Where(c => c.ContactDetailTypeId == GetContactDetailTypeWork().Id)
            .SingleOrDefault(c => c.UserId == 
              GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_FEMALE).Id)) == null)
      {
        contactDetail = new ContactDetail();
        _context.Add(contactDetail);
      }

      contactDetail.Address1 = CONTACT_DETAIL_ADDRESS1_DOCTOR_FEMALE;
      contactDetail.Address2 = CONTACT_DETAIL_ADDRESS2_DOCTOR_FEMALE;
      contactDetail.Address3 = CONTACT_DETAIL_ADDRESS3_DOCTOR_FEMALE;
      contactDetail.CcgId = GetCcgByName(CCG_NAME_NORTH_STAFFORDSHIRE).Id;
      contactDetail.ContactDetailTypeId = GetContactDetailTypeWork().Id;
      contactDetail.EmailAddress = CONTACT_DETAIL_EMAIL_ADDRESS_DOCTOR_FEMALE;
      contactDetail.Latitude = CONTACT_DETAIL_LATITUDE_DOCTOR_FEMALE;
      contactDetail.Longitude = CONTACT_DETAIL_LONGITUDE_DOCTOR_FEMALE;
      contactDetail.Postcode = CONTACT_DETAIL_POSTCODE_DOCTOR_FEMALE;
      contactDetail.TelephoneNumber = CONTACT_DETAIL_TELEPHONE_NUMBER_DOCTOR_FEMALE;
      contactDetail.Town = CONTACT_DETAIL_TOWN_DOCTOR_FEMALE;
      contactDetail.UserId = GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_FEMALE).Id;
      PopulateActiveAndModifiedWithSystemUser(contactDetail);

      if ((contactDetail = _context.ContactDetails
            .Where(c => c.ContactDetailTypeId == GetContactDetailTypeWork().Id)
            .Where(c => c.CcgId == GetCcgByName(CCG_NAME_STOKE_ON_TRENT).Id)
            .SingleOrDefault(c => c.UserId == 
              GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_FEMALE).Id)) == null)
      {
        contactDetail = new ContactDetail();
        _context.Add(contactDetail);
      }

      contactDetail.Address1 = CONTACT_DETAIL_ADDRESS1_DOCTOR_FEMALE;
      contactDetail.Address2 = CONTACT_DETAIL_ADDRESS2_DOCTOR_FEMALE;
      contactDetail.Address3 = CONTACT_DETAIL_ADDRESS3_DOCTOR_FEMALE;
      contactDetail.CcgId = GetCcgByName(CCG_NAME_STOKE_ON_TRENT).Id;
      contactDetail.ContactDetailTypeId = GetContactDetailTypeWork().Id;
      contactDetail.EmailAddress = CONTACT_DETAIL_EMAIL_ADDRESS_DOCTOR_FEMALE;
      contactDetail.Latitude = CONTACT_DETAIL_LATITUDE_DOCTOR_FEMALE;
      contactDetail.Longitude = CONTACT_DETAIL_LONGITUDE_DOCTOR_FEMALE;
      contactDetail.Postcode = CONTACT_DETAIL_POSTCODE_DOCTOR_FEMALE;
      contactDetail.TelephoneNumber = CONTACT_DETAIL_TELEPHONE_NUMBER_DOCTOR_FEMALE;
      contactDetail.Town = CONTACT_DETAIL_TOWN_DOCTOR_FEMALE;
      contactDetail.UserId = GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_FEMALE).Id;
      PopulateActiveAndModifiedWithSystemUser(contactDetail);      
    }
  }
}