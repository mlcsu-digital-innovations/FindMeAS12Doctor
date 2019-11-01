using System.ComponentModel.DataAnnotations;
namespace Fmas12d.Business.Models
{
  public class PaymentRule : NameDescription
  {
    [MaxLength(2000)]
    [Required]
    public string Criteria { get; set; }
    public virtual PaymentRuleSet PaymentRuleSet { get; set; }
    public int PaymentRuleSetId { get; set; }
  }
}