using System;
using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface IPaymentRuleSet
  {
    ICcg Ccg { get; set; }
    int CcgId { get; set; }
    DateTimeOffset DateTimeFrom { get; set; }
    DateTimeOffset DateTimeTo { get; set; }
    IList<IPaymentRule> PaymentRules { get; set; }
  }
}