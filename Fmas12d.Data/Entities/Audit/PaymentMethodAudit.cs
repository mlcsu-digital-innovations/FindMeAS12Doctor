using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("PaymentMethodsAudit")]
  public partial class PaymentMethodAudit : BaseAudit, IPaymentMethod
  {    
    public int CcgId { get; set; }
    public int PaymentMethodTypeId { get; set; }
    public int UserId { get; set; }
  }
}
