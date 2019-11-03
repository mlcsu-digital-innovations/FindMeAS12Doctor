using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("PaymentRuleSetsAudit")]
  public partial class PaymentRuleSetAudit : NameDescriptionAudit, IPaymentRuleSet
  {    
    public int CcgId { get; set; }
    public DateTimeOffset DateTimeFrom { get; set; }
    public DateTimeOffset DateTimeTo { get; set; }
  }
}
