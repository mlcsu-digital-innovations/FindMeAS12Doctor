using Fmas12d.Data.Entities;
using System.Linq;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class ContactDetailsSeeder : SeederBase<ContactDetail>
  {
    #region Constants      
    public const string ADDRESS1_DOCTOR_FEMALE_WORK = "5 Hide Street";
    public const string ADDRESS2_DOCTOR_FEMALE_WORK = null;
    public const string ADDRESS3_DOCTOR_FEMALE_WORK = null;
    public const string EMAIL_ADDRESS_DOCTOR_FEMALE_WORK = "doctor.feMALE_WORK@hidestreet.com";
    public const decimal LATITUDE_DOCTOR_FEMALE_WORK = 53.003440m;
    public const decimal LONGITUDE_DOCTOR_FEMALE_WORK = -2.186513m;
    public const string POSTCODE_DOCTOR_FEMALE_WORK = "ST4 1NF";
    public const int TELEPHONE_NUMBER_DOCTOR_FEMALE_WORK = 01782111111;
    public const string TOWN_DOCTOR_FEMALE_WORK = "Stoke-on-Trent";

    public const string ADDRESS1_DOCTOR_MALE_HOME = "5 Hartley Close";
    public const string ADDRESS2_DOCTOR_MALE_HOME = null;
    public const string ADDRESS3_DOCTOR_MALE_HOME = null;
    public const string EMAIL_ADDRESS_DOCTOR_MALE_HOME = "doctor.MALE_HOME@hartleyclose.com";
    public const decimal LATITUDE_DOCTOR_MALE_HOME = 52.905948m;
    public const decimal LONGITUDE_DOCTOR_MALE_HOME = -2.159811m;
    public const string POSTCODE_DOCTOR_MALE_HOME = "ST15 0WB";
    public const int TELEPHONE_NUMBER_DOCTOR_MALE_HOME = 01782222222;
    public const string TOWN_DOCTOR_MALE_HOME = "Stone";   

    public const string ADDRESS1_DOCTOR_MALE_WORK = "Hartley House";
    public const string ADDRESS2_DOCTOR_MALE_WORK = "Unit 21-22";
    public const string ADDRESS3_DOCTOR_MALE_WORK = "Galveston Grove";
    public const string EMAIL_ADDRESS_DOCTOR_MALE_WORK = "doctor.MALE_WORK@hartleyhouse.com";
    public const decimal LATITUDE_DOCTOR_MALE_WORK = 52.992566m;
    public const decimal LONGITUDE_DOCTOR_MALE_WORK = -2.150226m;
    public const string POSTCODE_DOCTOR_MALE_WORK = "ST4 3PE";
    public const int TELEPHONE_NUMBER_DOCTOR_MALE_WORK = 01782333333;
    public const string TOWN_DOCTOR_MALE_WORK = "Stoke-on-Trent";   
    #endregion

    internal void SeedData()
    {
      AddOrUpdate(
        ADDRESS1_DOCTOR_FEMALE_WORK,
        ADDRESS2_DOCTOR_FEMALE_WORK,
        ADDRESS3_DOCTOR_FEMALE_WORK,
        CcgSeeder.NORTH_STAFFORDSHIRE,
        Models.ContactDetailType.WORK,
        EMAIL_ADDRESS_DOCTOR_FEMALE_WORK,
        LATITUDE_DOCTOR_FEMALE_WORK,
        LONGITUDE_DOCTOR_FEMALE_WORK,
        POSTCODE_DOCTOR_FEMALE_WORK,
        TELEPHONE_NUMBER_DOCTOR_FEMALE_WORK,
        TOWN_DOCTOR_FEMALE_WORK,
        UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE
      );

       AddOrUpdate(
        ADDRESS1_DOCTOR_FEMALE_WORK,
        ADDRESS2_DOCTOR_FEMALE_WORK,
        ADDRESS3_DOCTOR_FEMALE_WORK,
        CcgSeeder.STOKE_ON_TRENT,
        Models.ContactDetailType.WORK,
        EMAIL_ADDRESS_DOCTOR_FEMALE_WORK,
        LATITUDE_DOCTOR_FEMALE_WORK,
        LONGITUDE_DOCTOR_FEMALE_WORK,
        POSTCODE_DOCTOR_FEMALE_WORK,
        TELEPHONE_NUMBER_DOCTOR_FEMALE_WORK,
        TOWN_DOCTOR_FEMALE_WORK,
        UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE
      );

      AddOrUpdate(
        ADDRESS1_DOCTOR_MALE_HOME,
        ADDRESS2_DOCTOR_MALE_HOME,
        ADDRESS3_DOCTOR_MALE_HOME,
        CcgSeeder.NORTH_STAFFORDSHIRE,
        Models.ContactDetailType.HOME,
        EMAIL_ADDRESS_DOCTOR_MALE_HOME,
        LATITUDE_DOCTOR_MALE_HOME,
        LONGITUDE_DOCTOR_MALE_HOME,
        POSTCODE_DOCTOR_MALE_HOME,
        TELEPHONE_NUMBER_DOCTOR_MALE_HOME,
        TOWN_DOCTOR_MALE_HOME,
        UserSeeder.DISPLAY_NAME_DOCTOR_MALE
      );

       AddOrUpdate(
        ADDRESS1_DOCTOR_MALE_HOME,
        ADDRESS2_DOCTOR_MALE_HOME,
        ADDRESS3_DOCTOR_MALE_HOME,
        CcgSeeder.STOKE_ON_TRENT,
        Models.ContactDetailType.HOME,
        EMAIL_ADDRESS_DOCTOR_MALE_HOME,
        LATITUDE_DOCTOR_MALE_HOME,
        LONGITUDE_DOCTOR_MALE_HOME,
        POSTCODE_DOCTOR_MALE_HOME,
        TELEPHONE_NUMBER_DOCTOR_MALE_HOME,
        TOWN_DOCTOR_MALE_HOME,
        UserSeeder.DISPLAY_NAME_DOCTOR_MALE
      );       

      AddOrUpdate(
        ADDRESS1_DOCTOR_MALE_WORK,
        ADDRESS2_DOCTOR_MALE_WORK,
        ADDRESS3_DOCTOR_MALE_WORK,
        CcgSeeder.NORTH_STAFFORDSHIRE,
        Models.ContactDetailType.WORK,
        EMAIL_ADDRESS_DOCTOR_MALE_WORK,
        LATITUDE_DOCTOR_MALE_WORK,
        LONGITUDE_DOCTOR_MALE_WORK,
        POSTCODE_DOCTOR_MALE_WORK,
        TELEPHONE_NUMBER_DOCTOR_MALE_WORK,
        TOWN_DOCTOR_MALE_WORK,
        UserSeeder.DISPLAY_NAME_DOCTOR_MALE
      );

       AddOrUpdate(
        ADDRESS1_DOCTOR_MALE_WORK,
        ADDRESS2_DOCTOR_MALE_WORK,
        ADDRESS3_DOCTOR_MALE_WORK,
        CcgSeeder.STOKE_ON_TRENT,
        Models.ContactDetailType.WORK,
        EMAIL_ADDRESS_DOCTOR_MALE_WORK,
        LATITUDE_DOCTOR_MALE_WORK,
        LONGITUDE_DOCTOR_MALE_WORK,
        POSTCODE_DOCTOR_MALE_WORK,
        TELEPHONE_NUMBER_DOCTOR_MALE_WORK,
        TOWN_DOCTOR_MALE_WORK,
        UserSeeder.DISPLAY_NAME_DOCTOR_MALE
      );      
    }

    private void AddOrUpdate(
      string address1,
      string address2,
      string address3,
      string ccgName,
      int contactDetailTypeId,
      string emailAddress,
      decimal latitude,
      decimal longitude,
      string postcode,
      int? telephoneNumber,
      string town,
      string userDisplayName
    )
    {
      ContactDetail contactDetail;

      if ((contactDetail = Context.ContactDetails
            .Where(c => c.CcgId == GetCcgByName(ccgName).Id)
            .Where(c => c.ContactDetailTypeId == contactDetailTypeId)
            .SingleOrDefault(c => c.UserId ==
              GetUserByDisplayName(userDisplayName).Id)) == null)
      {
        contactDetail = new ContactDetail();
        Context.Add(contactDetail);
      }

      contactDetail.Address1 = address1;
      contactDetail.Address2 = address2;
      contactDetail.Address3 = address3;
      contactDetail.CcgId = GetCcgByName(ccgName).Id;
      contactDetail.ContactDetailTypeId = contactDetailTypeId;
      contactDetail.EmailAddress = emailAddress;
      contactDetail.Latitude = latitude;
      contactDetail.Longitude = longitude;
      contactDetail.Postcode = postcode;
      contactDetail.TelephoneNumber = telephoneNumber;
      contactDetail.Town = town;
      contactDetail.UserId = GetUserByDisplayName(userDisplayName).Id;
      PopulateActiveAndModifiedWithSystemUser(contactDetail);
    }
  }
}