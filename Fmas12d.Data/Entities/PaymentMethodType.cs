using System.Collections.Generic;

namespace Fmas12d.Data.Entities
{
  public partial class PaymentMethodType : 
    NameDescription, IPaymentMethodType
  {
    public virtual IList<PaymentMethod> PaymentMethods { get; set; }
  }
}
