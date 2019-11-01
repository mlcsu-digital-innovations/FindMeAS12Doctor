using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("ClaimStatusesAudit")]
  public partial class ClaimStatusAudit : NameDescriptionAudit, IClaimStatus
  {
  }
}
