using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("PaymentMethodTypesAudit")]
  public partial class PaymentMethodTypeAudit : 
    NameDescriptionAudit, IPaymentMethodType
  {
  }
}
