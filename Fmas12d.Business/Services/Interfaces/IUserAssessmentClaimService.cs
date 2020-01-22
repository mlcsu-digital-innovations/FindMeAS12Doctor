using Fmas12d.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
    public interface IUserAssessmentClaimService : IServiceBase
    {
      Task<UserAssessmentClaim> GetAssessmentClaim(int Id);
    }
}