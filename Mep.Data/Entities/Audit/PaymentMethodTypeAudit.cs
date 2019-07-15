using System.Collections.Generic;

namespace Mep.Data.Entities.Audit
{
  public partial class PaymentMethodTypeAudit : NameDescription, IPaymentMethodType
  {
    public virtual IList<IPaymentMethod> PaymentMethods { get; set; }
  }
}
