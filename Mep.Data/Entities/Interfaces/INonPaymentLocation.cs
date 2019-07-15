using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface INonPaymentLocation
  {
    ICcg Ccg { get; set; }
    int CcgId { get; set; }
    IList<INonPaymentLocation> NonPaymentLocations { get; set; }
    INonPaymentLocationType NonPaymentLocationType { get; set; }
    int NonPaymentLocationTypeId { get; set; }
  }
}