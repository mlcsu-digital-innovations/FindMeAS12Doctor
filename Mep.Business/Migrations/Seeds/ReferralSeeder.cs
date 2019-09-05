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
      DateTimeOffset now = DateTimeOffset.Now;

      if ((referral =
      _context.Referrals
                .SingleOrDefault(g => g.PatientId == 2 )) == null)
      {
        referral = new Referral();
        _context.Add(referral);
      }

      referral.IsActive = true;
      referral.ModifiedAt = now;
      referral.ModifiedByUser = GetSystemAdminUser();
      referral.CreatedAt = now;
      referral.CreatedByUser = GetSystemAdminUser();
      referral.PatientId = 2;
      referral.ReferralStatusId = 1;
      referral.IsPlannedExamination = true;
      referral.LeadAmhpUser = GetSystemAdminUser();

            if ((referral =
      _context.Referrals
                .SingleOrDefault(g => g.PatientId == 3 )) == null)
      {
        referral = new Referral();
        _context.Add(referral);
      }

      referral.IsActive = true;
      referral.ModifiedAt = now;
      referral.ModifiedByUser = GetSystemAdminUser();
      referral.CreatedAt = now;
      referral.CreatedByUser = GetSystemAdminUser();
      referral.PatientId = 3;
      referral.ReferralStatusId = 1;
      referral.IsPlannedExamination = true;
      referral.LeadAmhpUser = GetSystemAdminUser();

            if ((referral =
      _context.Referrals
                .SingleOrDefault(g => g.PatientId == 4 )) == null)
      {
        referral = new Referral();
        _context.Add(referral);
      }

      referral.IsActive = true;
      referral.ModifiedAt = now;
      referral.ModifiedByUser = GetSystemAdminUser();
      referral.CreatedAt = now;
      referral.CreatedByUser = GetSystemAdminUser();
      referral.PatientId = 4;
      referral.ReferralStatusId = 1;
      referral.IsPlannedExamination = true;
      referral.LeadAmhpUser = GetSystemAdminUser();

            if ((referral =
      _context.Referrals
                .SingleOrDefault(g => g.PatientId == 5 )) == null)
      {
        referral = new Referral();
        _context.Add(referral);
      }

      referral.IsActive = true;
      referral.ModifiedAt = now;
      referral.ModifiedByUser = GetSystemAdminUser();
      referral.CreatedAt = now;
      referral.CreatedByUser = GetSystemAdminUser();
      referral.PatientId = 5;
      referral.ReferralStatusId = 1;
      referral.IsPlannedExamination = true;
      referral.LeadAmhpUser = GetSystemAdminUser();
    }
  }
}