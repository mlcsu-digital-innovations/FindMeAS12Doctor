using Mep.Data.Entities;
using System.Linq;
using System;

namespace Mep.Business.Migrations.Seeds
{
  public class SeederBase
  {
    protected DateTimeOffset _now = DateTimeOffset.Now;

    protected const string EXAMINATIONADDRESS1 = "Examination Address 1";
    protected const string EXAMINATIONADDRESS2 = "Examination Address 2";
    protected const string EXAMINATIONADDRESS3 = "Examination Address 3";
    protected const string EXAMINATIONADDRESS4 = "Examination Address 4";
    protected const string EXAMINATIONADDRESS5 = "Examination Address 5";
    protected const string EXAMINATIONADDRESS6 = "Examination Address 6";
    protected const string EXAMINATIONADDRESS7 = "Examination Address 7";
    protected const string FEMALE = "Female";
    protected const string MALE = "Male";
    protected const string NOTIFICATIONTEXT1 = "Notification Text 1";
    protected const string NOTIFICATIONTEXT2 = "Notification Text 2";
    protected const string ORGANISATION1 = "Organisation 1";
    protected const string ORGANISATION1USER = "Org 1 User";
    protected const string ORGANISATION2 = "Organisation 2";
    protected const string ORGANISATION2USER = "Org 2 User";
    protected const string ORGANISATION3 = "Organisation 3";
    protected const string ORGANISATION3USER = "Org 3 User";
    protected const string ORGANISATION4 = "Organisation 4";
    protected const string ORGANISATION4USER = "Org 4 User";
    protected const string OTHER = "Other";
    protected const string PATIENTALTERNATIVEIDENTIFIER1 = "Test Patient #1";
    protected const string PATIENTALTERNATIVEIDENTIFIER2 = "Test Patient #2";
    protected const string PATIENTALTERNATIVEIDENTIFIER3 = "Test Patient #3";
    protected const string PATIENTALTERNATIVEIDENTIFIER4 = "Test Patient #4";
    protected const string PATIENTALTERNATIVEIDENTIFIER5 = "Test Patient #5";
    protected const string PATIENTALTERNATIVEIDENTIFIER6 = "Test Patient #6";
    protected const string PATIENTALTERNATIVEIDENTIFIER7 = "Test Patient #7";
    protected const string PATIENTALTERNATIVEIDENTIFIER8 = "Test Patient #8";
    protected const long PATIENTNHSNUMBER1 = 9486844275;
    protected const long PATIENTNHSNUMBER2 = 9657966272;
    protected const long PATIENTNHSNUMBER3 = 9070304333;
    protected const long PATIENTNHSNUMBER4 = 9813607416;
    protected const string REFERRALSTATUS = "New Referral";
    protected const string SPECIALITY = "Section 12";
    protected const string USERDISPLAYNAMEFEMALE = "Doctor Female";
    protected const string USERDISPLAYNAMEMALE = "Doctor Male";

    protected const string SystemAdminIdentityServerIdentifier = "bf673270-2538-4e59-9d26-5b4808fd9ef6";


    protected readonly ApplicationContext _context;

    protected int GetFirstCcg()
    {
      try
      {
        return _context.Ccgs.FirstOrDefault().Id;
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
        return _context.GenderTypes.Single(gender => gender.Name == FEMALE).Id;
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
        return _context.GenderTypes.Single(gender => gender.Name == MALE).Id;
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
        return _context.GenderTypes.Single(gender => gender.Name == OTHER).Id;
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
        return _context.ReferralStatuses.Single(referralStatus => referralStatus.Name == REFERRALSTATUS).Id;
      }
      catch (Exception ex)
      {
        throw new Exception("Cannot find New Referral in ReferralStatuses", ex);
      }
    }

    protected int GetSpecialityId()
    {
      try
      {
        return _context.Specialities.Single(speciality => speciality.Name == SPECIALITY).Id;
      }
      catch (Exception ex)
      {
        throw new Exception("Cannot find Section 12 in Speciality", ex);
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
        throw new Exception($"Cannot find patinet with an NHS Number of {alternativeIdentifier} in Patients", ex);
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