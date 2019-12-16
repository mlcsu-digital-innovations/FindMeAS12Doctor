using Fmas12d.Data.Entities;
using System.Linq;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class ContactDetailCcgsSeeder : SeederBase<ContactDetailCcg>
  {
    #region Constants      
    public const string EMAIL_ADDRESS_DOCTOR_MALE_BASE_NORTH_STAFFORDSHIRE = 
      "doctormale.base@north_staffordshire";
    public const string EMAIL_ADDRESS_DOCTOR_MALE_HOME_NORTH_STAFFORDSHIRE = 
      "doctormale.home@north_staffordshire";
    public const string EMAIL_ADDRESS_DOCTOR_MALE_BASE_STOKE_ON_TRENT = 
      "doctormale.base@stoke_on_trent";
    public const string EMAIL_ADDRESS_DOCTOR_MALE_HOME_STOKE_ON_TRENT = 
      "doctormale.home@stoke_on_trent";            
    public const long TELEPHONE_NUMBER_DOCTOR_MALE_BASE_NORTH_STAFFORDSHIRE = 07886111111;
    public const long TELEPHONE_NUMBER_DOCTOR_MALE_HOME_NORTH_STAFFORDSHIRE = 07886222222;
    public const long TELEPHONE_NUMBER_DOCTOR_MALE_BASE_STOKE_ON_TRENT = 07886333333;
    public const long TELEPHONE_NUMBER_DOCTOR_MALE_HOME_STOKE_ON_TRENT = 07886444444;

    #endregion

    internal void SeedData()
    {
       AddOrUpdate(
        CcgSeeder.NORTH_STAFFORDSHIRE,
        Models.ContactDetailType.BASE,
        EMAIL_ADDRESS_DOCTOR_MALE_BASE_NORTH_STAFFORDSHIRE,
        TELEPHONE_NUMBER_DOCTOR_MALE_BASE_NORTH_STAFFORDSHIRE,
        UserSeeder.DISPLAY_NAME_DOCTOR_MALE
      );      

       AddOrUpdate(
        CcgSeeder.NORTH_STAFFORDSHIRE,
        Models.ContactDetailType.HOME,
        EMAIL_ADDRESS_DOCTOR_MALE_HOME_NORTH_STAFFORDSHIRE,
        TELEPHONE_NUMBER_DOCTOR_MALE_HOME_NORTH_STAFFORDSHIRE,
        UserSeeder.DISPLAY_NAME_DOCTOR_MALE
      );      

       AddOrUpdate(
        CcgSeeder.STOKE_ON_TRENT,
        Models.ContactDetailType.BASE,
        EMAIL_ADDRESS_DOCTOR_MALE_BASE_STOKE_ON_TRENT,
        TELEPHONE_NUMBER_DOCTOR_MALE_BASE_STOKE_ON_TRENT,
        UserSeeder.DISPLAY_NAME_DOCTOR_MALE
      );      

       AddOrUpdate(
        CcgSeeder.STOKE_ON_TRENT,
        Models.ContactDetailType.HOME,
        EMAIL_ADDRESS_DOCTOR_MALE_HOME_STOKE_ON_TRENT,
        TELEPHONE_NUMBER_DOCTOR_MALE_HOME_STOKE_ON_TRENT,
        UserSeeder.DISPLAY_NAME_DOCTOR_MALE
      );      
    }

    private void AddOrUpdate(
      string ccgName,
      int contactDetailTypeId,
      string emailAddress,
      long? telephoneNumber,
      string userDisplayName
    )
    {
      ContactDetailCcg contactDetailCcg;

      if ((contactDetailCcg = Context.ContactDetailCcgs
            .Where(c => c.CcgId == GetCcgByName(ccgName).Id)
            .Where(c => c.ContactDetailTypeId == contactDetailTypeId)
            .SingleOrDefault(c => c.UserId ==
              GetUserByDisplayName(userDisplayName).Id)) == null)
      {
        contactDetailCcg = new ContactDetailCcg();
        Context.Add(contactDetailCcg);
      }

      contactDetailCcg.CcgId = GetCcgByName(ccgName).Id;
      contactDetailCcg.ContactDetailTypeId = contactDetailTypeId;
      contactDetailCcg.EmailAddress = emailAddress;
      contactDetailCcg.TelephoneNumber = telephoneNumber;
      contactDetailCcg.UserId = GetUserByDisplayName(userDisplayName).Id;
      PopulateActiveAndModifiedWithSystemUser(contactDetailCcg);
    }
  }
}