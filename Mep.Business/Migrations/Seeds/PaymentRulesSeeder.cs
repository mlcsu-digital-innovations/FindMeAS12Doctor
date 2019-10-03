using Mep.Data.Entities;
using System.Linq;
using System;

namespace Mep.Business.Migrations.Seeds
{
  internal class PaymentRulesSeeder : SeederBase
  {

    internal PaymentRulesSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      PaymentRule paymentRule;

      if ((paymentRule = _context.PaymentRules
        .SingleOrDefault(g => g.Name == PAYMENT_RULE_NAME_1)) == null)
      {
        paymentRule = new PaymentRule();
        _context.Add(paymentRule);
      }
      paymentRule.Criteria = PAYMENT_RULE_CRITERIA_1;
      paymentRule.Description = PAYMENT_RULE_DESCRIPTION_1;
      paymentRule.IsActive = true;
      paymentRule.ModifiedAt = _now;
      paymentRule.ModifiedByUser = GetSystemAdminUser();
      paymentRule.Name = PAYMENT_RULE_NAME_1;
      // TODO: replace NonPaymentLocationTypeId = 1 with Get function when NonPaymentLocationTypeSeeder is populated with data
      paymentRule.PaymentRuleSetId = 1;
    }
  }
}