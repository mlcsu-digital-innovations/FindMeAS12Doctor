using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("PaymentMethodTypesAudit")]
  public partial class PaymentMethodTypeAudit : 
    NameDescriptionAudit, IPaymentMethodType
  {
  }
}
