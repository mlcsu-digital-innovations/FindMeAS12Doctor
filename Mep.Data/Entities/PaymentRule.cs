using System.ComponentModel.DataAnnotations;

namespace Mep.Data.Entities
{
  public partial class PaymentRule : NameDescription, IPaymentRule
  {
    [MaxLength(2000)]
    [Required]
    public string Criteria { get; set; }
    public virtual IPaymentRuleSet PaymentRuleSet { get; set; }
    public int PaymentRuleSetId { get; set; }
  }
}
