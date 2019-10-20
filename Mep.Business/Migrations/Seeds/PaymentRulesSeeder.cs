using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class PaymentRulesSeeder : SeederBase<PaymentRule>
  {
    #region Constants
    internal const string CRITERIA_1 = "Payment Rule Criteria 1";
    internal const string DESCRIPTION_1 = "Payment Rule Description 1";
    internal const string NAME_1 = "Payment Rule 1";
    #endregion

    internal void SeedData()
    {
      AddOrUpdate(
        NAME_1,
        DESCRIPTION_1,
        CRITERIA_1,
        PaymentRuleSetsSeeder.NAME_NORTH_STAFFORDSHIRE_1
      );
    }

    private void AddOrUpdate(
      string name, 
      string description, 
      string criteria, 
      string paymentRuleSetName)
    {
      PaymentRule paymentRule;

      if ((paymentRule = _context.PaymentRules
        .SingleOrDefault(g => g.Name == name)) == null)
      {
        paymentRule = new PaymentRule();
        _context.Add(paymentRule);
      }
      paymentRule.Criteria = criteria;
      paymentRule.Description = description;
      paymentRule.Name = name;
      paymentRule.PaymentRuleSetId = GetPaymentRuleSetIdByPaymentRuleSetName(paymentRuleSetName);
      PopulateActiveAndModifiedWithSystemUser(paymentRule);
    }
  }
}