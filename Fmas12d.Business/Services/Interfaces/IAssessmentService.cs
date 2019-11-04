using Fmas12d.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public interface IAssessmentService : IServiceBaseNoAutoMapper
  {
    Task<AssessmentCreate> CreateAsync(AssessmentCreate model);
    Task<IEnumerable<Assessment>> GetAllFilterByAmhpUserIdAsync(
      int amhpUserId, bool asNoTracking, bool activeOnly);
    Task<Assessment> GetByIdAsync(int id, bool activeOnly);
    Task<AssessmentOutcome> UpdateOutcomeAsync(AssessmentOutcome model);
  }
}