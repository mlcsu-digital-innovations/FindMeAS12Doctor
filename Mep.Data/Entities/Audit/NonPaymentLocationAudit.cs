using System.Collections.Generic;

namespace Mep.Data.Entities.Audit
{
  public partial class NonPaymentLocationAudit : BaseAudit, INonPaymentLocation
  {
    // public virtual CcgAudit Ccg { get; set; }
    public int CcgId { get; set; }
    // public virtual IList<NonPaymentLocationAudit> NonPaymentLocations { get; set; }
    // public virtual NonPaymentLocationTypeAudit NonPaymentLocationType { get; set; }
    public int NonPaymentLocationTypeId { get; set; }
  }
}
