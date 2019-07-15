﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("PaymentRuleSetsAudit")]
  public partial class PaymentRuleSetAudit : NameDescriptionAudit, IPaymentRuleSet
  {
    // public virtual CcgAudit Ccg { get; set; }
    public int CcgId { get; set; }
    public DateTimeOffset DateTimeFrom { get; set; }
    public DateTimeOffset DateTimeTo { get; set; }
    // public virtual IList<PaymentRuleAudit> PaymentRules { get; set; }
  }
}
