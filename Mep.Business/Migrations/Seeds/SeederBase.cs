using Mep.Data.Entities;
using System.Linq;
using System;

namespace Mep.Business.Migrations.Seeds
{
  public class SeederBase
  {
    protected Patient _patient1;
    protected Patient _patient2;
    protected Patient _patient3;
    protected Patient _patient4;
    protected Patient _patient5;
    protected Patient _patient6;
    protected Patient _patient7;

    protected ReferralStatus _referralStatus;
    protected GenderType _maleGender;
    protected GenderType _femaleGender;
    protected DateTimeOffset _now = DateTimeOffset.Now;

    protected const string SystemAdminIdentityServerIdentifier =
      "bf673270-2538-4e59-9d26-5b4808fd9ef6";
    protected readonly ApplicationContext _context;

    public SeederBase(ApplicationContext context)
    {
      _context = context;

      _patient1 = _context.Patients.Where(patient => patient.NhsNumber == 9486844275).FirstOrDefault();
      _patient2 = _context.Patients.Where(patient => patient.NhsNumber == 9657966272).FirstOrDefault();
      _patient3 = _context.Patients.Where(patient => patient.NhsNumber == 9070304333).FirstOrDefault();
      _patient4 = _context.Patients.Where(patient => patient.NhsNumber == 9813607416).FirstOrDefault();
      _patient5 = _context.Patients.Where(patient => patient.AlternativeIdentifier == "Test Patient #5").FirstOrDefault();
      _patient6 = _context.Patients.Where(patient => patient.AlternativeIdentifier == "Test Patient #6").FirstOrDefault();
      _patient7 = _context.Patients.Where(patient => patient.AlternativeIdentifier == "Test Patient #7").FirstOrDefault();

      _referralStatus = _context.ReferralStatuses.Where(referralStatus => referralStatus.Name == "New Referral").Single();
      _maleGender = _context.GenderTypes.Where(gender => gender.Name == "Male").Single();
      _femaleGender = _context.GenderTypes.Where(gender => gender.Name == "Female").Single();
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