using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface IClaimStatus
  {
    IList<IUserExaminationClaim> UserExaminationClaims { get; set; }
  }
}