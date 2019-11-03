using Fmas12d.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public interface IReferralService : IServiceBaseNoAutoMapper
  {
    Task<Referral> CreateAsync(ReferralCreate businessModel);
    Task<int?> GetCcgIdFromReferralPatient(int id);
    Task<Referral> GetEditByIdAsync(
      int id, bool activeOnly = true, bool asNoTracking = true);
    Task<IEnumerable<Referral>> GetListAsync(
      bool activeOnly = true, bool asNoTracking = true);
    Task<Referral> GetViewByIdAsync(
      int id, bool activeOnly = true, bool asNoTracking = true);
  }
}