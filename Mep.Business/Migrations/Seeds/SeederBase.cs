using Mep.Data.Entities;
using System.Linq;
using System;

namespace Mep.Business.Migrations.Seeds
{
  public class SeederBase
  {
    protected DateTimeOffset _now = DateTimeOffset.Now;
    protected GenderType _femaleGender;
    protected GenderType _maleGender;
    protected Patient _patient1;
    protected Patient _patient2;
    protected Patient _patient3;
    protected Patient _patient4;
    protected Patient _patient5;
    protected Patient _patient6;
    protected Patient _patient7;
    protected ReferralStatus _referralStatus;
    protected Speciality _speciality;

    protected const string SystemAdminIdentityServerIdentifier = "bf673270-2538-4e59-9d26-5b4808fd9ef6";
    protected const string FEMALE = "Female";
    protected const string MALE = "Male";
    protected const string OTHER = "Other";
    protected const string EXAMINATIONADDRESS1 = "Examination Address 1";
    protected const string EXAMINATIONADDRESS2 = "Examination Address 2";
    protected const string EXAMINATIONADDRESS3 = "Examination Address 3";
    protected const string EXAMINATIONADDRESS4 = "Examination Address 4";
    protected const string EXAMINATIONADDRESS5 = "Examination Address 5";
    protected const string EXAMINATIONADDRESS6 = "Examination Address 6";
    protected const string EXAMINATIONADDRESS7 = "Examination Address 7";
    
    protected const long PATIENTNHSNUMBER1 = 9486844275;
    protected const long PATIENTNHSNUMBER2 = 9657966272;
    protected const long PATIENTNHSNUMBER3 = 9070304333;
    protected const long PATIENTNHSNUMBER4 = 9813607416;
    protected const string PATIENTALTERNATIVEIDENTIFIER1 = "Test Patient #1";
    protected const string PATIENTALTERNATIVEIDENTIFIER2 = "Test Patient #2";
    protected const string PATIENTALTERNATIVEIDENTIFIER3 = "Test Patient #3";
    protected const string PATIENTALTERNATIVEIDENTIFIER4 = "Test Patient #4";
    protected const string PATIENTALTERNATIVEIDENTIFIER5 = "Test Patient #5";
    protected const string PATIENTALTERNATIVEIDENTIFIER6 = "Test Patient #6";
    protected const string PATIENTALTERNATIVEIDENTIFIER7 = "Test Patient #7";
    protected const string PATIENTALTERNATIVEIDENTIFIER8 = "Test Patient #8";
    protected const string REFERRALSTATUS = "New Referral";
    protected const string SPECIALITY = "Section 12";
    protected const string NOTIFICATIONTEXT1 = "Notification Text 1";
    protected const string NOTIFICATIONTEXT2 = "Notification Text 2";
    

    protected readonly ApplicationContext _context;

    protected int GetFemaleGenderTypeId()
    {
      try {
        return _context.GenderTypes.Single(gender => gender.Name == FEMALE).Id;
      } catch (Exception ex) {
        throw new Exception("Cannot find female in GenderTypes", ex);
      }
    }

    protected int GetMaleGenderTypeId()
    {
      try {
        return _context.GenderTypes.Single(gender => gender.Name == MALE).Id;
      } catch (Exception ex) {
        throw new Exception("Cannot find male in GenderTypes", ex);
      }
    }

    protected int GetOtherGenderTypeId()
    {
      try {
        return _context.GenderTypes.Single(gender => gender.Name == OTHER).Id;
      } catch (Exception ex) {
        throw new Exception("Cannot find other in GenderTypes", ex);
      }
    }

    protected int GetReferralStatusId()
    {
      try {
        return _context.GenderTypes.Single(gender => gender.Name == REFERRALSTATUS).Id;
      } catch (Exception ex) {
        throw new Exception("Cannot find New Referral in ReferralStatuses", ex);
      }
    }

    protected int GetPatientIdByNhsNumber(long nhsNumber) {
      try {
        return _context.Patients.Single(patient => patient.NhsNumber == nhsNumber).Id;
      } catch (Exception ex) {
        throw new Exception($"Cannot find patinet with an NHS Number of {nhsNumber} in Patients", ex);
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