using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface INonPaymentLocation
  {
    int CcgId { get; set; }
    int NonPaymentLocationTypeId { get; set; }
  }
}