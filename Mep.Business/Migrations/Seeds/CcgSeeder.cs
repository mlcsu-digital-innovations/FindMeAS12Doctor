using System;
using System.Linq;
using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class CcgSeeder : SeederBase
  {
    internal CcgSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      Ccg ccg;
      DateTimeOffset now = DateTimeOffset.Now;

      if ((ccg =
            _context.Ccgs
                    .SingleOrDefault(c => c.Name == "NHS Stoke on Trent CCG")) == null)
      {
        ccg = new Ccg();
        _context.Add(ccg);
      }

      ccg.IsActive = true;
      ccg.ModifiedAt = now;
      ccg.ModifiedByUser = GetSystemAdminUser();
      ccg.Name = "NHS Stoke on Trent CCG";
      ccg.CostCentre = 100;
      ccg.FailedExamPayment = 50.0m;
      ccg.IsPaymentApprovalRequired = true;
      ccg.SuccessfulPencePerMile = 20.0m;
      ccg.UnsuccessfulPencePerMile = 20.0m;

      if ((ccg =
            _context.Ccgs
                    .SingleOrDefault(c => c.Name == "NHS Stafford and Surrounds CCG")) == null)
      {
        ccg = new Ccg();
        _context.Add(ccg);
      }

      ccg.IsActive = true;
      ccg.ModifiedAt = now;
      ccg.ModifiedByUser = GetSystemAdminUser();
      ccg.Name = "NHS Stafford and Surrounds CCG";
      ccg.CostCentre = 100;
      ccg.FailedExamPayment = 50.0m;
      ccg.IsPaymentApprovalRequired = true;
      ccg.SuccessfulPencePerMile = 20.0m;
      ccg.UnsuccessfulPencePerMile = 20.0m;

      if ((ccg =
      _context.Ccgs
              .SingleOrDefault(c => c.Name == "NHS North Staffordshire CCG")) == null)
      {
        ccg = new Ccg();
        _context.Add(ccg);
      }

      ccg.IsActive = true;
      ccg.ModifiedAt = now;
      ccg.ModifiedByUser = GetSystemAdminUser();
      ccg.Name = "NHS North Staffordshire CCG";
      ccg.CostCentre = 100;
      ccg.FailedExamPayment = 50.0m;
      ccg.IsPaymentApprovalRequired = true;
      ccg.SuccessfulPencePerMile = 20.0m;
      ccg.UnsuccessfulPencePerMile = 20.0m;

    }
  }
}