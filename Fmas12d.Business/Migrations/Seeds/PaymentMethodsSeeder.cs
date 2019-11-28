using Fmas12d.Data.Entities;
using System.Linq;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class PaymentMethodsSeeder : SeederBase<PaymentMethod>
  {
    internal void SeedData()
    {
      AddOrUpdate(
        CcgSeeder.NORTH_STAFFORDSHIRE,
        Models.PaymentMethodType.BACS,
        UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE
      );

      AddOrUpdate(
        CcgSeeder.STOKE_ON_TRENT,
        Models.PaymentMethodType.CHEQUE,
        UserSeeder.DISPLAY_NAME_DOCTOR_ND11
      );      

    }

    private void AddOrUpdate(string ccgName, int paymentMethodTypeId, string userDisplayName)
    {
      PaymentMethod paymentMethod;

      if ((paymentMethod = Context.PaymentMethods
          .Where(p => p.CcgId == GetCcgByName(ccgName).Id)
          .Where(p => p.PaymentMethodTypeId == paymentMethodTypeId)
          .SingleOrDefault(g => g.UserId == GetUserByDisplayName(userDisplayName).Id)) == null)
      {
        paymentMethod = new PaymentMethod();
        Context.Add(paymentMethod);
      }
      paymentMethod.CcgId = GetCcgByName(ccgName).Id;
      paymentMethod.PaymentMethodTypeId = paymentMethodTypeId;
      paymentMethod.UserId = GetUserByDisplayName(userDisplayName).Id;
      PopulateActiveAndModifiedWithSystemUser(paymentMethod);      
    }
  }
}