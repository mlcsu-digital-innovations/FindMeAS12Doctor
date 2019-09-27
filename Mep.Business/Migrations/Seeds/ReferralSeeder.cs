using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class ReferralSeeder : SeederBase
  {
    internal ReferralSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      Referral referral;

      // referral with a current examination with no allocated doctors or notification responses

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == GetPatientIdByNhsNumber(PATIENT_NHS_NUMBER_1))) == null)
      {
        referral = new Referral();
        _context.Add(referral);
      }

      referral.CreatedAt = _now;
      referral.CreatedByUser = GetSystemAdminUser();
      referral.IsActive = true;
      referral.IsPlannedExamination = true;
      referral.LeadAmhpUser = GetSystemAdminUser();
      referral.ModifiedAt = _now;
      referral.ModifiedByUser = GetSystemAdminUser();
      referral.PatientId = GetPatientIdByNhsNumber(PATIENT_NHS_NUMBER_1);
      referral.ReferralStatusId = GetReferralStatusId();

      // referral with a previous examination

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == GetPatientIdByNhsNumber(PATIENT_NHS_NUMBER_2))) == null)
      {
        referral = new Referral();
        _context.Add(referral);
      }

      referral.CreatedAt = _now;
      referral.CreatedByUser = GetSystemAdminUser();
      referral.IsActive = true;
      referral.IsPlannedExamination = true;
      referral.LeadAmhpUser = GetSystemAdminUser();
      referral.ModifiedAt = _now;
      referral.ModifiedByUser = GetSystemAdminUser();
      referral.PatientId = GetPatientIdByNhsNumber(PATIENT_NHS_NUMBER_2);
      referral.ReferralStatusId = GetReferralStatusId();

      // referral with no examinations

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == GetPatientIdByNhsNumber(PATIENT_NHS_NUMBER_3))) == null)
      {
        referral = new Referral();
        _context.Add(referral);
      }

      referral.CreatedAt = _now;
      referral.CreatedByUser = GetSystemAdminUser();
      referral.IsActive = true;
      referral.IsPlannedExamination = true;
      referral.LeadAmhpUser = GetSystemAdminUser();
      referral.ModifiedAt = _now;
      referral.ModifiedByUser = GetSystemAdminUser();
      referral.PatientId = GetPatientIdByNhsNumber(PATIENT_NHS_NUMBER_3);
      referral.ReferralStatusId = GetReferralStatusId();

      // referral with both current and previous examinations

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == GetPatientIdByNhsNumber(PATIENT_NHS_NUMBER_4))) == null)
      {
        referral = new Referral();
        _context.Add(referral);
      }

      referral.CreatedAt = _now;
      referral.CreatedByUser = GetSystemAdminUser();
      referral.IsActive = true;
      referral.IsPlannedExamination = true;
      referral.LeadAmhpUser = GetSystemAdminUser();
      referral.ModifiedAt = _now;
      referral.ModifiedByUser = GetSystemAdminUser();
      referral.PatientId = GetPatientIdByNhsNumber(PATIENT_NHS_NUMBER_4);
      referral.ReferralStatusId = GetReferralStatusId();

      // referral with current examination and allocated doctors

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == GetPatientIdByAlternativeIdentifier(PATIENT_ALTERNATIVE_IDENTIFIER_5))) == null)
      {
        referral = new Referral();
        _context.Add(referral);
      }

      referral.CreatedAt = _now;
      referral.CreatedByUser = GetSystemAdminUser();
      referral.IsActive = true;
      referral.IsPlannedExamination = true;
      referral.LeadAmhpUser = GetSystemAdminUser();
      referral.ModifiedAt = _now;
      referral.ModifiedByUser = GetSystemAdminUser();
      referral.PatientId = GetPatientIdByAlternativeIdentifier(PATIENT_ALTERNATIVE_IDENTIFIER_5);
      referral.ReferralStatusId = GetReferralStatusId();

      // referral with current examination and notification responses

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == GetPatientIdByAlternativeIdentifier(PATIENT_ALTERNATIVE_IDENTIFIER_6))) == null)
      {
        referral = new Referral();
        _context.Add(referral);
      }

      referral.CreatedAt = _now;
      referral.CreatedByUser = GetSystemAdminUser();
      referral.IsActive = true;
      referral.IsPlannedExamination = true;
      referral.LeadAmhpUser = GetSystemAdminUser();
      referral.ModifiedAt = _now;
      referral.ModifiedByUser = GetSystemAdminUser();
      referral.PatientId = GetPatientIdByAlternativeIdentifier(PATIENT_ALTERNATIVE_IDENTIFIER_6);
      referral.ReferralStatusId = GetReferralStatusId();

      // referral with current examination and notification responses and allocated doctors

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == GetPatientIdByAlternativeIdentifier(PATIENT_ALTERNATIVE_IDENTIFIER_7))) == null)
      {
        referral = new Referral();
        _context.Add(referral);
      }

      referral.CreatedAt = _now;
      referral.CreatedByUser = GetSystemAdminUser();
      referral.IsActive = true;
      referral.IsPlannedExamination = true;
      referral.LeadAmhpUser = GetSystemAdminUser();
      referral.ModifiedAt = _now;
      referral.ModifiedByUser = GetSystemAdminUser();
      referral.PatientId = GetPatientIdByAlternativeIdentifier(PATIENT_ALTERNATIVE_IDENTIFIER_7);
      referral.ReferralStatusId = GetReferralStatusId();
    }
  }
}