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

      if ((nonPaymentLocation = _context
        .NonPaymentLocations
          .SingleOrDefault(g => g.NonPaymentLocationTypeId ==
            GetNonPaymentLocationTypeIdByNonPaymentLocationTypeName(NON_PAYMENT_LOCATION_TYPE_NAME)))
              == null)
      {
        nonPaymentLocation = new NonPaymentLocation();
        _context.Add(nonPaymentLocation);
      }
      nonPaymentLocation.IsActive = true;
      nonPaymentLocation.ModifiedAt = _now;
      nonPaymentLocation.ModifiedByUser = GetSystemAdminUser();
      nonPaymentLocation.CcgId = GetFirstCcg();
      nonPaymentLocation.NonPaymentLocationTypeId = GetNonPaymentLocationTypeIdByNonPaymentLocationTypeName(NON_PAYMENT_LOCATION_TYPE_NAME);
    }
  }
}