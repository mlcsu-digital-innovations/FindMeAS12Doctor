using System.Collections.Generic;
namespace Mep.Business.Models
{
  public class NonPaymentLocationType
  {
    public virtual IList<NonPaymentLocation> NonPaymentLocations { get; set; }
  }
}