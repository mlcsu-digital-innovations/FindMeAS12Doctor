using System.Collections.Generic;
namespace Fmas12d.Business.Models
{
  public class ClaimStatus : NameDescription
  {
    public ClaimStatus(
      Data.Entities.ClaimStatus entity
    ) : base(entity) {

    }

    public const int SUBMITTED = 1;
    public const int PROCESSING = 2;
    public const int QUERY = 3;
    public const int APPROVED = 4;
    public const int AWAITING_CCG_APPROVAL = 5;
    public const int REJECTED = 6;
    public virtual IList<UserAssessmentClaim> UserAssessmentClaims { get; set; }

  }
}