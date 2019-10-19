using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class NonPaymentLocationsSeeder : SeederBase<NonPaymentLocation>
  {
    internal void SeedData()
    {
      NonPaymentLocation nonPaymentLocation;

      if ((nonPaymentLocation = _context
        .NonPaymentLocations
          .SingleOrDefault(g => g.NonPaymentLocationTypeId ==
            GetNonPaymentLocationTypeIdByNonPaymentLocationTypeName(NON_PAYMENT_LOCATION_TYPE_NAME_GP_PRACTICE)))
              == null)
      {
        nonPaymentLocation = new NonPaymentLocation();
        _context.Add(nonPaymentLocation);
      }
      nonPaymentLocation.CcgId = GetCcgByName(CcgSeeder.STOKE_ON_TRENT).Id;
      nonPaymentLocation.NonPaymentLocationTypeId =
        GetNonPaymentLocationTypeIdByNonPaymentLocationTypeName(NON_PAYMENT_LOCATION_TYPE_NAME_GP_PRACTICE);
      PopulateActiveAndModifiedWithSystemUser(nonPaymentLocation);
    }
  }
}