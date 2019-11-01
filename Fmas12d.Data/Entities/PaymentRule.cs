using System.ComponentModel.DataAnnotations;

namespace Mep.Data.Entities
{
  public partial class PaymentRule : NameDescription, IPaymentRule
  {
    [MaxLength(2000)]
    [Required]
    public string Criteria { get; set; }
    public virtual PaymentRuleSet PaymentRuleSet { get; set; }
    public int PaymentRuleSetId { get; set; }
  }
}
