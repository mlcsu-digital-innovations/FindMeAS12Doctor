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

      // examination for referral with a current examination with no allocated doctors or notification responses

      if ((examination =
        _context.Examinations
          .SingleOrDefault(g => g.Address1 == EXAMINATIONADDRESS1)) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = EXAMINATIONADDRESS1;
      examination.CcgId = GetFirstCcg();
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST14 5PP";
      examination.ReferralId = GetReferralIdByPatientNhsNumber(PATIENTNHSNUMBER1);
      examination.SpecialityId = GetSpecialityId();

      // examination for referral with a previous examination

      if ((examination =
        _context.Examinations
          .SingleOrDefault(g => g.Address1 == EXAMINATIONADDRESS2)) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = EXAMINATIONADDRESS2;
      examination.CcgId = GetFirstCcg();
      examination.CompletedByUser = GetSystemAdminUser();
      examination.CompletedTime = _now;
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST2 9QL";
      examination.ReferralId = GetReferralIdByPatientNhsNumber(PATIENTNHSNUMBER2);
      examination.SpecialityId = GetSpecialityId();

      // examinations for referral with both current and previous examinations

      if ((examination =
        _context.Examinations
          .SingleOrDefault(g => g.Address1 == EXAMINATIONADDRESS3)) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = EXAMINATIONADDRESS3;
      examination.CcgId = GetFirstCcg();
      examination.CompletedByUser = GetSystemAdminUser();
      examination.CompletedTime = _now;
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST4 8ZH";
      examination.ReferralId = GetReferralIdByPatientNhsNumber(PATIENTNHSNUMBER4);
      examination.SpecialityId = GetSpecialityId();

      if ((examination =
        _context.Examinations
          .SingleOrDefault(g => g.Address1 == EXAMINATIONADDRESS4)) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = EXAMINATIONADDRESS4;
      examination.CcgId = GetFirstCcg();
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST5 7UE";
      examination.ReferralId = GetReferralIdByPatientNhsNumber(PATIENTNHSNUMBER4);
      examination.SpecialityId = GetSpecialityId();

      // examination for referral with current examination and allocated doctors

      if ((examination =
        _context.Examinations
          .SingleOrDefault(g => g.Address1 == EXAMINATIONADDRESS5)) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = EXAMINATIONADDRESS5;
      examination.CcgId = GetFirstCcg();
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST10 3JH";
      examination.ReferralId = GetReferralIdByAlternativeIdentifier(PATIENTALTERNATIVEIDENTIFIER5);
      examination.SpecialityId = GetSpecialityId();

      // examination for referral with current examination and notification responses

      if ((examination =
        _context.Examinations
          .SingleOrDefault(g => g.Address1 == EXAMINATIONADDRESS6)) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = EXAMINATIONADDRESS6;
      examination.CcgId = GetFirstCcg();
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST5 3DX";
      examination.ReferralId = GetReferralIdByAlternativeIdentifier(PATIENTALTERNATIVEIDENTIFIER6);
      examination.SpecialityId = GetSpecialityId();

      // examination for referral with current examination and notification responses and allocated doctors

      if ((examination =
        _context.Examinations
          .SingleOrDefault(g => g.Address1 == EXAMINATIONADDRESS7)) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = EXAMINATIONADDRESS7;
      examination.CcgId = GetFirstCcg();
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST6 7HG";
      examination.ReferralId = GetReferralIdByAlternativeIdentifier(PATIENTALTERNATIVEIDENTIFIER7);
      examination.SpecialityId = GetSpecialityId();
    }
  }
}