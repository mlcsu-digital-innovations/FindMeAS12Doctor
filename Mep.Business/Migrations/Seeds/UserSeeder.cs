using Mep.Data.Entities;
using System.Linq;
using System;

namespace Mep.Business.Migrations.Seeds
{
  internal class UserSeeder : SeederBase<User>
  {
    internal void SeedData()
    {
      AddUpdateUserDoctorWithDefaults(
        displayName: USER_DISPLAY_NAME_DOCTOR_FEMALE, 
        genderTypeId: GetGenderTypeFemale().Id);

      AddUpdateUserDoctorWithDefaults(
        displayName: USER_DISPLAY_NAME_DOCTOR_MALE,
        genderTypeId: GetGenderTypeMale().Id);
    
      AddUpdateUserDoctorWithDefaults(
        displayName: USER_DISPLAY_NAME_DOCTOR_ON_CALL, 
        genderTypeId: GetGenderTypeFemale().Id);

      AddUpdateUserDoctorWithDefaults(
        displayName: USER_DISPLAY_NAME_DOCTOR_S12_APPROVED,
        genderTypeId: GetGenderTypeMale().Id,
        section12ApprovalStatusId: GetSection12ApprovalStatusApproved().Id,
        section12ExpiryDate: DateTimeOffset.Now.AddYears(10));

      AddUpdateUserDoctorWithDefaults(
        displayName: USER_DISPLAY_NAME_DOCTOR_PATIENTS_GP,
        genderTypeId: GetGenderTypeMale().Id);

      AddUpdateUserWithDefaults(
        displayName: USER_DISPLAY_NAME_FINANCE_FEMALE,
        genderTypeId: GetGenderTypeFemale().Id,
        profileTypeId: GetProfileTypeFinance().Id);     

      AddUpdateUserWithDefaults(
        displayName: USER_DISPLAY_NAME_FINANCE_MALE,
        genderTypeId: GetGenderTypeMale().Id,
        profileTypeId: GetProfileTypeFinance().Id);

      AddUpdateUserWithDefaults(
        displayName: USER_DISPLAY_NAME_AMHP_FEMALE, 
        genderTypeId: GetGenderTypeFemale().Id, 
        profileTypeId: GetProfileTypeAmhp().Id);      

      AddUpdateUserWithDefaults(
        displayName: USER_DISPLAY_NAME_AMHP_MALE, 
        genderTypeId: GetGenderTypeMale().Id, 
        profileTypeId: GetProfileTypeAmhp().Id);
    }

    private User AddUpdateUserWithDefaults(
      string displayName,
      int genderTypeId,
      int profileTypeId,
      int? organisationId = null)
    {
      User user;
      if ((user = _context.Users
                          .SingleOrDefault(g => g.DisplayName == displayName)) == null)
      {
        user = new User();
        _context.Add(user);
      }
      
      user.DisplayName = displayName;
      user.GenderTypeId = genderTypeId;                  
      user.GmcNumber = null;
      user.HasReadTermsAndConditions = true;
      user.IdentityServerIdentifier = Guid.NewGuid().ToString();  
      user.OrganisationId = organisationId.HasValue ? 
                            (int)organisationId : 
                            GetOrganisationIdByName(ORGANISATION_NAME_1);
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
  }
}