using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface INonPaymentLocationType
  {
    IList<INonPaymentLocation> NonPaymentLocations { get; set; }
  }
}