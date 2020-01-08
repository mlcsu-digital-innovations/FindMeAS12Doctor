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
    // public const string DISPLAY_NAME_HELENCROSS = "Helen Cross";
    // public const string DISPLAY_NAME_JEHANDAVIDBEYERS = "Jehan-David Beyers";
    // public const string DISPLAY_NAME_MARKCARTER = "Mark Carter";
    // public const string DISPLAY_NAME_NEILDAVIES = "Neil Davies";
    // public const string DISPLAY_NAME_NEILDAVIESMLCSU = "Neil Davies (MLCSU)";
    // public const string DISPLAY_NAME_NEILEDAVIES = "Neil E Davies";
    // public const string DISPLAY_NAME_STEPHENHARLAND = "Stephen Harland";
    // public const string DISPLAY_NAME_PAULCOPELAND = "Paul Copeland";
    // public const string DISPLAY_NAME_SHARONIBBS = "Sharon Ibbs";
    // public const string DISPLAY_NAME_PRIYANTHA = "Priyantha Jawardine";

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

    // public const string IDENTITY_SERVER_IDENTIFIER_HELENCROSS = 
    //   "efc945eb-ee1b-4ed1-9fac-269c145196c6";      
    // public const string IDENTITY_SERVER_IDENTIFIER_JEHANDAVIDBEYERS = 
    //   "8a09f167-0183-4610-90fe-69f33675d3e8";      
    // public const string IDENTITY_SERVER_IDENTIFIER_MARKCARTER = 
    //   "1b6011e5-dfaf-4c5f-af80-f1911f8ee3fd";      
    // public const string IDENTITY_SERVER_IDENTIFIER_NEILDAVIES = 
    //   "f52c4a24-4071-4cd8-a7be-9e6f27446aba";      
    // public const string IDENTITY_SERVER_IDENTIFIER_NEILDAVIESMLCSU = 
    //   "fb222dee-faea-43f9-9eb5-a9d33e3bb7d0";      
    // public const string IDENTITY_SERVER_IDENTIFIER_NEILEDAVIES = 
    //   "f7a8b331-e898-4a0c-8564-227efc2a8721";      
    // public const string IDENTITY_SERVER_IDENTIFIER_STEPHENHARLAND = 
    //   "6d6fda51-7663-43d6-92c2-482bcfd14bd3"; 
    // public const string IDENTITY_SERVER_IDENTIFIER_PAULCOPELAND = 
    //   "558237f7-47ce-4046-9700-8e1fa0c20d21";
    // public const string IDENTITY_SERVER_IDENTIFIER_SHARONIBBS = 
    //   "9ec0170f-c3da-4525-835a-108aa271a6c3";
    // public const string IDENTITY_SERVER_IDENTIFIER_PRIYANTHA = 
    //   "fb5c9355-9b51-45f7-b7e8-dcced31186bb";     
    public const string IDENTITY_SERVER_IDENTIFIER_SYSTEM_ADMIN = 
      "bf673270-2538-4e59-9d26-5b4808fd9ef6";

    public readonly DateTimeOffset SECTION_12_EXPIRY_DATE_DOCTOR_FEMALE =
      new DateTimeOffset(2030, 1, 1,
                         0, 00, 00, 00, DateTimeOffset.Now.Offset);  
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

      // AddUpdateUserWithDefaults(
      //   DISPLAY_NAME_HELENCROSS,
      //   GetGenderTypeFemale().Id,
      //   GetProfileTypeAdmin().Id,
      //   IDENTITY_SERVER_IDENTIFIER_HELENCROSS
      // );

      // AddUpdateUserWithDefaults(
      //   DISPLAY_NAME_JEHANDAVIDBEYERS,
      //   GetGenderTypeMale().Id,
      //   GetProfileTypeAdmin().Id,
      //   IDENTITY_SERVER_IDENTIFIER_JEHANDAVIDBEYERS
      // );

      // AddUpdateUserWithDefaults(
      //   DISPLAY_NAME_MARKCARTER,
      //   GetGenderTypeMale().Id,
      //   GetProfileTypeAdmin().Id,
      //   IDENTITY_SERVER_IDENTIFIER_MARKCARTER
      // );

      // AddUpdateUserWithDefaults(
      //   DISPLAY_NAME_NEILDAVIES,
      //   GetGenderTypeMale().Id,
      //   GetProfileTypeAdmin().Id,
      //   IDENTITY_SERVER_IDENTIFIER_NEILDAVIES
      // );

      // AddUpdateUserWithDefaults(
      //   DISPLAY_NAME_NEILDAVIESMLCSU,
      //   GetGenderTypeMale().Id,
      //   GetProfileTypeAdmin().Id,
      //   IDENTITY_SERVER_IDENTIFIER_NEILDAVIESMLCSU
      // );

      // AddUpdateUserWithDefaults(
      //   DISPLAY_NAME_NEILEDAVIES,
      //   GetGenderTypeMale().Id,
      //   GetProfileTypeAdmin().Id,
      //   IDENTITY_SERVER_IDENTIFIER_NEILEDAVIES
      // );

      // AddUpdateUserWithDefaults(
      //   DISPLAY_NAME_STEPHENHARLAND,
      //   GetGenderTypeMale().Id,
      //   GetProfileTypeAdmin().Id,
      //   IDENTITY_SERVER_IDENTIFIER_STEPHENHARLAND
      // ); 

      // AddUpdateUserWithDefaults(
      //   DISPLAY_NAME_PAULCOPELAND,
      //   GetGenderTypeMale().Id,
      //   GetProfileTypeAdmin().Id,
      //   IDENTITY_SERVER_IDENTIFIER_PAULCOPELAND
      // ); 

      // AddUpdateUserWithDefaults(
      //   DISPLAY_NAME_SHARONIBBS,
      //   GetGenderTypeFemale().Id,
      //   GetProfileTypeAdmin().Id,
      //   IDENTITY_SERVER_IDENTIFIER_SHARONIBBS
      // ); 

      // AddUpdateUserWithDefaults(
      //   DISPLAY_NAME_PRIYANTHA,
      //   GetGenderTypeMale().Id,
      //   GetProfileTypeAdmin().Id,
      //   IDENTITY_SERVER_IDENTIFIER_PRIYANTHA
      // );                              
    }

    internal void SeedDataUat()
    {
      AddUpdateUserWithDefaults(
        DISPLAY_NAME_ADMIN,
        GetGenderTypeFemale().Id,
        GetProfileTypeAdmin().Id,
        IDENTITY_SERVER_IDENTIFIER_ADMIN,
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU)
      );

      AddUpdateUserWithDefaults(
        DISPLAY_NAME_AMHP_FEMALE, 
        GetGenderTypeFemale().Id, 
        GetProfileTypeAmhp().Id,
        IDENTITY_SERVER_IDENTIFIER_AMHP_FEMALE,
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU)
      );      

      AddUpdateUserWithDefaults(
        DISPLAY_NAME_AMHP_MALE, 
        GetGenderTypeMale().Id, 
        GetProfileTypeAmhp().Id,
        IDENTITY_SERVER_IDENTIFIER_AMHP_MALE,
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU)
      );      

      AddUpdateUserDoctorWithDefaults(
        DISPLAY_NAME_DOCTOR_FEMALE, 
        GetGenderTypeFemale().Id,
        GMCNUMBER_DOCTOR_FEMALE,
        IDENTITY_SERVER_IDENTIFIER_DOCTOR_FEMALE,
        GetSection12ApprovalStatusApproved().Id,
        SECTION_12_EXPIRY_DATE_DOCTOR_FEMALE,
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU)
      );

      AddUpdateUserDoctorWithDefaults(
        DISPLAY_NAME_DOCTOR_MALE,
        GetGenderTypeMale().Id,
        GMCNUMBER_DOCTOR_MALE,
        IDENTITY_SERVER_IDENTIFIER_DOCTOR_MALE,
        GetSection12ApprovalStatusApproved().Id,
        SECTION_12_EXPIRY_DATE_DOCTOR_MALE,
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU)
      );
    
      // AMHPS

      AddUpdateUserWithDefaults(
        DISPLAY_NAME_FINANCE,
        GetGenderTypeFemale().Id,
        GetProfileTypeFinance().Id,
        IDENTITY_SERVER_IDENTIFIER_FINANCE,
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU)
      );

      AddUpdateUserWithDefaults(
        "Amanda Pillar",
        GetGenderTypeFemale().Id,
        GetProfileTypeAmhp().Id,
        "f624a2f1-faee-4f06-8945-6061106a4f3c",
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU)
      );

      AddUpdateUserWithDefaults(
        "Andy Latham",
        GetGenderTypeMale().Id,
        GetProfileTypeAmhp().Id,
        "824e286e-ae36-4562-a3b0-6f8de4fe1a29",
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU)
      );

      AddUpdateUserWithDefaults(
        "Colin Burgess",
        GetGenderTypeMale().Id,
        GetProfileTypeAmhp().Id,
        "da285572-d5ad-4f1e-9ec6-7400f5bd6713",
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU)
      );

      AddUpdateUserWithDefaults(
        "Debbie Faulkner",
        GetGenderTypeFemale().Id,
        GetProfileTypeAmhp().Id,
        "63274866-13e6-4d9c-a809-5c63d1a0d4b1",
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU)
      );

      AddUpdateUserWithDefaults(
        "Denise Heatley",
        GetGenderTypeFemale().Id,
        GetProfileTypeAmhp().Id,
        "bc6b036a-a114-471e-9ef7-85876fffdd65",
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU)
      );

      AddUpdateUserWithDefaults(
        "Emma Jebb",
        GetGenderTypeFemale().Id,
        GetProfileTypeAmhp().Id,
        "d056d337-60b8-4068-b2f0-91f42ba22442",
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU)
      );

      AddUpdateUserWithDefaults(
        "Jo Willis",
        GetGenderTypeFemale().Id,
        GetProfileTypeAmhp().Id,
        "1684260a-1c78-444a-8bdb-5da1a1475778",
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU)
      ); 

      AddUpdateUserWithDefaults(
        "Laura Rushton",
        GetGenderTypeFemale().Id,
        GetProfileTypeAmhp().Id,
        "ff0a8f3c-d311-48c8-ae6e-0686b0e1765f",
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU)
      );

      AddUpdateUserWithDefaults(
        "Liane Devaney",
        GetGenderTypeFemale().Id,
        GetProfileTypeAmhp().Id,
        "50718d71-42d9-43ac-b3ee-da4e0ed843d7",
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU)
      );

      AddUpdateUserWithDefaults(
        "Matt Nixon",
        GetGenderTypeMale().Id,
        GetProfileTypeAmhp().Id,
        "b3d2c8c1-bafa-4927-8deb-c274029d39ed",
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU)
      ); 

      AddUpdateUserWithDefaults(
        "Nick Slater",
        GetGenderTypeFemale().Id,
        GetProfileTypeAmhp().Id,
        "bea9dc26-6826-44b9-b6d1-7f736e9eb349",
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU)
      );

      AddUpdateUserWithDefaults(
        "Rachael Witter",
        GetGenderTypeFemale().Id,
        GetProfileTypeAmhp().Id,
        "bdcb9d7d-2da3-41a1-9529-848413e209cc",
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU)
      );

      AddUpdateUserWithDefaults(
        "Tim Hamlett",
        GetGenderTypeFemale().Id,
        GetProfileTypeAmhp().Id,
        "e7ed5af2-8833-4b81-abfc-d5aa5adc13f4",
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU)
      );                                  

      AddUpdateUserWithDefaults(
        "Wes Machin",
        GetGenderTypeFemale().Id,
        GetProfileTypeAmhp().Id,
        "c309ba3e-269e-449f-9668-75a55dac52cd",
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU)
      );

      // Dummy GPs
      AddUpdateUserDoctorWithDefaults(
        "Dr Snow",
        GetGenderTypeMale().Id,
        3333301,
        "00000000-0000-0000-0000-000003333301",
        GetSection12ApprovalStatusApproved().Id,
        SECTION_12_EXPIRY_DATE_DOCTOR_MALE,
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU),
        GetProfileTypeGp().Id
      );                                                                                 

      AddUpdateUserDoctorWithDefaults(
        "Dr Bell",
        GetGenderTypeMale().Id,
        3333302,
        "00000000-0000-0000-0000-000003333302",
        GetSection12ApprovalStatusApproved().Id,
        SECTION_12_EXPIRY_DATE_DOCTOR_MALE,
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU),
        GetProfileTypeGp().Id
      );

      AddUpdateUserDoctorWithDefaults(
        "Dr White",
        GetGenderTypeMale().Id,
        3333303,
        "00000000-0000-0000-0000-000003333303",
        GetSection12ApprovalStatusApproved().Id,
        SECTION_12_EXPIRY_DATE_DOCTOR_MALE,
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU),
        GetProfileTypeGp().Id
      );

      AddUpdateUserDoctorWithDefaults(
        "Dr Black",
        GetGenderTypeMale().Id,
        3333304,
        "00000000-0000-0000-0000-000003333304",
        GetSection12ApprovalStatusApproved().Id,
        SECTION_12_EXPIRY_DATE_DOCTOR_MALE,
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU),
        GetProfileTypePsychiatrist().Id
      );

      AddUpdateUserDoctorWithDefaults(
        "Dr Brown",
        GetGenderTypeMale().Id,
        3333305,
        "00000000-0000-0000-0000-000003333305",
        GetSection12ApprovalStatusApproved().Id,
        SECTION_12_EXPIRY_DATE_DOCTOR_MALE,
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU),
        GetProfileTypePsychiatrist().Id
      );

      AddUpdateUserDoctorWithDefaults(
        "Dr Jones",
        GetGenderTypeFemale().Id,
        3333306,
        "00000000-0000-0000-0000-000003333306",
        GetSection12ApprovalStatusApproved().Id,
        SECTION_12_EXPIRY_DATE_DOCTOR_FEMALE,
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU),
        GetProfileTypeGp().Id
      );

      AddUpdateUserDoctorWithDefaults(
        "Dr Smith",
        GetGenderTypeFemale().Id,
        3333307,
        "00000000-0000-0000-0000-000003333307",
        GetSection12ApprovalStatusApproved().Id,
        SECTION_12_EXPIRY_DATE_DOCTOR_FEMALE,
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU),
        GetProfileTypeGp().Id
      );

      AddUpdateUserDoctorWithDefaults(
        "Dr Morris",
        GetGenderTypeFemale().Id,
        3333308,
        "00000000-0000-0000-0000-000003333308",
        GetSection12ApprovalStatusApproved().Id,
        SECTION_12_EXPIRY_DATE_DOCTOR_FEMALE,
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU),
        GetProfileTypeGp().Id
      );

      AddUpdateUserDoctorWithDefaults(
        "Dr Bailey",
        GetGenderTypeFemale().Id,
        3333309,
        "00000000-0000-0000-0000-000003333309",
        GetSection12ApprovalStatusApproved().Id,
        SECTION_12_EXPIRY_DATE_DOCTOR_FEMALE,
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU),
        GetProfileTypePsychiatrist().Id
      );

      AddUpdateUserDoctorWithDefaults(
        "Dr Taylor",
        GetGenderTypeFemale().Id,
        3333310,
        "00000000-0000-0000-0000-000003333310",
        GetSection12ApprovalStatusApproved().Id,
        SECTION_12_EXPIRY_DATE_DOCTOR_FEMALE,
        GetOrganisationIdByName(OrganisationSeeder.NAME_MLCSU),
        GetProfileTypePsychiatrist().Id
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
      DateTimeOffset? section12ExpiryDate = null,
      int? organisationId = null,
      int? profileTypeId = null
    )
    {
      User user = AddUpdateUserWithDefaults(
        displayName, 
        genderTypeId,
        profileTypeId ?? GetProfileTypeGp().Id,
        identityServerIdentifier: identityServerIdentifier,
        organisationId);

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