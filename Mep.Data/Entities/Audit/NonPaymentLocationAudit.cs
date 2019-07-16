using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("NonPaymentLocationsAudit")]
  public partial class NonPaymentLocationAudit : BaseAudit, INonPaymentLocation
  {
    // public virtual CcgAudit Ccg { get; set; }
    public int CcgId { get; set; }
    // public virtual IList<NonPaymentLocationAudit> NonPaymentLocations { get; set; }
    // public virtual NonPaymentLocationTypeAudit NonPaymentLocationType { get; set; }
    public int NonPaymentLocationTypeId { get; set; }
  }
}
