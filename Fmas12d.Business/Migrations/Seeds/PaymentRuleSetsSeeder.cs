using Fmas12d.Data.Entities;
using System;
using System.Linq;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class PaymentRuleSetsSeeder : SeederBase<PaymentRuleSet>
  {
    #region Constants    
    internal const string DESCRIPTION_NORTH_STAFFORDSHIRE_1 = "North Staffordshire Rule Set Description";
    internal const int ID_NORTH_STAFFORDSHIRE_1 = 1;
    internal const string NAME_NORTH_STAFFORDSHIRE_1 = "North Staffordshire Name";
    internal readonly DateTimeOffset DATE_TIME_FROM_NORTH_STAFFORDSHIRE_1 =
      new DateTimeOffset(2019, 1, 1,
                         00, 00, 00, 00, DateTimeOffset.Now.Offset);    
    internal readonly DateTimeOffset DATE_TIME_TO_NORTH_STAFFORDSHIRE_1 =
      new DateTimeOffset(2025, 12, 31,
                         23, 59, 59, 999, DateTimeOffset.Now.Offset);    
    #endregion

    internal void SeedData()
    {
      AddOrUpdate(
        ID_NORTH_STAFFORDSHIRE_1,
        NAME_NORTH_STAFFORDSHIRE_1,
        DESCRIPTION_NORTH_STAFFORDSHIRE_1,
        CcgSeeder.NORTH_STAFFORDSHIRE,
        DATE_TIME_FROM_NORTH_STAFFORDSHIRE_1,
        DATE_TIME_TO_NORTH_STAFFORDSHIRE_1
      );

      SaveChangesWithIdentity();
    }

    private void AddOrUpdate(
      int id, 
      string name,
      string description, 
      string ccgName,
      DateTimeOffset from,
      DateTimeOffset to)
    {
      PaymentRuleSet paymentRuleSet;

      if ((paymentRuleSet = Context.PaymentRuleSets
        .SingleOrDefault(g => g.Id == id)) == null)
      {
        paymentRuleSet = new PaymentRuleSet
        {
          Id = id
        };
        Context.Add(paymentRuleSet);
      }
      paymentRuleSet.CcgId = GetCcgByName(ccgName).Id;
      paymentRuleSet.DateTimeFrom = from;
      paymentRuleSet.DateTimeTo = to;
      paymentRuleSet.Description = description;
      paymentRuleSet.Name = name;      
      PopulateActiveAndModifiedWithSystemUser(paymentRuleSet);
    }
  }
}