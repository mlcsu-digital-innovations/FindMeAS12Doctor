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
            GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_FEMALE).Id)) == null)
      {
        paymentMethod = new PaymentMethod();
        _context.Add(paymentMethod);
      }
      paymentMethod.CcgId = GetCcgByName(CCG_NAME_NORTH_STAFFORDSHIRE).Id;
      paymentMethod.IsActive = true;
      paymentMethod.ModifiedAt = _now;
      paymentMethod.ModifiedByUser = GetSystemAdminUser();
      paymentMethod.PaymentMethodTypeId =
        GetPaymentMethodTypeIdByPaymentMethodTypeName(PAYMENT_METHOD_TYPE_NAME);
      paymentMethod.UserId =
        GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_FEMALE).Id;

      if ((paymentMethod = _context
        .PaymentMethods
          .SingleOrDefault(g => g.UserId ==
            GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_MALE).Id)) == null)
      {
        paymentMethod = new PaymentMethod();
        _context.Add(paymentMethod);
      }
      paymentMethod.CcgId = GetCcgByName(CCG_NAME_NORTH_STAFFORDSHIRE).Id;
      paymentMethod.IsActive = true;
      paymentMethod.ModifiedAt = _now;
      paymentMethod.ModifiedByUser = GetSystemAdminUser();
      paymentMethod.PaymentMethodTypeId =
        GetPaymentMethodTypeIdByPaymentMethodTypeName(PAYMENT_METHOD_TYPE_NAME);
      paymentMethod.UserId =
        GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_MALE).Id;
    }
  }
}