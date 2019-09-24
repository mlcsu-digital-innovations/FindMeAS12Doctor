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
          .SingleOrDefault(g => g.PatientId == _patient1.Id)) == null)
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
      referral.PatientId = _patient1.Id;
      referral.ReferralStatusId = _referralStatus.Id;

      // referral with a previous examination

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == _patient2.Id)) == null)
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
      referral.PatientId = _patient2.Id;
      referral.ReferralStatusId = _referralStatus.Id;

      // referral with no examinations

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == _patient3.Id)) == null)
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
      referral.PatientId = _patient3.Id;
      referral.ReferralStatusId = _referralStatus.Id;

      // referral with both current and previous examinations

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == _patient4.Id)) == null)
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
      referral.PatientId = _patient4.Id;
      referral.ReferralStatusId = _referralStatus.Id;

      // referral with current examination and allocated doctors

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == _patient5.Id)) == null)
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
      referral.PatientId = _patient5.Id;
      referral.ReferralStatusId = _referralStatus.Id;

      // referral with current examination and notification responses

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == _patient6.Id)) == null)
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
      referral.PatientId = _patient6.Id;
      referral.ReferralStatusId = _referralStatus.Id;

      // referral with current examination and notification responses and allocated doctors

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == _patient7.Id)) == null)
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
      referral.PatientId = _patient7.Id;
      referral.ReferralStatusId = _referralStatus.Id;
    }
  }
}