using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public partial class NonPaymentLocationType : NameDescription, INonPaymentLocationType
  {
    public virtual IList<NonPaymentLocation> NonPaymentLocations { get; set; }
  }
}
