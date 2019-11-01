using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("NonPaymentLocationsAudit")]
  public partial class NonPaymentLocationAudit : 
    BaseAudit, INonPaymentLocation
  {
    public int CcgId { get; set; }
    public int NonPaymentLocationTypeId { get; set; }
  }
}
