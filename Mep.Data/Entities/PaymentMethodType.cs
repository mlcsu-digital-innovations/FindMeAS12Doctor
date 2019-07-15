using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public partial class PaymentMethodType : NameDescription, IPaymentMethodType
  {
    public virtual IList<PaymentMethod> PaymentMethods { get; set; }
  }
}
