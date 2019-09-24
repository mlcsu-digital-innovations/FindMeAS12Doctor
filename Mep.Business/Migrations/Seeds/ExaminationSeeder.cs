using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class ExaminationSeeder : SeederBase
  {
    internal ExaminationSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      Examination examination;

      Referral referral1 = _context.Referrals.Where(referral => referral.PatientId == _patient1.Id).FirstOrDefault();
      Referral referral2 = _context.Referrals.Where(referral => referral.PatientId == _patient2.Id).FirstOrDefault();
      Referral referral3 = _context.Referrals.Where(referral => referral.PatientId == _patient3.Id).FirstOrDefault();
      Referral referral4 = _context.Referrals.Where(referral => referral.PatientId == _patient4.Id).FirstOrDefault();
      Referral referral5 = _context.Referrals.Where(referral => referral.PatientId == _patient5.Id).FirstOrDefault();
      Referral referral6 = _context.Referrals.Where(referral => referral.PatientId == _patient6.Id).FirstOrDefault();
      Referral referral7 = _context.Referrals.Where(referral => referral.PatientId == _patient7.Id).FirstOrDefault();

      // examination for referral with a current examination with no allocated doctors or notification responses

      if ((examination =
        _context.Examinations
          .SingleOrDefault(g => g.Address1 == "Examination Address 1")) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = "Examination Address 1";
      examination.CcgId = 1;
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST14 5PP";
      examination.ReferralId = referral1.Id;
      examination.SpecialityId = 1;

      // examination for referral with a previous examination

      if ((examination =
        _context.Examinations
          .SingleOrDefault(g => g.Address1 == "Examination Address 2")) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = "Examination Address 2";
      examination.CcgId = 1;
      examination.CompletedByUser = GetSystemAdminUser();
      examination.CompletedTime = _now;
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST2 9QL";
      examination.ReferralId = referral2.Id;
      examination.SpecialityId = 1;

      // examinations for referral with both current and previous examinations

      if ((examination =
        _context.Examinations
          .SingleOrDefault(g => g.Address1 == "Examination Address 3")) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = "Examination Address 3";
      examination.CcgId = 1;
      examination.CompletedByUser = GetSystemAdminUser();
      examination.CompletedTime = _now;
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST4 8ZH";
      examination.ReferralId = referral4.Id;;
      examination.SpecialityId = 1;

      if ((examination =
        _context.Examinations
          .SingleOrDefault(g => g.Address1 == "Examination Address 4")) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = "Examination Address 4";
      examination.CcgId = 1;
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST5 7UE";
      examination.ReferralId = referral4.Id;;
      examination.SpecialityId = 1;

      // examination for referral with current examination and allocated doctors

      if ((examination =
        _context.Examinations
          .SingleOrDefault(g => g.Address1 == "Examination Address 5")) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = "Examination Address 5";
      examination.CcgId = 1;
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST10 3JH";
      examination.ReferralId = referral5.Id;;
      examination.SpecialityId = 1;

      // examination for referral with current examination and notification responses

      if ((examination =
        _context.Examinations
          .SingleOrDefault(g => g.Address1 == "Examination Address 6")) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = "Examination Address 6";
      examination.CcgId = 1;
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST5 3DX";
      examination.ReferralId = referral6.Id;;
      examination.SpecialityId = 1;

      // examination for referral with current examination and notification responses and allocated doctors

      if ((examination =
        _context.Examinations
          .SingleOrDefault(g => g.Address1 == "Examination Address 7")) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = "Examination Address 7";
      examination.CcgId = 1;
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST6 7HG";
      examination.ReferralId = referral7.Id;;
      examination.SpecialityId = 1;
    }
  }
}