using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("ReferralStatusesAudit")]
  public partial class ReferralStatusAudit : NameDescriptionAudit, IReferralStatus
  {
    // public virtual IList<ReferralStatusAudit> ReferralStatuses { get; set; }
  }
}
