using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class PaymentRuleSetsSeeder : SeederBase<PaymentRuleSet>
  {
    internal void SeedData()
    {
      PaymentRuleSet paymentRuleSet;

      if ((paymentRuleSet = _context
        .PaymentRuleSets
          .SingleOrDefault(g => g.Name ==
            PAYMENT_RULE_SET_NAME)) == null)
      {
        paymentRuleSet = new PaymentRuleSet();
        _context.Add(paymentRuleSet);
      }
      paymentRuleSet.CcgId = GetCcgByName(CcgSeeder.NORTH_STAFFORDSHIRE).Id;
      paymentRuleSet.DateTimeFrom = _now;
      paymentRuleSet.DateTimeTo = _now.AddYears(1);
      paymentRuleSet.Description = PAYMENT_RULE_SET_DESCRIPTION;
      paymentRuleSet.IsActive = true;
      paymentRuleSet.ModifiedAt = _now;
      paymentRuleSet.ModifiedByUser = GetSystemAdminUser();
      paymentRuleSet.Name = PAYMENT_RULE_SET_NAME;
    }
  }
}