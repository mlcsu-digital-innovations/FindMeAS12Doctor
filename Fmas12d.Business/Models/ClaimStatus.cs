using System.Collections.Generic;
namespace Fmas12d.Business.Models
{
  public class ClaimStatus : NameDescription
  {
    public ClaimStatus(
      Data.Entities.ClaimStatus entity
    ) : base(entity) {

    }

    public const int ACCEPTED = 1;
    public virtual IList<UserAssessmentClaim> UserAssessmentClaims { get; set; }

  }
}