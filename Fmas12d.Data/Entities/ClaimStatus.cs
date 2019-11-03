using System.Collections.Generic;

namespace Fmas12d.Data.Entities
{
  public partial class ClaimStatus : NameDescription, IClaimStatus
  {
    public virtual IList<UserAssessmentClaim> UserAssessmentClaims { get; set; }
  }
}
