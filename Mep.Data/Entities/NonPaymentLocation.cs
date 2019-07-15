using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public partial class NonPaymentLocation : BaseEntity, INonPaymentLocation
  {
    public virtual ICcg Ccg { get; set; }
    public int CcgId { get; set; }
    public virtual IList<INonPaymentLocation> NonPaymentLocations { get; set; }
    public virtual INonPaymentLocationType NonPaymentLocationType { get; set; }
    public int NonPaymentLocationTypeId { get; set; }
  }
}
