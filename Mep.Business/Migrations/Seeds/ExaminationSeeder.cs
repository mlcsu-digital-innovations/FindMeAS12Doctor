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

      if ((examination =
      _context.Examinations
                .SingleOrDefault(g => g.MeetingArrangementComment == "New Examination")) == null)
      {
        examination = new Examination();
        _context.Add(examination);
      }

      examination.IsActive = true;
      examination.ModifiedAt = now;
      examination.ModifiedByUser = GetSystemAdminUser();
      examination.Address1 = "Examination Address 1";
      examination.CcgId = 1;
      examination.CreatedByUser = GetSystemAdminUser();
      examination.Postcode = "ST14 5PP";
      examination.ReferralId = 11;
      examination.SpecialityId = 1;



    }
  }
}