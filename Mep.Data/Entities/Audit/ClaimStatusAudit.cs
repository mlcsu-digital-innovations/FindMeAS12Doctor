using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("ClaimStatusesAudit")]
  public partial class ClaimStatusAudit : NameDescriptionAudit, IClaimStatus
  {
    // public virtual IList<UserExaminationClaimAudit> UserExaminationClaims { get; set; }
  }
}
