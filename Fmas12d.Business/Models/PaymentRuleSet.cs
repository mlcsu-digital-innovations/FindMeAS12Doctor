using System;
using System.Collections.Generic;
namespace Mep.Business.Models
{
  public class PaymentRuleSet : NameDescription
  {
    public virtual Ccg Ccg { get; set; }
    public int CcgId { get; set; }
    public DateTimeOffset DateTimeFrom { get; set; }
    public DateTimeOffset DateTimeTo { get; set; }
    public virtual IList<PaymentRule> PaymentRules { get; set; } 
  }
}