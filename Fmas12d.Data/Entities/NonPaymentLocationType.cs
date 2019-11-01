using System.Collections.Generic;

namespace Fmas12d.Data.Entities
{
  public partial class NonPaymentLocationType : 
    NameDescription, INonPaymentLocationType
  {
    public virtual IList<NonPaymentLocation> NonPaymentLocations { get; set; }
  }
}
