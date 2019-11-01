using Fmas12d.Data.Entities;
using System.Linq;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class ContactDetailsSeeder : SeederBase<ContactDetail>
  {
    #region Constants      
    protected const string ADDRESS1_DOCTOR_FEMALE = "Doctor Female Address 1";
    protected const string ADDRESS2_DOCTOR_FEMALE = "Doctor Female Address 2";
    protected const string ADDRESS3_DOCTOR_FEMALE = "Doctor Female Address 3";
    protected const string EMAIL_ADDRESS_DOCTOR_FEMALE = "doctor.female@fmas12d.local";
    protected const decimal LATITUDE_DOCTOR_FEMALE = 52.991581m;
    protected const decimal LONGITUDE_DOCTOR_FEMALE = -2.167857m;
    protected const string POSTCODE_DOCTOR_FEMALE = "ST4 1NF";
    protected const int TELEPHONE_NUMBER_DOCTOR_FEMALE = 101;
    protected const string TOWN_DOCTOR_FEMALE = "Doctor Female Town";
    #endregion

    internal void SeedData()
    {
      ContactDetail contactDetail;

      if ((contactDetail = Context.ContactDetails
            .Where(c => c.CcgId == GetCcgByName(CcgSeeder.NORTH_STAFFORDSHIRE).Id)
            .Where(c => c.ContactDetailTypeId == GetContactDetailTypeWork().Id)
            .SingleOrDefault(c => c.UserId == 
              GetUserByDisplayName(UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE).Id)) == null)
      {
        contactDetail = new ContactDetail();
        Context.Add(contactDetail);
      }

      contactDetail.Address1 = ADDRESS1_DOCTOR_FEMALE;
      contactDetail.Address2 = ADDRESS2_DOCTOR_FEMALE;
      contactDetail.Address3 = ADDRESS3_DOCTOR_FEMALE;
      contactDetail.CcgId = GetCcgByName(CcgSeeder.NORTH_STAFFORDSHIRE).Id;
      contactDetail.ContactDetailTypeId = GetContactDetailTypeWork().Id;
      contactDetail.EmailAddress = EMAIL_ADDRESS_DOCTOR_FEMALE;
      contactDetail.Latitude = LATITUDE_DOCTOR_FEMALE;
      contactDetail.Longitude = LONGITUDE_DOCTOR_FEMALE;
      contactDetail.Postcode = POSTCODE_DOCTOR_FEMALE;
      contactDetail.TelephoneNumber = TELEPHONE_NUMBER_DOCTOR_FEMALE;
      contactDetail.Town = TOWN_DOCTOR_FEMALE;
      contactDetail.UserId = GetUserByDisplayName(UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE).Id;
      PopulateActiveAndModifiedWithSystemUser(contactDetail);

      if ((contactDetail = Context.ContactDetails
            .Where(c => c.ContactDetailTypeId == GetContactDetailTypeWork().Id)
            .Where(c => c.CcgId == GetCcgByName(CcgSeeder.STOKE_ON_TRENT).Id)
            .SingleOrDefault(c => c.UserId == 
              GetUserByDisplayName(UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE).Id)) == null)
      {
        contactDetail = new ContactDetail();
        Context.Add(contactDetail);
      }

      contactDetail.Address1 = ADDRESS1_DOCTOR_FEMALE;
      contactDetail.Address2 = ADDRESS2_DOCTOR_FEMALE;
      contactDetail.Address3 = ADDRESS3_DOCTOR_FEMALE;
      contactDetail.CcgId = GetCcgByName(CcgSeeder.STOKE_ON_TRENT).Id;
      contactDetail.ContactDetailTypeId = GetContactDetailTypeWork().Id;
      contactDetail.EmailAddress = EMAIL_ADDRESS_DOCTOR_FEMALE;
      contactDetail.Latitude = LATITUDE_DOCTOR_FEMALE;
      contactDetail.Longitude = LONGITUDE_DOCTOR_FEMALE;
      contactDetail.Postcode = POSTCODE_DOCTOR_FEMALE;
      contactDetail.TelephoneNumber = TELEPHONE_NUMBER_DOCTOR_FEMALE;
      contactDetail.Town = TOWN_DOCTOR_FEMALE;
      contactDetail.UserId = GetUserByDisplayName(UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE).Id;
      PopulateActiveAndModifiedWithSystemUser(contactDetail);      
    }
  }
}