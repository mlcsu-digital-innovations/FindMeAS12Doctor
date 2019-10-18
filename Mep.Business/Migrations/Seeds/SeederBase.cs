using Mep.Data.Entities;
using System.Linq;
using System;
using Microsoft.Extensions.Configuration;

namespace Mep.Business.Migrations.Seeds
{
  public class SeederBase
  {
    #region CONSTANTS
    protected const int BANK_DETAILS_ACCOUNT_NUMBER = 10000000;
    protected const string BANK_DETAILS_BANK_NAME = "Bank Name 1";
    protected const string BANK_DETAILS_NAME_ON_ACCOUNT = "Name on Account 1";
    protected const int BANK_DETAILS_SORT_CODE = 100000;
    protected const int BANK_DETAILS_VRS_NUMBER = 1000000000;
    protected const string CCG_NAME_STOKE_ON_TRENT = "NHS Stoke on Trent CCG";
    protected const string CCG_NAME_NORTH_STAFFORDSHIRE = "NHS North Staffordshire CCG";
    protected const string CLAIM_STATUS_ACCEPTED_NAME = "Accepted";
    protected const string CLAIM_STATUS_ACCEPTED_DESCRIPTION = "Accepted";
    protected const string CONTACT_DETAIL_ADDRESS_1 = "Contact Detail Address 1";
    protected const string CONTACT_DETAIL_TYPE_NAME = "Contact Detail Type";
    protected const string CONTACT_DETAIL_TYPE_DESCRIPTION = "Contact Detail Type Description";
    protected const string EMAIL_ADDRESS = "mlcsu.digitalinnovations@nhs.net";
    protected const string EXAMINATION_ADDRESS_1 = "Examination Address 1";
    protected const string EXAMINATION_ADDRESS_2 = "Examination Address 2";
    protected const string EXAMINATION_ADDRESS_3 = "Examination Address 3";
    protected const string EXAMINATION_ADDRESS_4 = "Examination Address 4";
    protected const string EXAMINATION_ADDRESS_5 = "Examination Address 5";
    protected const string EXAMINATION_ADDRESS_6 = "Examination Address 6";
    protected const string EXAMINATION_ADDRESS_7 = "Examination Address 7";
    protected const string EXAMINATION_TYPE_AGRESSIVE_NEIGHBOUR_NAME = "Agressive neighbour";
    protected const string EXAMINATION_TYPE_AGRESSIVE_NEIGHBOUR_DESCRIPTION =
      "There is an agressive neighbour at the location";
    protected const string EXAMINATION_TYPE_DANGEROUS_ANIMAL_NAME = "Dangerous animal";
    protected const string EXAMINATION_TYPE_DANGEROUS_ANIMAL_DESCRIPTION =
      "A dangerous animal has been reported to be present on the premises";
    protected const string EXAMINATION_TYPE_DIFFICULT_PARKING_NAME = "Parking is difficult";
    protected const string EXAMINATION_TYPE_DIFFICULT_PARKING_DESCRIPTION =
      "Parking is difficult at the location";      
    protected const string GENDER_TYPE_DESCRIPTION_FEMALE = "Female";
    protected const string GENDER_TYPE_DESCRIPTION_MALE = "Male";
    protected const string GENDER_TYPE_DESCRIPTION_OTHER = "Other";
    protected const string GENDER_TYPE_NAME_FEMALE = "Female";
    protected const string GENDER_TYPE_NAME_MALE = "Male";
    protected const string GENDER_TYPE_NAME_OTHER = "Other";
    protected const string GP_PRACTICE_NAME_1 = "POTTERIES MEDICAL CENTRE";
    protected const string GP_PRACTICE_NAME_2 = "STAFFORD MEDICAL CENTRE";
    protected const decimal LATITUDE = 0.000000m;
    protected const decimal LONGITUDE = 0.000000m;
    protected const string NON_PAYMENT_LOCATION_TYPE_NAME = "Non Payment Location Type Name";
    protected const string NON_PAYMENT_LOCATION_TYPE_DESCRIPTION = "Non Payment Location Type Description";
    protected const string NOTIFICATION_TEXT_DESCRIPTION_ASSIGNED_TO_EXAMINATION = "Assigned to examination description";
    protected const string NOTIFICATION_TEXT_DESCRIPTION_2 = "Notification Text Description 2";
    protected const string NOTIFICATION_TEXT_MESSAGE_TEMPLATE_ASSIGNED_TO_EXAMINATION = "Assigned to examination Template";
    protected const string NOTIFICATION_TEXT_MESSAGE_TEMPLATE_2 = "Notification Text Message Template 2";
    protected const string NOTIFICATION_TEXT_NAME_ASSIGNED_TO_EXAMINATION = "Assigned to examination";
    protected const string NOTIFICATION_TEXT_NAME_2 = "Notification Text 2";
    protected const string USER_DISPLAY_NAME_FINANCE_FEMALE = "Finance Female";
    protected const string USER_DISPLAY_NAME_FINANCE_MALE = "Finance Male";
    protected const string USER_DISPLAY_NAME_AMHP_FEMALE = "Amhp Female";
    protected const string USER_DISPLAY_NAME_AMHP_MALE = "Amhp Male";
    protected const string ORGANISATION_DESCRIPTION_1 = "Organisation 1 Description";
    protected const string ORGANISATION_DESCRIPTION_2 = "Organisation 2 Description";
    protected const string ORGANISATION_DESCRIPTION_3 = "Organisation 3 Description";
    protected const string ORGANISATION_DESCRIPTION_4 = "Organisation 4 Description";
    protected const string ORGANISATION_DESCRIPTION_SYSTEM_ADMIN = "System Organisation Description";
    protected const string ORGANISATION_NAME_1 = "Organisation 1";
    protected const string ORGANISATION_NAME_2 = "Organisation 2";
    protected const string ORGANISATION_NAME_3 = "Organisation 3";
    protected const string ORGANISATION_NAME_4 = "Organisation 4";
    protected const string ORGANISATION_NAME_SYSTEM_ADMIN = "System Organisation";
    protected const string PATIENT_ALTERNATIVE_IDENTIFIER_1 = "Test Patient #1";
    protected const string PATIENT_ALTERNATIVE_IDENTIFIER_2 = "Test Patient #2";
    protected const string PATIENT_ALTERNATIVE_IDENTIFIER_3 = "Test Patient #3";
    protected const string PATIENT_ALTERNATIVE_IDENTIFIER_4 = "Test Patient #4";
    protected const string PATIENT_ALTERNATIVE_IDENTIFIER_5 = "Test Patient #5";
    protected const string PATIENT_ALTERNATIVE_IDENTIFIER_6 = "Test Patient #6";
    protected const string PATIENT_ALTERNATIVE_IDENTIFIER_7 = "Test Patient #7";
    protected const string PATIENT_ALTERNATIVE_IDENTIFIER_8 = "Test Patient #8";
    protected const long PATIENT_NHS_NUMBER_1 = 9486844275;
    protected const long PATIENT_NHS_NUMBER_2 = 9657966272;
    protected const long PATIENT_NHS_NUMBER_3 = 9070304333;
    protected const long PATIENT_NHS_NUMBER_4 = 9813607416;
    protected const string PAYMENT_METHOD_TYPE_DESCRIPTION = "Payment Method Type Description";
    protected const string PAYMENT_METHOD_TYPE_NAME = "Payment Method Type Name";
    protected const string PAYMENT_RULE_CRITERIA_1 = "Payment Rule Criteria 1";
    protected const string PAYMENT_RULE_DESCRIPTION_1 = "Payment Rule Description 1";
    protected const string PAYMENT_RULE_NAME_1 = "Payment Rule 1";
    protected const string PAYMENT_RULE_SET_DESCRIPTION = "Payment Rule Set Description";
    protected const string PAYMENT_RULE_SET_NAME = "Payment Rule Set Name";
    protected const string POSTCODE = "AB12CD";
    protected const string PROFILE_TYPE_DESCRIPTION_AMHP = "Profile Type Description AMHP";
    protected const string PROFILE_TYPE_DESCRIPTION_DOCTOR = "ProfileType Description Doctor";
    protected const string PROFILE_TYPE_DESCRIPTION_FINANCE = "ProfileType Description Finance";
    protected const string PROFILE_TYPE_DESCRIPTION_SYSTEM = "ProfileType Description System";
    protected const string PROFILE_TYPE_NAME_AMHP = "ProfileType Name AMHP";
    protected const string PROFILE_TYPE_NAME_DOCTOR = "ProfileType Name Doctor";
    protected const string PROFILE_TYPE_NAME_FINANCE = "ProfileType Name Finance";
    protected const string PROFILE_TYPE_NAME_SYSTEM = "ProfileType Name System";
    protected const string REFERRAL_STATUS_DESCRIPTION_NEW_REFERRAL = "New Referral Description";
    protected const string REFERRAL_STATUS_NAME_NEW_REFERRAL = "New Referral";
    protected const string SECTION_12_APPROVAL_STATUS_APPROVED_DESCRIPTION = "Section 12 Status Is Approved";
    protected const string SECTION_12_APPROVAL_STATUS_APPROVED_NAME = "Approved";
    protected const string SPECIALITY_SECTION_12 = "Section 12";
    protected const string SYSTEM_ADMIN_IDENTITY_SERVER_IDENTIFIER = "bf673270-2538-4e59-9d26-5b4808fd9ef6";
    protected const int TELEPHONE_NUMBER = 01000100000;
    protected const string TOWN = "Test Town";
    protected const string UNSUCCESSFUL_EXAMINATION_TYPE_DESCRIPTION = "Unsuccessful Examination Type Description";
    protected const string UNSUCCESSFUL_EXAMINATION_TYPE_NAME = "Unsuccessful Examination Type Name";
    protected const string USER_COMMENTS = "Test Comments";
    protected const string USER_DISPLAY_NAME_DOCTOR_FEMALE = "Doctor Female";
    protected const string USER_DISPLAY_NAME_DOCTOR_PATIENTS_GP = "Doctor Patients GP";
    protected const string USER_DISPLAY_NAME_DOCTOR_MALE = "Doctor Male";
    protected const string USER_DISPLAY_NAME_DOCTOR_ON_CALL = "Doctor On Call";
    protected const string USER_DISPLAY_NAME_DOCTOR_S12_APPROVED = "Doctor 12 Approved";
    protected const string USER_DISPLAY_NAME_FINANCE = "Finance";
    protected const string USER_DISPLAY_NAME_SYSTEM_ADMIN = "System Admin User";
#endregion
   
    protected static IConfiguration _config;
    protected static ApplicationContext _context;    
    protected DateTimeOffset _now = DateTimeOffset.Now;    
    private User _systemAdminUser = null;

    public SeederBase(){}

    protected int GetCcgIdByName(string CcgName)
    {
      try
      {
        return _context
          .Ccgs
            .Single(ccg => ccg.Name == CcgName).Id;
      }
      catch (Exception ex)
      {
        throw new Exception($"Cannot find a CCG with the name {CcgName} in Ccgs", ex);
      }
    }  

    protected int GetClaimStatusIdByClaimStatusName(string ClaimStatusName)
    {
      try
      {
        return _context
          .ClaimStatuses
            .Single(claimStatus => claimStatus.Name == ClaimStatusName).Id;
      }
      catch (Exception ex)
      {
        throw new Exception($"Cannot find a Claim Status with the name {ClaimStatusName} in ClaimStatuses", ex);
      }
    }

    protected int GetContactDetailTypeIdByContactDetailTypeName(string ContactDetailTypeName)
    {
      try
      {
        return _context
          .ContactDetailTypes
            .Single(contactDetailType => contactDetailType.Name == ContactDetailTypeName).Id;
      }
      catch (Exception ex)
      {
        throw new Exception($"Cannot find a Contact Detail Type with the name of {ContactDetailTypeName} in ContactDetailTypes", ex);
      }
    }

    protected int GetExaminationIdByExaminationAddress(string examinationAddress)
    {
      try
      {
        return _context
          .Examinations
            .Single(examination => examination.Address1 == examinationAddress).Id;
      }
      catch (Exception ex)
      {
        throw new Exception(
          $"Cannot find examination with an Address1 of {examinationAddress} in Examinations", ex);
      }
    }    

    protected int GetFemaleGenderTypeId()
    {
      try
      {
        return _context
          .GenderTypes
            .Single(gender => gender.Name == GENDER_TYPE_NAME_FEMALE).Id;
      }
      catch (Exception ex)
      {
        throw new Exception("Cannot find Gender Type female in GenderTypes", ex);
      }
    }

    protected GenderType GetGenderTypeById(int id)
    {
      try
      {
        return _context.GenderTypes.Single(gt => gt.Id == id);
      }
      catch (Exception ex)
      {
        throw new Exception($"Cannot find Gender Type with and id of {id} in GenderTypes", ex);
      }
    }
    protected GenderType GetGenderTypeFemale()
    {
      return GetGenderTypeById(Models.GenderType.FEMALE);
    }    
    protected GenderType GetGenderTypeMale()
    {
      return GetGenderTypeById(Models.GenderType.MALE);
    }
    protected GenderType GetGenderTypeOther()
    {
      return GetGenderTypeById(Models.GenderType.OTHER);
    }

    protected int GetFirstCcg()
    {
      try
      {
        return _context
          .Ccgs
            .First().Id;
      }
      catch (Exception ex)
      {
        throw new Exception("Cannot find CCG", ex);
      }
    }

    protected GpPractice GetGpPracticeByName(string gpPracticeName)
    {
      try
      {

        return _context.GpPractices.Single(gpPractice => gpPractice.Name == gpPracticeName);
      }
      catch (Exception ex)
      {
        throw new Exception(
          $"Cannot find a GP Practice with the name {gpPracticeName} in GpPractices", ex);
      }
    }
    protected int GetGpPracticeIdByName(string gpPracticeName)
    {
      return GetGpPracticeByName(gpPracticeName).Id;
    }

    protected int GetMaleGenderTypeId()
    {
      try
      {
        return _context
          .GenderTypes
            .Single(gender => gender.Name == GENDER_TYPE_NAME_MALE).Id;
      }
      catch (Exception ex)
      {
        throw new Exception("Cannot find Gender Type male in GenderTypes", ex);
      }
    }

    protected int GetNonPaymentLocationTypeIdByNonPaymentLocationTypeName(string nonPaymentLocationTypeName)
    {
      try
      {
        return _context
          .NonPaymentLocationTypes
            .Single(nonPaymentLocationType => nonPaymentLocationType.Name == nonPaymentLocationTypeName).Id;
      }
      catch (Exception ex)
      {
        throw new Exception(
          $"Cannot find Non Payment Location Type with the name of {nonPaymentLocationTypeName} in NonPaymentLocationTypes", ex);
      }
    }

    protected int GetNotificationTextId(string notificationTextName)
    {
      try
      {
        return _context
          .NotificationTexts
            .Single(notificationText => notificationText.Name == notificationTextName).Id;
      }
      catch (Exception ex)
      {
        throw new Exception($"Cannot find {notificationTextName} in NotificationTexts", ex);
      }
    }

    protected int GetOrganisationIdByName(string name)
    {
      try
      {
        return _context
          .Organisations
            .Single(organisation => organisation.Name == name).Id;
      }
      catch (Exception ex)
      {
        throw new Exception($"Cannot find an organisation with the Name {name} in Organisations", ex);
      }
    }

    protected int GetOtherGenderTypeId()
    {
      try
      {
        return _context
          .GenderTypes
            .Single(gender => gender.Name == GENDER_TYPE_NAME_OTHER).Id;
      }
      catch (Exception ex)
      {
        throw new Exception("Cannot find Gender Type other in GenderTypes", ex);
      }
    }

    protected int GetPatientIdByAlternativeIdentifier(string alternativeIdentifier)
    {
      try
      {
        return _context
          .Patients
            .Single(patient => patient.AlternativeIdentifier == alternativeIdentifier).Id;
      }
      catch (Exception ex)
      {
        throw new Exception(
          $"Cannot find a Patient with an Alternative Identifier of {alternativeIdentifier} in Patients", ex);
      }
    }

    protected int GetPatientIdByNhsNumber(long nhsNumber)
    {
      try
      {
        return _context
          .Patients
            .Single(patient => patient.NhsNumber == nhsNumber).Id;
      }
      catch (Exception ex)
      {
        throw new Exception(
          $"Cannot find a Patient with an NHS Number of {nhsNumber} in Patients", ex);
      }
    }

    protected int GetPaymentMethodTypeIdByPaymentMethodTypeName(string paymentMethodTypeName)
    {
      try
      {
        return _context
          .PaymentMethodTypes
            .Single(paymentMethodType => paymentMethodType.Name == paymentMethodTypeName).Id;
      }
      catch (Exception ex)
      {
        throw new Exception(
          $"Cannot find Payment Method Type with the name of {paymentMethodTypeName} in PaymentMethodTypes", ex);
      }
    }

    protected int GetPaymentRuleSetIdByPaymentRuleSetName(string paymentRuleSetName)
    {
      try
      {
        return _context
          .PaymentRuleSets
            .Single(paymentRuleSet => paymentRuleSet.Name == paymentRuleSetName).Id;
      }
      catch (Exception ex)
      {
        throw new Exception(
          $"Cannot find Payment Rule Set with the name of {paymentRuleSetName} in PaymentRuleSets", ex);
      }
    }

    protected int GetProfileTypeId(string profileTypeName)
    {
      try
      {
        return _context
          .ProfileTypes
            .Single(profileType => profileType.Name == profileTypeName).Id;
      }
      catch (Exception ex)
      {
        throw new Exception(
          $"Cannot find a Profile Type with the name {profileTypeName} in ProfileTypes", ex);
      }
    }

    protected ProfileType GetProfileTypeById(int id)
    {
      try
      {
        return _context.ProfileTypes
          .Single(profileType => profileType.Id == id);
      }
      catch (Exception ex)
      {
        throw new Exception(
          $"Cannot find a Profile Type with an id of {id} in ProfileTypes", ex);
      }
    }
    protected ProfileType GetProfileTypeAmhp()
    {
      return GetProfileTypeById(Models.ProfileType.AMHP);
    }

    protected ProfileType GetProfileTypeDoctor()
    {
      return GetProfileTypeById(Models.ProfileType.DOCTOR);
    }

    protected ProfileType GetProfileTypeFinance()
    {
      return GetProfileTypeById(Models.ProfileType.FINANCE);
    }
    protected ProfileType GetProfileTypeSystem()
    {
      return GetProfileTypeById(Models.ProfileType.SYSTEM);
    }

    protected int GetReferralIdByAlternativeIdentifier(string alternativeIdentifier)
    {
      try
      {
        return _context
          .Referrals
            .Single(referral => referral.PatientId == GetPatientIdByAlternativeIdentifier(alternativeIdentifier)).Id;
      }
      catch (Exception ex)
      {
        throw new Exception(
          $"Cannot find a referral that has a Patient with an Alternative Identifier of {alternativeIdentifier} in Referrals", ex);
      }
    }

    protected int GetReferralIdByPatientNhsNumber(long nhsNumber)
    {
      try
      {
        return _context
          .Referrals
            .Single(referral => referral.PatientId == GetPatientIdByNhsNumber(nhsNumber)).Id;
      }
      catch (Exception ex)
      {
        throw new Exception(
          $"Cannot find a Referral that has a Patient with an NHS Number of {nhsNumber} in Referrals", ex);
      }
    }

    protected int GetReferralStatusId()
    {
      try
      {
        return _context
          .ReferralStatuses
            .Single(referralStatus => referralStatus.Name == REFERRAL_STATUS_NAME_NEW_REFERRAL).Id;
      }
      catch (Exception ex)
      {
        throw new Exception(
          $"Cannot find a Referral Status with the name {REFERRAL_STATUS_NAME_NEW_REFERRAL} in ReferralStatuses", ex);
      }
    }

    protected int GetSection12ApprovalStatusIdBySection12ApprovalStatusName(string section12ApprovalStatusName)
    {
      try
      {
        return _context
          .Section12ApprovalStatuses
            .Single(section12ApprovalStatus => section12ApprovalStatus.Name == section12ApprovalStatusName).Id;
      }
      catch (Exception ex)
      {
        throw new Exception(
          $"Cannot find Section 12 Approval Status with the name of {section12ApprovalStatusName} in Section12ApprovalStatuses", ex);
      }
    }

    protected Section12ApprovalStatus GetSection12ApprovalStatusById(int id)
    {
      try
      {
        return _context.Section12ApprovalStatuses
                       .Single(s => s.Id == id);
      }
      catch (Exception ex)
      {
        throw new Exception(
          $"Cannot find Section 12 Approval Status with an id of {id} in Section12ApprovalStatuses", ex);
      }
    }
    
    protected Section12ApprovalStatus GetSection12ApprovalStatusApproved()
    {
      return GetSection12ApprovalStatusById(Models.Section12ApprovalStatus.APPROVED);
    }
    

    protected int GetSpecialityId()
    {
      try
      {
        return _context
          .Specialities
            .Single(speciality => speciality.Name == SPECIALITY_SECTION_12).Id;
      }
      catch (Exception ex)
      {
        throw new Exception(
          $"Cannot find a Speciality with the name {SPECIALITY_SECTION_12} in Speciality", ex);
      }
    }

    protected User GetSystemAdminUser()
    {
      if (_systemAdminUser == null)
      {
        _systemAdminUser = _context
          .Users
          .SingleOrDefault(u => u.IdentityServerIdentifier == 
                                SYSTEM_ADMIN_IDENTITY_SERVER_IDENTIFIER);
      }
      return _systemAdminUser;
    }

    protected int GetUserIdByDisplayname(string displayName)
    {
      try
      {
        return _context
          .Users
            .Single(user => user.DisplayName == displayName).Id;
      }
      catch (Exception ex)
      {
        throw new Exception(
          $"Cannot find a user with the Display Name {displayName} in Users", ex);
      }
    }

    protected void PopulateActiveAndModifiedWithSystemUser(BaseEntity entity)
    {
      entity.IsActive = true;
      entity.ModifiedAt = _now;
      entity.ModifiedByUser = GetSystemAdminUser();
    }

    protected void PopulateNameDescriptionActiveAndModifiedWithSystemUser(
      NameDescription entity,
      string name,
      string description)
      {
        entity.Name = name;
        entity.Description = description;
        PopulateActiveAndModifiedWithSystemUser(entity);
      }
  }
}