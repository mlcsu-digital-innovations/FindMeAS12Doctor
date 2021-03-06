using Fmas12d.Business.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Fmas12d.Business.Services
{
    public interface IUserAssessmentClaimService : IServiceBase
    {
      Task<UserAssessmentClaim> GetAssessmentClaimAsync(int Id);
      Task<UserAssessmentClaimDetail> GetAssessmentAndContactAsync(int assessmentId, int userId);
      Task<UserAssessmentClaimResult> ValidateAssessmentClaimAsync(
        int assessmentId,
        int userId,
        UserAssessmentClaimCreate model
      );

      Task<UserAssessmentClaim> ConfirmAssessmentClaimAsync(
      int assessmentId,
      int userId,
      UserAssessmentClaimCreate model
      );
      Task<UserAssessmentClaimList> GetAssessmentClaimsByUserIdAsync(int userId);
      Task<IEnumerable<UserAssessmentClaim>> GetAssessmentClaimsListByUserIdAsync(int userId);
    }
}