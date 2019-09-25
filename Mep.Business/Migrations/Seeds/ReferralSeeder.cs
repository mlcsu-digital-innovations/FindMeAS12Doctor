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
          .SingleOrDefault(g => g.PatientId == GetPatientIdByNhsNumber(PATIENTNHSNUMBER1))) == null)
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
      referral.PatientId = GetPatientIdByNhsNumber(PATIENTNHSNUMBER1);
      referral.ReferralStatusId = GetReferralStatusId();

      // referral with a previous examination

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == GetPatientIdByNhsNumber(PATIENTNHSNUMBER2))) == null)
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
      referral.PatientId = GetPatientIdByNhsNumber(PATIENTNHSNUMBER2);
      referral.ReferralStatusId = GetReferralStatusId();

      // referral with no examinations

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == GetPatientIdByNhsNumber(PATIENTNHSNUMBER3))) == null)
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
      referral.PatientId = GetPatientIdByNhsNumber(PATIENTNHSNUMBER3);
      referral.ReferralStatusId = GetReferralStatusId();

      // referral with both current and previous examinations

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == GetPatientIdByNhsNumber(PATIENTNHSNUMBER4))) == null)
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
      referral.PatientId = GetPatientIdByNhsNumber(PATIENTNHSNUMBER4);
      referral.ReferralStatusId = GetReferralStatusId();

      // referral with current examination and allocated doctors

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == GetPatientIdByAlternativeIdentifier(PATIENTALTERNATIVEIDENTIFIER5))) == null)
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
      referral.PatientId = GetPatientIdByAlternativeIdentifier(PATIENTALTERNATIVEIDENTIFIER5);
      referral.ReferralStatusId = GetReferralStatusId();

      // referral with current examination and notification responses

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == GetPatientIdByAlternativeIdentifier(PATIENTALTERNATIVEIDENTIFIER6))) == null)
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
      referral.PatientId = GetPatientIdByAlternativeIdentifier(PATIENTALTERNATIVEIDENTIFIER6);
      referral.ReferralStatusId = GetReferralStatusId();

      // referral with current examination and notification responses and allocated doctors

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == GetPatientIdByAlternativeIdentifier(PATIENTALTERNATIVEIDENTIFIER7))) == null)
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
      referral.PatientId = GetPatientIdByAlternativeIdentifier(PATIENTALTERNATIVEIDENTIFIER7);
      referral.ReferralStatusId = GetReferralStatusId();
    }
  }
}