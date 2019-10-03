using Mep.Data.Entities;
using System.Linq;
using System;

namespace Mep.Business.Migrations.Seeds
{
  internal class NonPaymentLocationsSeeder : SeederBase
  {

    internal NonPaymentLocationsSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      NonPaymentLocation nonPaymentLocation;

      if ((nonPaymentLocation = _context.NonPaymentLocations
        .SingleOrDefault(g => g.NonPaymentLocationTypeId == 1)) == null)
      {
        nonPaymentLocation = new NonPaymentLocation();
        _context.Add(nonPaymentLocation);
      }
      nonPaymentLocation.IsActive = true;
      nonPaymentLocation.ModifiedAt = _now;
      nonPaymentLocation.ModifiedByUser = GetSystemAdminUser();
      nonPaymentLocation.CcgId = GetFirstCcg();
      nonPaymentLocation.NonPaymentLocationTypeId = 1;
    }
  }
}