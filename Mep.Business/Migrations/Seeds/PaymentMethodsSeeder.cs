using Mep.Data.Entities;
using System.Linq;
using System;

namespace Mep.Business.Migrations.Seeds
{
  internal class PaymentMethodsSeeder : SeederBase
  {

    internal PaymentMethodsSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      PaymentMethod paymentMethod;

      if ((paymentMethod = _context.PaymentMethods
        .SingleOrDefault(g => g.UserId == GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_FEMALE))) == null)
      {
        paymentMethod = new PaymentMethod();
        _context.Add(paymentMethod);
      }
      paymentMethod.CcgId = GetFirstCcg();
      paymentMethod.IsActive = true;
      paymentMethod.ModifiedAt = _now;
      paymentMethod.ModifiedByUser = GetSystemAdminUser();
      // TODO: replace PaymentMethodTypeId = 1 with Get function when PaymentMethodTypeSeeder is populated with data
      paymentMethod.PaymentMethodTypeId = 1;
      paymentMethod.UserId = GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_FEMALE);

      if ((paymentMethod = _context.PaymentMethods
        .SingleOrDefault(g => g.UserId == GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_MALE))) == null)
      {
        paymentMethod = new PaymentMethod();
        _context.Add(paymentMethod);
      }
      paymentMethod.CcgId = GetFirstCcg();
      paymentMethod.IsActive = true;
      paymentMethod.ModifiedAt = _now;
      paymentMethod.ModifiedByUser = GetSystemAdminUser();
      // TODO: replace PaymentMethodTypeId = 1 with Get function when PaymentMethodTypeSeeder is populated with data
      paymentMethod.PaymentMethodTypeId = 1;
      paymentMethod.UserId = GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_MALE);
    }
  }
}