using System.Collections.Generic;
namespace Mep.Business.Models
{
  public class NonPaymentLocationType : NameDescription
  {
    public virtual IList<NonPaymentLocation> NonPaymentLocations { get; set; }
  }
}