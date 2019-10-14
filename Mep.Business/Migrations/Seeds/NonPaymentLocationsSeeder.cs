using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class NonPaymentLocationsSeeder : SeederBase
  {
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
      nonPaymentLocation.CcgId = GetFirstCcg();
      nonPaymentLocation.IsActive = true;
      nonPaymentLocation.ModifiedAt = _now;
      nonPaymentLocation.ModifiedByUser = GetSystemAdminUser();
      nonPaymentLocation.NonPaymentLocationTypeId =
        GetNonPaymentLocationTypeIdByNonPaymentLocationTypeName(NON_PAYMENT_LOCATION_TYPE_NAME);
    }
  }
}