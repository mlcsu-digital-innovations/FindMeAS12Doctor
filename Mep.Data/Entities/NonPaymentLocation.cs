using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public partial class NonPaymentLocation : BaseEntity, INonPaymentLocation
  {
    public virtual Ccg Ccg { get; set; }
    public int CcgId { get; set; }
    public virtual IList<NonPaymentLocation> NonPaymentLocations { get; set; }
    public virtual NonPaymentLocationType NonPaymentLocationType { get; set; }
    public int NonPaymentLocationTypeId { get; set; }
  }
}
