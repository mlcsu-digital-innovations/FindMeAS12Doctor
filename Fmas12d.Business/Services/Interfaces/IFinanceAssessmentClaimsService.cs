using Fmas12d.Business.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Fmas12d.Business.Services
{
    public interface IFinanceAssessmentClaimService : IServiceBase
    {
      Task<IEnumerable<FinanceAssessmentClaim>> GetListAsync();
      Task<FinanceAssessmentClaim> GetClaimByIdAsync(int claimId);
    }
}