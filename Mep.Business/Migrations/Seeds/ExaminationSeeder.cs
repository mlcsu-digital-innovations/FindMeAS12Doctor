using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class ExaminationSeeder : SeederBase
  {
    internal void SeedData()
    {
      Examination examination;

      // examination for referral with a current examination with no allocated doctors or notification responses

      if ((examination = _context
        .Examinations
          .SingleOrDefault(g => g.Address1 ==
            EXAMINATION_ADDRESS_1)) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = EXAMINATION_ADDRESS_1;
      examination.CcgId = GetCcgByName(CCG_NAME_NORTH_STAFFORDSHIRE).Id;
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST14 5PP";
      examination.ReferralId =
        GetReferralIdByPatientNhsNumber(PATIENT_NHS_NUMBER_1);
      examination.SpecialityId = GetSpecialityId();

      // examination for referral with a previous examination

      if ((examination = _context
        .Examinations
          .SingleOrDefault(g => g.Address1 ==
            EXAMINATION_ADDRESS_2)) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = EXAMINATION_ADDRESS_2;
      examination.CcgId = GetCcgByName(CCG_NAME_NORTH_STAFFORDSHIRE).Id;
      examination.CompletedByUser = GetSystemAdminUser();
      examination.CompletedTime = _now;
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST2 9QL";
      examination.ReferralId =
        GetReferralIdByPatientNhsNumber(PATIENT_NHS_NUMBER_2);
      examination.SpecialityId = GetSpecialityId();

      // examinations for referral with both current and previous examinations

      if ((examination = _context
        .Examinations
          .SingleOrDefault(g => g.Address1 ==
            EXAMINATION_ADDRESS_3)) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = EXAMINATION_ADDRESS_3;
      examination.CcgId = GetCcgByName(CCG_NAME_NORTH_STAFFORDSHIRE).Id;
      examination.CompletedByUser = GetSystemAdminUser();
      examination.CompletedTime = _now;
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST4 8ZH";
      examination.ReferralId =
        GetReferralIdByPatientNhsNumber(PATIENT_NHS_NUMBER_4);
      examination.SpecialityId = GetSpecialityId();

      if ((examination = _context
        .Examinations
          .SingleOrDefault(g => g.Address1 ==
            EXAMINATION_ADDRESS_4)) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = EXAMINATION_ADDRESS_4;
      examination.CcgId = GetCcgByName(CCG_NAME_NORTH_STAFFORDSHIRE).Id;
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST5 7UE";
      examination.ReferralId =
        GetReferralIdByPatientNhsNumber(PATIENT_NHS_NUMBER_4);
      examination.SpecialityId = GetSpecialityId();

      // examination for referral with current examination and allocated doctors

      if ((examination = _context
        .Examinations
          .SingleOrDefault(g => g.Address1 ==
            EXAMINATION_ADDRESS_5)) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = EXAMINATION_ADDRESS_5;
      examination.CcgId = GetCcgByName(CCG_NAME_STOKE_ON_TRENT).Id;
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST10 3JH";
      examination.ReferralId =
        GetReferralIdByAlternativeIdentifier(PATIENT_ALTERNATIVE_IDENTIFIER_5);
      examination.SpecialityId = GetSpecialityId();

      // examination for referral with current examination and notification responses

      if ((examination = _context
        .Examinations
          .SingleOrDefault(g => g.Address1 ==
            EXAMINATION_ADDRESS_6)) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = EXAMINATION_ADDRESS_6;
      examination.CcgId = GetCcgByName(CCG_NAME_STOKE_ON_TRENT).Id;
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST5 3DX";
      examination.ReferralId =
        GetReferralIdByAlternativeIdentifier(PATIENT_ALTERNATIVE_IDENTIFIER_6);
      examination.SpecialityId = GetSpecialityId();

      // examination for referral with current examination and notification responses and allocated doctors

      if ((examination = _context
        .Examinations
          .SingleOrDefault(g => g.Address1 ==
            EXAMINATION_ADDRESS_7)) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }
      examination.Address1 = EXAMINATION_ADDRESS_7;
      examination.CcgId = GetCcgByName(CCG_NAME_STOKE_ON_TRENT).Id;
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = _now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST6 7HG";
      examination.ReferralId =
        GetReferralIdByAlternativeIdentifier(PATIENT_ALTERNATIVE_IDENTIFIER_7);
      examination.SpecialityId = GetSpecialityId();
    }
  }
}