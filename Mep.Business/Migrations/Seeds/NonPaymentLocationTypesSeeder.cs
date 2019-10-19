using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class NonPaymentLocationTypesSeeder : SeederBase<NonPaymentLocationType>
  {
    internal void SeedData()
    {
      NonPaymentLocationType nonPaymentLocationType;

      if ((nonPaymentLocationType = _context
        .NonPaymentLocationTypes
          .SingleOrDefault(g => g.Name ==
            NON_PAYMENT_LOCATION_TYPE_NAME_GP_PRACTICE)) == null)
      {
        nonPaymentLocationType = new NonPaymentLocationType();
        _context.Add(nonPaymentLocationType);
      }
      nonPaymentLocationType.Description =
        NON_PAYMENT_LOCATION_TYPE_DESCRIPTION_GP_PRACTICE;
      nonPaymentLocationType.IsActive = true;
      nonPaymentLocationType.ModifiedAt = _now;
      nonPaymentLocationType.ModifiedByUser = GetSystemAdminUser();
      nonPaymentLocationType.Name = NON_PAYMENT_LOCATION_TYPE_NAME_GP_PRACTICE;
    }
  }
}