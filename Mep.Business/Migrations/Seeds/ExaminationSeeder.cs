using System;
using System.Linq;
using Mep.Data.Entities;

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
      DateTimeOffset now = DateTimeOffset.Now;

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
      examination.ModifiedAt = now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST14 5PP";
      examination.ReferralId = 1;
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
      examination.CompletedTime = now;
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST14 5PP";
      examination.ReferralId = 2;
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
      examination.CompletedTime = now;
      examination.CreatedByUser = GetSystemAdminUser();
      examination.IsActive = true;
      examination.ModifiedAt = now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST14 5PP";
      examination.ReferralId = 4;
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
      examination.ModifiedAt = now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST14 5PP";
      examination.ReferralId = 4;
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
      examination.ModifiedAt = now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST14 5PP";
      examination.ReferralId = 5;
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
      examination.ModifiedAt = now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST14 5PP";
      examination.ReferralId = 6;
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
      examination.ModifiedAt = now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Postcode = "ST14 5PP";
      examination.ReferralId = 7;
      examination.SpecialityId = 1;
    }
  }
}