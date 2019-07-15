using System.Collections.Generic;

namespace Mep.Data.Entities.Audit
{
  public partial class ClaimStatusAudit : NameDescription, IClaimStatus
  {
    public virtual IList<IUserExaminationClaim> UserExaminationClaims { get; set; }
  }
}
