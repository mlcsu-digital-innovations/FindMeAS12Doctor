using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("ReferralStatusesAudit")]
  public partial class ReferralStatusAudit : NameDescriptionAudit, IReferralStatus
  {
  }
}
