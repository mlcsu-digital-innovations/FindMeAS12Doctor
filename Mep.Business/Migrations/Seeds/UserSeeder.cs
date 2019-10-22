using Mep.Data.Entities;
using System.Linq;
using System;

namespace Mep.Business.Migrations.Seeds
{
  internal class UserSeeder : SeederBase<User>
  {
    #region Constants
    internal const string DISPLAY_NAME_AMHP_FEMALE = "Amhp Female";
    internal const string DISPLAY_NAME_AMHP_MALE = "Amhp Male";
    internal const string DISPLAY_NAME_DOCTOR_FEMALE = "Doctor Female";
    internal const string DISPLAY_NAME_DOCTOR_PATIENTS_GP = "Doctor Patients GP";
    internal const string DISPLAY_NAME_DOCTOR_MALE = "Doctor Male";
    internal const string DISPLAY_NAME_DOCTOR_ON_CALL = "Doctor On Call";
    internal const string DISPLAY_NAME_DOCTOR_S12_APPROVED = "Doctor 12 Approved";    
    internal const string DISPLAY_NAME_FINANCE_FEMALE = "Finance Female";
    internal const string DISPLAY_NAME_FINANCE_MALE = "Finance Male";
    internal readonly DateTimeOffset SECTION_12_EXPIRY_DATE_DOCTOR_12 =
      new DateTimeOffset(2025, 1, 1,
                         0, 00, 00, 00, DateTimeOffset.Now.Offset);   
    internal const string IDENTITY_SERVER_IDENTIFIER_SYSTEM_ADMIN = "bf673270-2538-4e59-9d26-5b4808fd9ef6";                          

    #endregion
    internal void SeedData()
    {
      AddUpdateUserDoctorWithDefaults(
        DISPLAY_NAME_DOCTOR_FEMALE, 
        GetGenderTypeFemale().Id
      );

      AddUpdateUserDoctorWithDefaults(
        DISPLAY_NAME_DOCTOR_MALE,
        GetGenderTypeMale().Id
      );
    
      AddUpdateUserDoctorWithDefaults(
        DISPLAY_NAME_DOCTOR_ON_CALL, 
        GetGenderTypeFemale().Id
      );

      AddUpdateUserDoctorWithDefaults(
        DISPLAY_NAME_DOCTOR_S12_APPROVED,
        GetGenderTypeMale().Id,
        section12ApprovalStatusId: GetSection12ApprovalStatusApproved().Id,
        section12ExpiryDate: SECTION_12_EXPIRY_DATE_DOCTOR_12
      );

      AddUpdateUserDoctorWithDefaults(
        DISPLAY_NAME_DOCTOR_PATIENTS_GP,
        GetGenderTypeMale().Id
      );

      AddUpdateUserWithDefaults(
        DISPLAY_NAME_FINANCE_FEMALE,
        GetGenderTypeFemale().Id,
        profileTypeId: GetProfileTypeFinance().Id
      );     

      AddUpdateUserWithDefaults(
        DISPLAY_NAME_FINANCE_MALE,
        GetGenderTypeMale().Id,
        profileTypeId: GetProfileTypeFinance().Id
      );

      AddUpdateUserWithDefaults(
        DISPLAY_NAME_AMHP_FEMALE, 
        GetGenderTypeFemale().Id, 
        profileTypeId: GetProfileTypeAmhp().Id
      );      

      AddUpdateUserWithDefaults(
        DISPLAY_NAME_AMHP_MALE, 
        GetGenderTypeMale().Id, 
        profileTypeId: GetProfileTypeAmhp().Id
      );
    }

    private User AddUpdateUserWithDefaults(
      string displayName,
      int genderTypeId,
      int profileTypeId,
      int? organisationId = null)
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
      user.IdentityServerIdentifier = Guid.NewGuid().ToString();  
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
      int? gmcNumber = null,
      int? section12ApprovalStatusId = null,
      DateTimeOffset? section12ExpiryDate = null
      )
    {
      User user = AddUpdateUserWithDefaults(
        displayName, 
        genderTypeId,
        GetProfileTypeDoctor().Id);

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