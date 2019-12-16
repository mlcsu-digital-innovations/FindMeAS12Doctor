using Fmas12d.Data.Entities;
using System.Linq;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class ContactDetailsSeeder : SeederBase<ContactDetail>
  {
    #region Constants      
    public const string ADDRESS1_DOCTOR_FEMALE_HOME = "2 Field Close";
    public const string ADDRESS2_DOCTOR_FEMALE_HOME = "Baldwin's Gate";
    public const string ADDRESS3_DOCTOR_FEMALE_HOME = null;
    public const string EMAIL_ADDRESS_DOCTOR_FEMALE_HOME = "doctorfemale_home@fieldclose.com";
    public const decimal LATITUDE_DOCTOR_FEMALE_HOME = 52.958159m;
    public const decimal LONGITUDE_DOCTOR_FEMALE_HOME = -2.306840m;
    public const string POSTCODE_DOCTOR_FEMALE_HOME = "ST5 5DJ";
    public const int TELEPHONE_NUMBER_DOCTOR_FEMALE_HOME = 01782444444;
    public const string TOWN_DOCTOR_FEMALE_HOME = "Newcastle";

    public const string ADDRESS1_DOCTOR_FEMALE_BASE = "5 Hide Street";
    public const string ADDRESS2_DOCTOR_FEMALE_BASE = null;
    public const string ADDRESS3_DOCTOR_FEMALE_BASE = null;
    public const string EMAIL_ADDRESS_DOCTOR_FEMALE_BASE = "doctorfemale_base@hidestreet.com";
    public const decimal LATITUDE_DOCTOR_FEMALE_BASE = 53.003440m;
    public const decimal LONGITUDE_DOCTOR_FEMALE_BASE = -2.186513m;
    public const string POSTCODE_DOCTOR_FEMALE_BASE = "ST4 1NF";
    public const int TELEPHONE_NUMBER_DOCTOR_FEMALE_BASE = 01782111111;
    public const string TOWN_DOCTOR_FEMALE_BASE = "Stoke-on-Trent";

    public const string ADDRESS1_DOCTOR_MALE_HOME = "5 Hartley Close";
    public const string ADDRESS2_DOCTOR_MALE_HOME = null;
    public const string ADDRESS3_DOCTOR_MALE_HOME = null;
    public const string EMAIL_ADDRESS_DOCTOR_MALE_HOME = "doctormale_home@hartleyclose.com";
    public const decimal LATITUDE_DOCTOR_MALE_HOME = 52.905948m;
    public const decimal LONGITUDE_DOCTOR_MALE_HOME = -2.159811m;
    public const string POSTCODE_DOCTOR_MALE_HOME = "ST15 0WB";
    public const int TELEPHONE_NUMBER_DOCTOR_MALE_HOME = 01782222222;
    public const string TOWN_DOCTOR_MALE_HOME = "Stone";   

    public const string ADDRESS1_DOCTOR_MALE_BASE = "Hartley House";
    public const string ADDRESS2_DOCTOR_MALE_BASE = "Unit 21-22";
    public const string ADDRESS3_DOCTOR_MALE_BASE = "Galveston Grove";
    public const string EMAIL_ADDRESS_DOCTOR_MALE_BASE = "doctormale_base@hartleyhouse.com";
    public const decimal LATITUDE_DOCTOR_MALE_BASE = 52.992566m;
    public const decimal LONGITUDE_DOCTOR_MALE_BASE = -2.150226m;
    public const string POSTCODE_DOCTOR_MALE_BASE = "ST4 3PE";
    public const int TELEPHONE_NUMBER_DOCTOR_MALE_BASE = 01782333333;
    public const string TOWN_DOCTOR_MALE_BASE = "Stoke-on-Trent";   
    #endregion

    internal void SeedData()
    {
      AddOrUpdate(
        ADDRESS1_DOCTOR_FEMALE_BASE,
        ADDRESS2_DOCTOR_FEMALE_BASE,
        ADDRESS3_DOCTOR_FEMALE_BASE,
        Models.ContactDetailType.BASE,
        EMAIL_ADDRESS_DOCTOR_FEMALE_BASE,
        LATITUDE_DOCTOR_FEMALE_BASE,
        LONGITUDE_DOCTOR_FEMALE_BASE,
        POSTCODE_DOCTOR_FEMALE_BASE,
        TELEPHONE_NUMBER_DOCTOR_FEMALE_BASE,
        TOWN_DOCTOR_FEMALE_BASE,
        UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE
      );

      AddOrUpdate(
        ADDRESS1_DOCTOR_FEMALE_HOME,
        ADDRESS2_DOCTOR_FEMALE_HOME,
        ADDRESS3_DOCTOR_FEMALE_HOME,
        Models.ContactDetailType.HOME,
        EMAIL_ADDRESS_DOCTOR_FEMALE_HOME,
        LATITUDE_DOCTOR_FEMALE_HOME,
        LONGITUDE_DOCTOR_FEMALE_HOME,
        POSTCODE_DOCTOR_FEMALE_HOME,
        TELEPHONE_NUMBER_DOCTOR_FEMALE_HOME,
        TOWN_DOCTOR_FEMALE_HOME,
        UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE
      );


      AddOrUpdate(
        ADDRESS1_DOCTOR_MALE_BASE,
        ADDRESS2_DOCTOR_MALE_BASE,
        ADDRESS3_DOCTOR_MALE_BASE,
        Models.ContactDetailType.BASE,
        EMAIL_ADDRESS_DOCTOR_MALE_BASE,
        LATITUDE_DOCTOR_MALE_BASE,
        LONGITUDE_DOCTOR_MALE_BASE,
        POSTCODE_DOCTOR_MALE_BASE,
        TELEPHONE_NUMBER_DOCTOR_MALE_BASE,
        TOWN_DOCTOR_MALE_BASE,
        UserSeeder.DISPLAY_NAME_DOCTOR_MALE
      );

       AddOrUpdate(
        ADDRESS1_DOCTOR_MALE_HOME,
        ADDRESS2_DOCTOR_MALE_HOME,
        ADDRESS3_DOCTOR_MALE_HOME,
        Models.ContactDetailType.HOME,
        EMAIL_ADDRESS_DOCTOR_MALE_HOME,
        LATITUDE_DOCTOR_MALE_HOME,
        LONGITUDE_DOCTOR_MALE_HOME,
        POSTCODE_DOCTOR_MALE_HOME,
        TELEPHONE_NUMBER_DOCTOR_MALE_HOME,
        TOWN_DOCTOR_MALE_HOME,
        UserSeeder.DISPLAY_NAME_DOCTOR_MALE
      );       
 
    }

    private void AddOrUpdate(
      string address1,
      string address2,
      string address3,
      int contactDetailTypeId,
      string emailAddress,
      decimal latitude,
      decimal longitude,
      string postcode,
      long? telephoneNumber,
      string town,
      string userDisplayName
    )
    {
      ContactDetail contactDetail;

      if ((contactDetail = Context.ContactDetails
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