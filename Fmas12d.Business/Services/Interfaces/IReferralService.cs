using Fmas12d.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public interface IReferralService : IServiceBase
  {
    Task<Referral> CreateAsync(ReferralCreate businessModel);
    Task<Referral> CreateRetrospectiveAsync(ReferralCreate businessModel);
    Task<bool> Exists(int id, bool activeOnly = true);
    Task<Referral> GetAsync(int id, bool activeOnly = true, bool asNoTracking = true);
    Task<int?> GetCcgIdFromReferralPatient(int id);
    Task<Referral> GetEditByIdAsync(
      int id, 
      bool activeOnly = true, 
      bool asNoTracking = true
    );
    Task<IEnumerable<Referral>> GetListAsync(
      bool activeOnly = true, 
      bool asNoTracking = true
    );
    Task<IEnumerable<Referral>> GetListAsync(      
      List<int> excludeStatusIds,
      List<int> includeStatusIds,
      bool activeOnly = true,
      bool asNoTracking = true
    );
    Task<Referral> GetViewByIdAsync(
      int id, bool activeOnly = true, bool asNoTracking = true);
    Task<bool> HasCurrentAssessment(int id);
    Task<Referral> UpdateAsync(ReferralUpdate model);
    Task<Referral> UpdateRetrospectiveAsync(ReferralUpdate model);
  }
}