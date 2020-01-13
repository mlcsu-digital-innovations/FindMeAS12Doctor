using Fmas12d.Data.Entities;
using System;
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
    public const string TELEPHONE_NUMBER_DOCTOR_FEMALE_HOME = "01782444444";
    public const string TOWN_DOCTOR_FEMALE_HOME = "Newcastle";

    public const string ADDRESS1_DOCTOR_FEMALE_BASE = "5 Hide Street";
    public const string ADDRESS2_DOCTOR_FEMALE_BASE = null;
    public const string ADDRESS3_DOCTOR_FEMALE_BASE = null;
    public const string EMAIL_ADDRESS_DOCTOR_FEMALE_BASE = "doctorfemale_base@hidestreet.com";
    public const decimal LATITUDE_DOCTOR_FEMALE_BASE = 53.003440m;
    public const decimal LONGITUDE_DOCTOR_FEMALE_BASE = -2.186513m;
    public const string POSTCODE_DOCTOR_FEMALE_BASE = "ST4 1NF";
    public const string TELEPHONE_NUMBER_DOCTOR_FEMALE_BASE = "01782111111";
    public const string TOWN_DOCTOR_FEMALE_BASE = "Stoke-on-Trent";

    public const string ADDRESS1_DOCTOR_MALE_HOME = "5 Hartley Close";
    public const string ADDRESS2_DOCTOR_MALE_HOME = null;
    public const string ADDRESS3_DOCTOR_MALE_HOME = null;
    public const string EMAIL_ADDRESS_DOCTOR_MALE_HOME = "doctormale_home@hartleyclose.com";
    public const decimal LATITUDE_DOCTOR_MALE_HOME = 52.905948m;
    public const decimal LONGITUDE_DOCTOR_MALE_HOME = -2.159811m;
    public const string POSTCODE_DOCTOR_MALE_HOME = "ST15 0WB";
    public const string TELEPHONE_NUMBER_DOCTOR_MALE_HOME = "01782222222";
    public const string TOWN_DOCTOR_MALE_HOME = "Stone";

    public const string ADDRESS1_DOCTOR_MALE_BASE = "Hartley House";
    public const string ADDRESS2_DOCTOR_MALE_BASE = "Unit 21-22";
    public const string ADDRESS3_DOCTOR_MALE_BASE = "Galveston Grove";
    public const string EMAIL_ADDRESS_DOCTOR_MALE_BASE = "doctormale_base@hartleyhouse.com";
    public const decimal LATITUDE_DOCTOR_MALE_BASE = 52.992566m;
    public const decimal LONGITUDE_DOCTOR_MALE_BASE = -2.150226m;
    public const string POSTCODE_DOCTOR_MALE_BASE = "ST4 3PE";
    public const string TELEPHONE_NUMBER_DOCTOR_MALE_BASE = "01782333333";
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

    internal void SeedDataUat()
    {
      AddOrUpdate("28-30 Weston Street", "", "", Models.ContactDetailType.BASE, "drsnow_base@gmail.com", 52.9979482m, -2.1263289m, "ST3 5DQ", "01743 111101", "Stoke on Trent", UserSeeder.DISPLAY_NAME_DR_SNOW);
      AddOrUpdate("Upper Huntbach Street", "", "", Models.ContactDetailType.BASE, "drbell_base@gmail.com", 53.027237m, -2.172997m, "ST1 2BN", "01743 111102", "Stoke on Trent", UserSeeder.DISPLAY_NAME_DR_BELL);
      AddOrUpdate("Merton Street", "", "", Models.ContactDetailType.BASE, "drwhite_base@gmail.com", 52.9915623m, -2.1307032m, "ST3 1LG", "01743 111103", "Stoke on Trent", UserSeeder.DISPLAY_NAME_DR_WHITE);
      AddOrUpdate("Norfolk Street", "", "", Models.ContactDetailType.BASE, "drblack_base@gmail.com", 53.0157262m, -2.1849027m, "ST1 4PB", "01743 111104", "Stoke on Trent", UserSeeder.DISPLAY_NAME_DR_BLACK);
      AddOrUpdate("124 Werrington Road", "", "", Models.ContactDetailType.BASE, "drbrown_base@gmail.com", 53.0244087m, -2.1464677m, "ST2 9AJ", "01743 111105", "Stoke on Trent", UserSeeder.DISPLAY_NAME_DR_BROWN);
      AddOrUpdate("Scotia Road", "", "", Models.ContactDetailType.BASE, "drjones_base@gmail.com", 53.0579206m, -2.2085756m, "ST6 6BE", "01743 111106", "Stoke on Trent", UserSeeder.DISPLAY_NAME_DR_JONES);
      AddOrUpdate("876 London Road", "", "", Models.ContactDetailType.BASE, "drsmith_base@gmail.com", 52.9873044m, -2.204212m, "ST4 5NX", "01743 111107", "Stoke on Trent", UserSeeder.DISPLAY_NAME_DR_SMITH);
      AddOrUpdate("13 Drayton Road", "", "", Models.ContactDetailType.BASE, "drmorris_base@gmail.com", 52.9920556m, 2.1374935m, "ST3 1EQ", "01743 111108", "Stoke on Trent", UserSeeder.DISPLAY_NAME_DR_MORRIS);
      AddOrUpdate("Church Terrace", "", "", Models.ContactDetailType.BASE, "drbailey_base@gmail.com", 53.0352936m, -2.1889541m, "ST6 2JN", "01743 111109", "Stoke on Trent", UserSeeder.DISPLAY_NAME_DR_BAILEY);
      AddOrUpdate("988 Leek New Road", "", "", Models.ContactDetailType.BASE, "drtaylor_base@gmail.com", 53.061417m, -2.1363147m, "ST9 9PB", "01743 111110", "Stoke on Trent", UserSeeder.DISPLAY_NAME_DR_TAYLOR);

      AddOrUpdate("9 Dilke Street", "", "", Models.ContactDetailType.HOME, "drsnow_home@gmail.com", 53.0314424m, -2.172372m, "ST1 2LJ", "01743 222201", "Stoke on Trent", UserSeeder.DISPLAY_NAME_DR_SNOW);
      AddOrUpdate("Birches Head Road", "", "", Models.ContactDetailType.HOME, "drbell_home@gmail.com", 53.0393739m, -2.1499477m, "ST2 8DD", "01743 222202", "Stoke on Trent", UserSeeder.DISPLAY_NAME_DR_BELL);
      AddOrUpdate("54 Normanton Road", "", "", Models.ContactDetailType.HOME, "drwhite_home@gmail.com", 52.9997862m, -2.1275117m, "ST3 5BY", "01743 222203", "Stoke on Trent", UserSeeder.DISPLAY_NAME_DR_WHITE);
      AddOrUpdate("11 Grove Avenue", "", "", Models.ContactDetailType.HOME, "drblack_home@gmail.com", 52.9926856m, -2.1659017m, "ST4 3BA", "01743 222204", "Stoke on Trent", UserSeeder.DISPLAY_NAME_DR_BLACK);
      AddOrUpdate("2 Chatterley Close", "", "", Models.ContactDetailType.HOME, "drbrown_home@gmail.com", 53.043422m, -2.2298026m, "ST5 8LE", "01743 222205", "Stoke on Trent", UserSeeder.DISPLAY_NAME_DR_BROWN);
      AddOrUpdate("17 Port Street", "", "", Models.ContactDetailType.HOME, "drjones_home@gmail.com", 53.0408353m, -2.2110494m, "ST6 3PF", "01743 222206", "Stoke on Trent", UserSeeder.DISPLAY_NAME_DR_JONES);
      AddOrUpdate("53 Hassall Road", "", "", Models.ContactDetailType.HOME, "drsmith_home@gmail.com", 53.1002073m, -2.3218685m, "ST7 2HP", "01743 222207", "Stoke on Trent", UserSeeder.DISPLAY_NAME_DR_SMITH);
      AddOrUpdate("Lynmouth Close", "", "", Models.ContactDetailType.HOME, "drmorris_home@gmail.com", 53.1089476m, -2.1768125m, "ST8 6LS", "01743 222208", "Stoke on Trent", UserSeeder.DISPLAY_NAME_DR_MORRIS);
      AddOrUpdate("7 Station Road", "", "", Models.ContactDetailType.HOME, "drbailey_home@gmail.com", 53.0759573m, -2.1125302m, "ST9 9DR", "01743 222209", "Stoke on Trent", UserSeeder.DISPLAY_NAME_DR_BAILEY);
      AddOrUpdate("3 Tennyson Close", "", "", Models.ContactDetailType.HOME, "drtaylor_home@gmail.com", 52.9812184m, -2.0037175m, "ST10 1XF", "01743 222210", "Stoke on Trent", UserSeeder.DISPLAY_NAME_DR_TAYLOR);

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
      string telephoneNumber,
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