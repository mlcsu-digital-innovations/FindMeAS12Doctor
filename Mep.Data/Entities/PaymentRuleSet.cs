using System;
using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public partial class PaymentRuleSet : NameDescription, IPaymentRuleSet
  {
    public virtual ICcg Ccg { get; set; }
    public int CcgId { get; set; }
    public DateTimeOffset DateTimeFrom { get; set; }
    public DateTimeOffset DateTimeTo { get; set; }
    public virtual IList<IPaymentRule> PaymentRules { get; set; }
  }
}
