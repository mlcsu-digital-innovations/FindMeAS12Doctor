using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public partial class ClaimStatus : NameDescription, IClaimStatus
  {
    public virtual IList<IUserExaminationClaim> UserExaminationClaims { get; set; }
  }
}
