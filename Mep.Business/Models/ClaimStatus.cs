using System.Collections.Generic;
namespace Mep.Business.Models
{
  public class ClaimStatus : NameDescription
  {
    public virtual IList<UserExaminationClaim> UserExaminationClaims { get; set; }
  }
}