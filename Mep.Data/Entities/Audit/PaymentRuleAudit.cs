using System.ComponentModel.DataAnnotations;

namespace Mep.Data.Entities.Audit
{
  public partial class PaymentRuleAudit : NameDescription, IPaymentRule
  {
    [MaxLength(2000)]
    [Required]
    public string Criteria { get; set; }
    // public virtual PaymentRuleSetAudit PaymentRuleSet { get; set; }
    public int PaymentRuleSetId { get; set; }
  }
}
