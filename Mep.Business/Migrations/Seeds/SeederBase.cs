using Mep.Data.Entities;
using System.Linq;
using System;

namespace Mep.Business.Migrations.Seeds
{
  public class SeederBase
  {
    protected DateTimeOffset _now = DateTimeOffset.Now;

    protected const string EXAMINATION_ADDRESS_1 = "Examination Address 1";
    protected const string EXAMINATION_ADDRESS_2 = "Examination Address 2";
    protected const string EXAMINATION_ADDRESS_3 = "Examination Address 3";
    protected const string EXAMINATION_ADDRESS_4 = "Examination Address 4";
    protected const string EXAMINATION_ADDRESS_5 = "Examination Address 5";
    protected const string EXAMINATION_ADDRESS_6 = "Examination Address 6";
    protected const string EXAMINATION_ADDRESS_7 = "Examination Address 7";
    protected const string GENDER_TYPE_FEMALE = "Female";
    protected const string GENDER_TYPE_MALE = "Male";
    protected const string GENDER_TYPE_OTHER = "Other";
    protected const string NOTIFICATION_TEXT_1 = "Notification Text 1";
    protected const string NOTIFICATION_TEXT_2 = "Notification Text 2";
    protected const string ORGANISATION_1 = "Organisation 1";
    protected const string ORGANISATION_1_USER = "Org 1 User";
    protected const string ORGANISATION_2 = "Organisation 2";
    protected const string ORGANISATION_2_USER = "Org 2 User";
    protected const string ORGANISATION_3 = "Organisation 3";
    protected const string ORGANISATION_3_USER = "Org 3 User";
    protected const string ORGANISATION_4 = "Organisation 4";
    protected const string ORGANISATION_4_USER = "Org 4 User";
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
    protected const string PROFILE_TYPE_1 = "AMHP";
    protected const string PROFILE_TYPE_2 = "Doctor";
    protected const string PROFILE_TYPE_3 = "Finance";
    protected const string REFERRAL_STATUS_NEW_REFERRAL = "New Referral";
    protected const string SPECIALITY_SECTION_12 = "Section 12";
    protected const string USER_DISPLAY_NAME_FEMALE = "Doctor Female";
    protected const string USER_DISPLAY_NAME_MALE = "Doctor Male";

    protected const string SystemAdminIdentityServerIdentifier = "bf673270-2538-4e59-9d26-5b4808fd9ef6";

    protected readonly ApplicationContext _context;

    protected int GetFirstCcg()
    {
      try
      {
        return _context.Ccgs.First().Id;
      }
      catch (Exception ex)
      {
        throw new Exception("Cannot find CCG", ex);
      }
    }

    protected int GetFemaleGenderTypeId()
    {
      try
      {
        return _context.GenderTypes.Single(gender => gender.Name == GENDER_TYPE_FEMALE).Id;
      }
      catch (Exception ex)
      {
        throw new Exception("Cannot find female in GenderTypes", ex);
      }
    }

    protected int GetMaleGenderTypeId()
    {
      try
      {
        return _context.GenderTypes.Single(gender => gender.Name == GENDER_TYPE_MALE).Id;
      }
      catch (Exception ex)
      {
        throw new Exception("Cannot find male in GenderTypes", ex);
      }
    }

    protected int GetOtherGenderTypeId()
    {
      try
      {
        return _context.GenderTypes.Single(gender => gender.Name == GENDER_TYPE_OTHER).Id;
      }
      catch (Exception ex)
      {
        throw new Exception("Cannot find other in GenderTypes", ex);
      }
    }

    protected int GetReferralStatusId()
    {
      try
      {
        return _context.ReferralStatuses.Single(referralStatus => referralStatus.Name == REFERRAL_STATUS_NEW_REFERRAL).Id;
      }
      catch (Exception ex)
      {
        throw new Exception("Cannot find {REFERRAL_STATUS_NEW_REFERRAL} in ReferralStatuses", ex);
      }
    }

    protected int GetSpecialityId()
    {
      try
      {
        return _context.Specialities.Single(speciality => speciality.Name == SPECIALITY_SECTION_12).Id;
      }
      catch (Exception ex)
      {
        throw new Exception("Cannot find Section 12 in Speciality", ex);
      }
    }

    protected int GetProfileTypeId(string profileTypeName)
    {
      try
      {
        return _context.ProfileTypes.Single(profileType => profileType.Name == profileTypeName).Id;
      }
      catch (Exception ex)
      {
        throw new Exception("Cannot find {profileTypeName} in ProfileTypes", ex);
      }
    }

    protected int GetPatientIdByNhsNumber(long nhsNumber)
    {
      try
      {
        return _context.Patients.Single(patient => patient.NhsNumber == nhsNumber).Id;
      }
      catch (Exception ex)
      {
        throw new Exception($"Cannot find patinet with an NHS Number of {nhsNumber} in Patients", ex);
      }
    }

    protected int GetNotificationTextId(string notificationTextName)
    {
      try
      {
        return _context.NotificationTexts.Single(notificationText => notificationText.Name == notificationTextName).Id;
      }
      catch (Exception ex)
      {
        throw new Exception($"Cannot find {notificationTextName} in NotificationTexts", ex);
      }
    }

    protected int GetPatientIdByAlternativeIdentifier(string alternativeIdentifier)
    {
      try
      {
        return _context.Patients.Single(patient => patient.AlternativeIdentifier == alternativeIdentifier).Id;
      }
      catch (Exception ex)
      {
        throw new Exception($"Cannot find patinet with an Alternative Identifier of {alternativeIdentifier} in Patients", ex);
      }
    }

    protected int GetUserIdByDisplayname(string displayName)
    {
      try
      {
        return _context.Users.Single(user => user.DisplayName == displayName).Id;
      }
      catch (Exception ex)
      {
        throw new Exception($"Cannot find user {displayName} in Users", ex);
      }
    }

    protected int GetOrganisationIdByName(string name)
    {
      try
      {
        return _context.Organisations.Single(organisation => organisation.Name == name).Id;
      }
      catch (Exception ex)
      {
        throw new Exception($"Cannot find organisation {name} in Organisations", ex);
      }
    }

    protected int GetReferralIdByPatientNhsNumber(long nhsNumber)
    {
      try
      {
        return _context.Referrals.Single(referral => referral.PatientId == GetPatientIdByNhsNumber(nhsNumber)).Id;
      }
      catch (Exception ex)
      {
        throw new Exception($"Cannot find referral with an NHS Number of {nhsNumber} in Referrals", ex);
      }
    }

    protected int GetReferralIdByAlternativeIdentifier(string alternativeIdentifier)
    {
      try
      {
        return _context.Referrals.Single(referral => referral.PatientId == GetPatientIdByAlternativeIdentifier(alternativeIdentifier)).Id;
      }
      catch (Exception ex)
      {
        throw new Exception($"Cannot find referral with an Alternative Identifier of {alternativeIdentifier} in Referrals", ex);
      }
    }

    public SeederBase(ApplicationContext context)
    {
      _context = context;
    }

    protected User GetSystemAdminUser()
    {
      return _context.Users
        .SingleOrDefault(u =>
          u.IdentityServerIdentifier ==
            SystemAdminIdentityServerIdentifier);
    }
  }
}