using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface IPaymentMethodType
  {
    IList<IPaymentMethod> PaymentMethods { get; set; }
  }
}