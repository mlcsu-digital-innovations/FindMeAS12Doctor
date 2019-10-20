using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class ExaminationSeeder : SeederBase<Examination>
  {
    // internal void SeedData()
    // {
    //   Examination examination;

    //   // examination for referral with a current examination with no allocated doctors or notification responses

    //   if ((examination = _context
    //     .Examinations
    //       .SingleOrDefault(g => g.Address1 ==
    //         EXAMINATION_ADDRESS_1)) == null)
    //   {
    //     examination = new Examination();
    //     _context.Add(examination);
    //   }
    //   examination.Address1 = EXAMINATION_ADDRESS_1;
    //   examination.CcgId = GetCcgByName(CcgSeeder.NORTH_STAFFORDSHIRE).Id;
    //   examination.CreatedByUserId = GetUserByDisplayName(USER_DISPLAY_NAME_AMHP_FEMALE).Id;
    //   examination.Postcode = "ST14 5PP";
    //   examination.ReferralId = GetReferralIdByPatientNhsNumber(PatientSeeder.NHS_NUMBER_CCG_NORTH_STAFFORDSHIRE);
    //   examination.SpecialityId = GetSpeciality(Models.Speciality.SECTION_12).Id;
    //   PopulateActiveAndModifiedWithSystemUser(examination);

    //   // examination for referral with a previous examination

    //   if ((examination = _context
    //     .Examinations
    //       .SingleOrDefault(g => g.Address1 ==
    //         EXAMINATION_ADDRESS_2)) == null)
    //   {
    //     examination = new Examination();
    //     _context.Add(examination);
    //   }
    //   examination.Address1 = EXAMINATION_ADDRESS_2;
    //   examination.CcgId = GetCcgByName(CcgSeeder.NORTH_STAFFORDSHIRE).Id;
    //   examination.CompletedByUser = GetSystemAdminUser();
    //   examination.CompletedTime = _now;
    //   examination.CreatedByUser = GetSystemAdminUser();
    //   examination.IsActive = true;
    //   examination.ModifiedAt = _now;
    //   examination.ModifiedByUser = GetSystemAdminUser();
    //   examination.Postcode = "ST2 9QL";
    //   examination.ReferralId =
    //     GetReferralIdByPatientNhsNumber(PatientSeeder.NHS_NUMBER_CCG_NORTH_STAFFORDSHIRE);
    //   examination.SpecialityId = GetSpeciality(Models.Speciality.SECTION_12).Id;

    //   // examinations for referral with both current and previous examinations

    //   if ((examination = _context
    //     .Examinations
    //       .SingleOrDefault(g => g.Address1 ==
    //         EXAMINATION_ADDRESS_3)) == null)
    //   {
    //     examination = new Examination();
    //     _context.Add(examination);
    //   }
    //   examination.Address1 = EXAMINATION_ADDRESS_3;
    //   examination.CcgId = GetCcgByName(CcgSeeder.NORTH_STAFFORDSHIRE).Id;
    //   examination.CompletedByUser = GetSystemAdminUser();
    //   examination.CompletedTime = _now;
    //   examination.CreatedByUser = GetSystemAdminUser();
    //   examination.IsActive = true;
    //   examination.ModifiedAt = _now;
    //   examination.ModifiedByUser = GetSystemAdminUser();
    //   examination.Postcode = "ST4 8ZH";
    //   examination.ReferralId =
    //     GetReferralIdByPatientNhsNumber(PatientSeeder.NHS_NUMBER_POTTERIES_MEDICAL_CENTRE);
    //   examination.SpecialityId = GetSpeciality(Models.Speciality.SECTION_12).Id;

    //   if ((examination = _context
    //     .Examinations
    //       .SingleOrDefault(g => g.Address1 ==
    //         EXAMINATION_ADDRESS_4)) == null)
    //   {
    //     examination = new Examination();
    //     _context.Add(examination);
    //   }
    //   examination.Address1 = EXAMINATION_ADDRESS_4;
    //   examination.CcgId = GetCcgByName(CcgSeeder.NORTH_STAFFORDSHIRE).Id;
    //   examination.CreatedByUser = GetSystemAdminUser();
    //   examination.IsActive = true;
    //   examination.ModifiedAt = _now;
    //   examination.ModifiedByUser = GetSystemAdminUser();
    //   examination.Postcode = "ST5 7UE";
    //   examination.ReferralId =
    //     GetReferralIdByPatientNhsNumber(PatientSeeder.NHS_NUMBER_POTTERIES_MEDICAL_CENTRE);
    //   examination.SpecialityId = GetSpeciality(Models.Speciality.SECTION_12).Id;

    //   // examination for referral with current examination and allocated doctors

    //   if ((examination = _context
    //     .Examinations
    //       .SingleOrDefault(g => g.Address1 ==
    //         EXAMINATION_ADDRESS_5)) == null)
    //   {
    //     examination = new Examination();
    //     _context.Add(examination);
    //   }
    //   examination.Address1 = EXAMINATION_ADDRESS_5;
    //   examination.CcgId = GetCcgByName(CcgSeeder.STOKE_ON_TRENT).Id;
    //   examination.CreatedByUser = GetSystemAdminUser();
    //   examination.IsActive = true;
    //   examination.ModifiedAt = _now;
    //   examination.ModifiedByUser = GetSystemAdminUser();
    //   examination.Postcode = "ST10 3JH";
    //   examination.ReferralId =
    //     GetReferralIdByAlternativeIdentifier(PatientSeeder.ALTERNATIVE_IDENTIFIER_POSTCODE_STOKE_ON_TRENT);
    //   examination.SpecialityId = GetSpeciality(Models.Speciality.SECTION_12).Id;

    //   // examination for referral with current examination and notification responses

    //   if ((examination = _context
    //     .Examinations
    //       .SingleOrDefault(g => g.Address1 ==
    //         EXAMINATION_ADDRESS_6)) == null)
    //   {
    //     examination = new Examination();
    //     _context.Add(examination);
    //   }
    //   examination.Address1 = EXAMINATION_ADDRESS_6;
    //   examination.CcgId = GetCcgByName(CcgSeeder.STOKE_ON_TRENT).Id;
    //   examination.CreatedByUser = GetSystemAdminUser();
    //   examination.IsActive = true;
    //   examination.ModifiedAt = _now;
    //   examination.ModifiedByUser = GetSystemAdminUser();
    //   examination.Postcode = "ST5 3DX";
    //   examination.ReferralId =
    //     GetReferralIdByAlternativeIdentifier(PatientSeeder.ALTERNATIVE_IDENTIFIER_STAFFORD_MEDICAL_CENTRE);
    //   examination.SpecialityId = GetSpeciality(Models.Speciality.SECTION_12).Id;

    //   // examination for referral with current examination and notification responses and allocated doctors

    //   if ((examination = _context
    //     .Examinations
    //       .SingleOrDefault(g => g.Address1 ==
    //         EXAMINATION_ADDRESS_7)) == null)
    //   {
    //     examination = new Examination();
    //     _context.Add(examination);
    //   }
    //   examination.Address1 = EXAMINATION_ADDRESS_7;
    //   examination.CcgId = GetCcgByName(CcgSeeder.STOKE_ON_TRENT).Id;
    //   examination.CreatedByUser = GetSystemAdminUser();
    //   examination.IsActive = true;
    //   examination.ModifiedAt = _now;
    //   examination.ModifiedByUser = GetSystemAdminUser();
    //   examination.Postcode = "ST6 7HG";
    //   examination.ReferralId =
    //     GetReferralIdByAlternativeIdentifier(PatientSeeder.ALTERNATIVE_IDENTIFIER_CCG_STOKE_ON_TRENT);
    //   examination.SpecialityId = GetSpeciality(Models.Speciality.SECTION_12).Id;
    // }
  }
}