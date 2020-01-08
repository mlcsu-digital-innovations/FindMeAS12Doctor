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
      AddOrUpdate("2 Lower Claremont St", "", "", Models.ContactDetailType.BASE, "drsnow_base@gmail.com", 52.709428m, -2.758459m, "SY1 1RT", "01743 111101", "Shrewsbury", "Dr Snow");
      AddOrUpdate("3 Smith St", "", "", Models.ContactDetailType.BASE, "drbell_base@gmail.com", 52.71104m, -2.755198m, "SY1 1PG", "01743 111102", "Shrewsbury", "Dr Bell");
      AddOrUpdate("1-5 Talisman Dr", "", "", Models.ContactDetailType.BASE, "drwhite_base@gmail.com", 52.708435m, -2.729964m, "SY2 5NB", "01743 111103", "Shrewsbury", "Dr White");
      AddOrUpdate("15 Sutton Rd", "", "", Models.ContactDetailType.BASE, "drblack_base@gmail.com", 52.699698m, -2.730908m, "SY2 6DD", "01743 111104", "Shrewsbury", "Dr Black");
      AddOrUpdate("Bank Farm Rd", "", "", Models.ContactDetailType.BASE, "drbrown_base@gmail.com", 52.697107m, -2.776269m, "SY3 6DU", "01743 111105", "Shrewsbury", "Dr Brown");
      AddOrUpdate("Whitehall Mansion", "", "", Models.ContactDetailType.BASE, "drjones_base@gmail.com", 52.707706m, -2.736698m, "SY2 5AP", "01743 111106", "Shrewsbury", "Dr Jones");
      AddOrUpdate("Burlington Pl", "", "", Models.ContactDetailType.BASE, "drsmith_base@gmail.com", 52.700245m, -2.75207m, "SY3 7LF", "01743 111107", "Shrewsbury", "Dr Smith");
      AddOrUpdate("Brook St", "", "", Models.ContactDetailType.BASE, "drmorris_base@gmail.com", 52.696568m, -2.746454m, "SY3 7QR", "01743 111108", "Shrewsbury", "Dr Morris");
      AddOrUpdate("Mytton Oak Rd", "", "", Models.ContactDetailType.BASE, "drbailey_base@gmail.com", 52.706775m, -2.793391m, "SY3 8XQ", "01743 111109", "Shrewsbury", "Dr Bailey");
      AddOrUpdate("Apley Castle", "", "", Models.ContactDetailType.BASE, "drtaylor_base@gmail.com", 52.711417m, -2.511643m, "TF1 6TF", "01743 111110", "Shrewsbury", "Dr Taylor");

      AddOrUpdate("34-42 Montague Pl", "", "", Models.ContactDetailType.HOME, "drsnow_home@gmail.com", 52.709428m, -2.758459m, "SY3 7NF", "01743 222201", "Shrewsbury", "Dr Snow");
      AddOrUpdate("87 Copthorne Rd", "", "", Models.ContactDetailType.HOME, "drbell_home@gmail.com", 52.71104m, -2.755198m, "SY3 8NL", "01743 222202", "Shrewsbury", "Dr Bell");
      AddOrUpdate("110-98 Tilstock Cres", "", "", Models.ContactDetailType.HOME, "drwhite_home@gmail.com", 52.708435m, -2.729964m, "SY2 6HB", "01743 222203", "Shrewsbury", "Dr White");
      AddOrUpdate("16-12 Stanhill Rd", "", "", Models.ContactDetailType.HOME, "drblack_home@gmail.com", 52.699698m, -2.730908m, "SY3 6AL", "01743 222204", "Shrewsbury", "Dr Black");
      AddOrUpdate("41 Grange Fields Rd", "", "", Models.ContactDetailType.HOME, "drbrown_home@gmail.com", 52.697107m, -2.776269m, "SY3 9DD", "01743 222205", "Shrewsbury", "Dr Brown");
      AddOrUpdate("28-2 Athelstan Rd", "", "", Models.ContactDetailType.HOME, "drjones_home@gmail.com", 52.184323m, -2.210714m, "WR5 2BW", "01743 222206", "Shrewsbury", "Dr Jones");
      AddOrUpdate("62-54 Broad St", "", "", Models.ContactDetailType.HOME, "drsmith_home@gmail.com", 52.473451m, -1.917353m, "B15 1DT", "01743 222207", "Shrewsbury", "Dr Smith");
      AddOrUpdate("6 Harley Rd", "", "", Models.ContactDetailType.HOME, "drmorris_home@gmail.com", 52.696568m, -2.746454m, "SY5 7AX", "01743 222208", "Shrewsbury", "Dr Morris");
      AddOrUpdate("12 Stretton Close", "", "", Models.ContactDetailType.HOME, "drbailey_home@gmail.com", 52.689321m, -2.734735m, "SY2 6EY", "01743 222209", "Shrewsbury", "Dr Bailey");
      AddOrUpdate("20 Grinshill Drive", "", "", Models.ContactDetailType.HOME, "drtaylor_home@gmail.com", 52.709769m, -2.728653m, "SY2 5JE", "01743 222210", "Shrewsbury", "Dr Taylor");

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