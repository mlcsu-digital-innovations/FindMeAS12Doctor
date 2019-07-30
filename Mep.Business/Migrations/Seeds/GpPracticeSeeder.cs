using System;
using System.Linq;
using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class GpPracticeSeeder : SeederBase
  {
    internal GpPracticeSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      GpPractice gpPractice;
      DateTimeOffset now = DateTimeOffset.Now;

      if ((gpPractice =
            _context.GpPractices
                    .SingleOrDefault(gp => gp.GpPracticeCode == "M83067")) == null)
      {
        gpPractice = new GpPractice();
        _context.Add(gpPractice);
      }

      gpPractice.IsActive = true;
      gpPractice.ModifiedAt = now;
      gpPractice.ModifiedByUser = GetSystemAdminUser();
      gpPractice.GpPracticeCode = "M83067";
      gpPractice.Name = "Lyme Valley Medical Centre";
      gpPractice.Postcode = "ST5 3TF";
      gpPractice.CcgId = 9;

      if ((gpPractice =
      _context.GpPractices
              .SingleOrDefault(gp => gp.GpPracticeCode == "M83143")) == null)
      {
        gpPractice = new GpPractice();
        _context.Add(gpPractice);
      }

      gpPractice.IsActive = true;
      gpPractice.ModifiedAt = now;
      gpPractice.ModifiedByUser = GetSystemAdminUser();
      gpPractice.GpPracticeCode = "M83143";
      gpPractice.Name = "Goldenhill Medical Centre";
      gpPractice.Postcode = "ST6 5QJ";
      gpPractice.CcgId = 9;

      if ((gpPractice =
      _context.GpPractices
        .SingleOrDefault(gp => gp.GpPracticeCode == "M83047")) == null)
      {
        gpPractice = new GpPractice();
        _context.Add(gpPractice);
      }

      gpPractice.IsActive = true;
      gpPractice.ModifiedAt = now;
      gpPractice.ModifiedByUser = GetSystemAdminUser();
      gpPractice.GpPracticeCode = "M83047";
      gpPractice.Name = "Meir Park Surgery";
      gpPractice.Postcode = "ST3 7TW";
      gpPractice.CcgId = 8;
    }
  }
}