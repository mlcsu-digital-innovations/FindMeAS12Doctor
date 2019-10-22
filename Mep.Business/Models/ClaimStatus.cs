using System.Collections.Generic;
namespace Mep.Business.Models
{
  public class ClaimStatus : NameDescription
  {
    public const int ACCEPTED = 1;
    public virtual IList<UserExaminationClaim> UserExaminationClaims { get; set; }
  }
}