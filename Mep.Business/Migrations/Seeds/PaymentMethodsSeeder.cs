using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class PaymentMethodsSeeder : SeederBase
  {

    internal void SeedData()
    {
      PaymentMethod paymentMethod;

      if ((paymentMethod = _context
        .PaymentMethods
          .SingleOrDefault(g => g.UserId ==
            GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_FEMALE))) == null)
      {
        paymentMethod = new PaymentMethod();
        _context.Add(paymentMethod);
      }
      paymentMethod.CcgId = GetFirstCcg();
      paymentMethod.IsActive = true;
      paymentMethod.ModifiedAt = _now;
      paymentMethod.ModifiedByUser = GetSystemAdminUser();
      paymentMethod.PaymentMethodTypeId =
        GetPaymentMethodTypeIdByPaymentMethodTypeName(PAYMENT_METHOD_TYPE_NAME);
      paymentMethod.UserId =
        GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_FEMALE);

      if ((paymentMethod = _context
        .PaymentMethods
          .SingleOrDefault(g => g.UserId ==
            GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_MALE))) == null)
      {
        paymentMethod = new PaymentMethod();
        _context.Add(paymentMethod);
      }
      paymentMethod.CcgId = GetFirstCcg();
      paymentMethod.IsActive = true;
      paymentMethod.ModifiedAt = _now;
      paymentMethod.ModifiedByUser = GetSystemAdminUser();
      paymentMethod.PaymentMethodTypeId =
        GetPaymentMethodTypeIdByPaymentMethodTypeName(PAYMENT_METHOD_TYPE_NAME);
      paymentMethod.UserId =
        GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_MALE);
    }
  }
}