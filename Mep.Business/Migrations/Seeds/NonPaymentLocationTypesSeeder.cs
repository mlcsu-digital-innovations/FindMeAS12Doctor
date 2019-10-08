using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class NonPaymentLocationTypesSeeder : SeederBase
  {

    internal NonPaymentLocationTypesSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      NonPaymentLocationType nonPaymentLocationType;

      if ((nonPaymentLocationType = _context
        .NonPaymentLocationTypes
          .SingleOrDefault(g => g.Name ==
            NON_PAYMENT_LOCATION_TYPE_NAME)) == null)
      {
        nonPaymentLocationType = new NonPaymentLocationType();
        _context.Add(nonPaymentLocationType);
      }
      nonPaymentLocationType.IsActive = true;
      nonPaymentLocationType.ModifiedAt = _now;
      nonPaymentLocationType.ModifiedByUser = GetSystemAdminUser();
      nonPaymentLocationType.Name = NON_PAYMENT_LOCATION_TYPE_NAME;
      nonPaymentLocationType.Description =
        NON_PAYMENT_LOCATION_TYPE_DESCRIPTION;
    }
  }
}