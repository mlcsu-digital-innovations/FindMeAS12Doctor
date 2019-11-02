using System.Collections.Generic;
namespace Fmas12d.Business.Models
{
  public class ClaimStatus : NameDescription
  {
    public const int ACCEPTED = 1;
    public virtual IList<UserAssessmentClaim> UserAssessmentClaims { get; set; }
  }
}