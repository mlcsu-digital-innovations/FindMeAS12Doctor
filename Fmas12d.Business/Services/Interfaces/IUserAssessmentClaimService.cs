using Fmas12d.Business.Models;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
    public interface IUserAssessmentClaimService : IServiceBase
    {
      Task<UserAssessmentClaim> GetAssessmentClaimAsync(int Id);
      Task<UserAssessmentClaimDetail> GetAssessmentAndContactAsync(int assessmentId, int userId);
      Task<UserAssessmentClaimResult> ValidateAssessmentClaim(
        int assessmentId,
        int userId,
        UserAssessmentClaimCreate model
      );
    }
}