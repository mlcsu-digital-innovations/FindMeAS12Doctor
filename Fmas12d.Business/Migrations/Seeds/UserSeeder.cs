using Fmas12d.Data.Entities;
using System.Linq;
using System;

namespace Fmas12d.Business.Migrations.Seeds
{
  public class UserSeeder : SeederBase<User>
  {
    #region Constants
    public const string DISPLAY_NAME_ADMIN = "Admin";
    public const string DISPLAY_NAME_AMHP_FEMALE = "Amhp Female";
    public const string DISPLAY_NAME_AMHP_MALE = "Amhp Male";
    public const string DISPLAY_NAME_DOCTOR_FEMALE = "Doctor Female";
    public const string DISPLAY_NAME_DOCTOR_MALE = "Doctor Male";
    public const string DISPLAY_NAME_FINANCE = "Finance";
    public const string DISPLAY_NAME_HELENCROSS = "Helen Cross";
    public const string DISPLAY_NAME_JEHANDAVIDBEYERS = "Jehan-David Beyers";
    public const string DISPLAY_NAME_MARKCARTER = "Mark Carter";
    public const string DISPLAY_NAME_NEILDAVIES = "Neil Davies";
    public const string DISPLAY_NAME_NEILDAVIESMLCSU = "Neil Davies (MLCSU)";
    public const string DISPLAY_NAME_NEILEDAVIES = "Neil E Davies";
    public const string DISPLAY_NAME_STEPHENHARLAND = "Stephen Harland";

    public const int GMCNUMBER_DOCTOR_FEMALE = 1111111;
    public const int GMCNUMBER_DOCTOR_MALE = 2222222;

    public const string IDENTITY_SERVER_IDENTIFIER_ADMIN = 
      "977f7610-4048-4a7f-ae8f-47ff0f33a59b";    
    public const string IDENTITY_SERVER_IDENTIFIER_AMHP_FEMALE = 
      "1635d415-69a8-45e3-8449-1112924beccf";
    public const string IDENTITY_SERVER_IDENTIFIER_AMHP_MALE = 
      "da5b9a7b-5a63-499e-a694-fe8286ae9d9f";      
    public const string IDENTITY_SERVER_IDENTIFIER_DOCTOR_FEMALE = 
      "7eb68d85-7df7-4a47-8320-55be73644fa8";
    public const string IDENTITY_SERVER_IDENTIFIER_DOCTOR_MALE = 
      "d5f1594d-cdee-4961-ab2b-dd70228c9611";
    public const string IDENTITY_SERVER_IDENTIFIER_FINANCE =
      "e2d78f4e-4a0c-4289-aafd-31a1d5928411";
    public const string IDENTITY_SERVER_IDENTIFIER_HELENCROSS = 
      "efc945eb-ee1b-4ed1-9fac-269c145196c6";      
    public const string IDENTITY_SERVER_IDENTIFIER_JEHANDAVIDBEYERS = 
      "ac74d440-5901-4660-9ed7-3e579e248e2e";      
    public const string IDENTITY_SERVER_IDENTIFIER_MARKCARTER = 
      "903ec2b7-a3be-4e91-a8ef-e3f5292f44f1";      
    public const string IDENTITY_SERVER_IDENTIFIER_NEILDAVIES = 
      "f52c4a24-4071-4cd8-a7be-9e6f27446aba";      
    public const string IDENTITY_SERVER_IDENTIFIER_NEILDAVIESMLCSU = 
      "b1f8a2ca-48d3-4c05-aff9-4c343384190b";      
    public const string IDENTITY_SERVER_IDENTIFIER_NEILEDAVIES = 
      "bb3712bb-d1ea-4bbf-8a0c-02a3dae26b1d";      
    public const string IDENTITY_SERVER_IDENTIFIER_STEPHENHARLAND = 
      "c37a5d2d-ffa0-47b7-9286-c4e252a65696";      
    public const string IDENTITY_SERVER_IDENTIFIER_SYSTEM_ADMIN = 
      "bf673270-2538-4e59-9d26-5b4808fd9ef6";

    public readonly DateTimeOffset SECTION_12_EXPIRY_DATE_DOCTOR_MALE =
      new DateTimeOffset(2025, 1, 1,
                         0, 00, 00, 00, DateTimeOffset.Now.Offset);   

    #endregion
    public void SeedData()
    {
      AddUpdateUserWithDefaults(
        DISPLAY_NAME_ADMIN,
        GetGenderTypeFemale().Id,
        GetProfileTypeAdmin().Id,
        IDENTITY_SERVER_IDENTIFIER_ADMIN
      );

      AddUpdateUserWithDefaults(
        DISPLAY_NAME_AMHP_FEMALE, 
        GetGenderTypeFemale().Id, 
        GetProfileTypeAmhp().Id,
        IDENTITY_SERVER_IDENTIFIER_AMHP_FEMALE
      );      

      AddUpdateUserWithDefaults(
        DISPLAY_NAME_AMHP_MALE, 
        GetGenderTypeMale().Id, 
        GetProfileTypeAmhp().Id,
        IDENTITY_SERVER_IDENTIFIER_AMHP_MALE
      );      

      AddUpdateUserDoctorWithDefaults(
        DISPLAY_NAME_DOCTOR_FEMALE, 
        GetGenderTypeFemale().Id,
        GMCNUMBER_DOCTOR_FEMALE,
        IDENTITY_SERVER_IDENTIFIER_DOCTOR_FEMALE
      );

      AddUpdateUserDoctorWithDefaults(
        DISPLAY_NAME_DOCTOR_MALE,
        GetGenderTypeMale().Id,
        GMCNUMBER_DOCTOR_MALE,
        IDENTITY_SERVER_IDENTIFIER_DOCTOR_MALE,
        GetSection12ApprovalStatusApproved().Id,
        SECTION_12_EXPIRY_DATE_DOCTOR_MALE        
      );
    
      AddUpdateUserWithDefaults(
        DISPLAY_NAME_FINANCE,
        GetGenderTypeFemale().Id,
        GetProfileTypeFinance().Id,
        IDENTITY_SERVER_IDENTIFIER_FINANCE
      );

      AddUpdateUserWithDefaults(
        DISPLAY_NAME_HELENCROSS,
        GetGenderTypeFemale().Id,
        GetProfileTypeAdmin().Id,
        IDENTITY_SERVER_IDENTIFIER_HELENCROSS
      );

      AddUpdateUserWithDefaults(
        DISPLAY_NAME_JEHANDAVIDBEYERS,
        GetGenderTypeMale().Id,
        GetProfileTypeAdmin().Id,
        IDENTITY_SERVER_IDENTIFIER_JEHANDAVIDBEYERS
      );

      AddUpdateUserWithDefaults(
        DISPLAY_NAME_MARKCARTER,
        GetGenderTypeMale().Id,
        GetProfileTypeAdmin().Id,
        IDENTITY_SERVER_IDENTIFIER_MARKCARTER
      );

      AddUpdateUserWithDefaults(
        DISPLAY_NAME_NEILDAVIES,
        GetGenderTypeMale().Id,
        GetProfileTypeAdmin().Id,
        IDENTITY_SERVER_IDENTIFIER_NEILDAVIES
      );

      AddUpdateUserWithDefaults(
        DISPLAY_NAME_NEILDAVIESMLCSU,
        GetGenderTypeMale().Id,
        GetProfileTypeAdmin().Id,
        IDENTITY_SERVER_IDENTIFIER_NEILDAVIESMLCSU
      );

      AddUpdateUserWithDefaults(
        DISPLAY_NAME_NEILEDAVIES,
        GetGenderTypeMale().Id,
        GetProfileTypeAdmin().Id,
        IDENTITY_SERVER_IDENTIFIER_NEILEDAVIES
      );

      AddUpdateUserWithDefaults(
        DISPLAY_NAME_STEPHENHARLAND,
        GetGenderTypeMale().Id,
        GetProfileTypeAdmin().Id,
        IDENTITY_SERVER_IDENTIFIER_STEPHENHARLAND
      );                              
    }

    private User AddUpdateUserWithDefaults(
      string displayName,
      int genderTypeId,
      int profileTypeId,
      string identityServerIdentifier,
      int? organisationId = null
    )
    {
      User user;
      if ((user = Context.Users
                          .SingleOrDefault(g => g.DisplayName == displayName)) == null)
      {
        user = new User();
        Context.Add(user);
      }
      
      user.DisplayName = displayName;
      user.GenderTypeId = genderTypeId;                  
      user.GmcNumber = null;
      user.HasReadTermsAndConditions = true;
      user.IdentityServerIdentifier = identityServerIdentifier ?? Guid.NewGuid().ToString();  
      user.OrganisationId = organisationId.HasValue ? 
                            (int)organisationId : 
                            GetOrganisationIdByName(OrganisationSeeder.NAME_1);
      user.ProfileTypeId = profileTypeId;
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;

      PopulateActiveAndModifiedWithSystemUser(user);

      return user;
    }

    private User AddUpdateUserDoctorWithDefaults(
      string displayName,
      int genderTypeId,
      int gmcNumber,
      string identityServerIdentifier,      
      int? section12ApprovalStatusId = null,
      DateTimeOffset? section12ExpiryDate = null
    )
    {
      User user = AddUpdateUserWithDefaults(
        displayName, 
        genderTypeId,
        GetProfileTypeDoctor().Id,
        identityServerIdentifier: identityServerIdentifier);

      user.GmcNumber = gmcNumber;
      user.Section12ApprovalStatusId = section12ApprovalStatusId;
      user.Section12ExpiryDate = section12ExpiryDate;

      return user;
    }      
     
    /// <summary>
    /// Deletes all seeds except for Id = 1 which is required for the system account
    /// </summary>
    internal override void DeleteSeeds()
    {
      Context.Users.RemoveRange(
        Context.Users.Where(u => u.Id != 1).ToList()
      );

      ResetIdentity(1);
    }
  }
  
}