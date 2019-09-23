using System;
using System.Linq;
using Mep.Data.Entities;

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
                .SingleOrDefault(g => g.PatientId == 1)) == null)
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
      referral.PatientId = 1;
      referral.ReferralStatusId = 1;

      // referral with a previous examination

      if ((referral =
      _context.Referrals
                .SingleOrDefault(g => g.PatientId == 2)) == null)
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
      referral.PatientId = 2;
      referral.ReferralStatusId = 1;

      // referral with no examinations

      if ((referral =
      _context.Referrals
                .SingleOrDefault(g => g.PatientId == 3)) == null)
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
      referral.PatientId = 3;
      referral.ReferralStatusId = 1;

      // referral with both current and previous examinations

      if ((referral =
      _context.Referrals
                .SingleOrDefault(g => g.PatientId == 4)) == null)
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
      referral.PatientId = 4;
      referral.ReferralStatusId = 1;

      // referral with current examination and allocated doctors

      if ((referral =
      _context.Referrals
                .SingleOrDefault(g => g.PatientId == 5)) == null)
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
      referral.PatientId = 5;
      referral.ReferralStatusId = 1;

      // referral with current examination and notification responses

      if ((referral =
      _context.Referrals
                .SingleOrDefault(g => g.PatientId == 6)) == null)
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
      referral.PatientId = 6;
      referral.ReferralStatusId = 1;

      // referral with current examination and notification responses and allocated doctors

      if ((referral =
      _context.Referrals
                .SingleOrDefault(g => g.PatientId == 7)) == null)
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
      referral.PatientId = 7;
      referral.ReferralStatusId = 1;
    }
  }
}