using System.Collections.Generic;
namespace Mep.Business.Models
{
  public class PaymentMethodType : NameDescription
  {
    public const int BACS = 1;
    public const int CHEQUE = 2;

    public virtual IList<PaymentMethod> PaymentMethods { get; set; }
  }
}