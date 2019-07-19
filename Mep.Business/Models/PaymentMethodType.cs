using System.Collections.Generic;
namespace Mep.Business.Models
{
  public class PaymentMethodType : NameDescription
  {
    public virtual IList<PaymentMethod> PaymentMethods { get; set; }
  }
}