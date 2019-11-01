using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("PaymentRulesAudit")]
  public partial class PaymentRuleAudit : NameDescriptionAudit, IPaymentRule
  {
    [MaxLength(2000)]
    [Required]
    public string Criteria { get; set; }
    public int PaymentRuleSetId { get; set; }
  }
}
