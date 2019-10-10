using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class PaymentMethodTypesSeeder : SeederBase
  {

    internal PaymentMethodTypesSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      PaymentMethodType paymentMethodType;

      if ((paymentMethodType = _context
        .PaymentMethodTypes
          .SingleOrDefault(g => g.Name ==
            PAYMENT_METHOD_TYPE_NAME)) == null)
      {
        paymentMethodType = new PaymentMethodType();
        _context.Add(paymentMethodType);
      }
      paymentMethodType.Description = PAYMENT_METHOD_TYPE_DESCRIPTION;
      paymentMethodType.IsActive = true;
      paymentMethodType.ModifiedAt = _now;
      paymentMethodType.ModifiedByUser = GetSystemAdminUser();
      paymentMethodType.Name = PAYMENT_METHOD_TYPE_NAME;
    }
  }
}