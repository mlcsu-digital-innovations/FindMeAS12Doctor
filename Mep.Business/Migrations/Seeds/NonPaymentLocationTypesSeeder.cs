using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class NonPaymentLocationTypesSeeder : SeederBase<NonPaymentLocationType>
  {
    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntity(
        Models.NonPaymentLocationType.GP_PRACTICE,
        NON_PAYMENT_LOCATION_TYPE_NAME_GP_PRACTICE,
        NON_PAYMENT_LOCATION_TYPE_DESCRIPTION_GP_PRACTICE
      );
    }
  }
}