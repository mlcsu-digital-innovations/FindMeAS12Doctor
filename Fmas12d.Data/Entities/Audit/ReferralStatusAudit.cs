using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("ReferralStatusesAudit")]
  public partial class ReferralStatusAudit : NameDescriptionAudit, IReferralStatus
  {
  }
}
