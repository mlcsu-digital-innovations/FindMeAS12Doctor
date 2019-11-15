using Fmas12d.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public interface IAssessmentService : IServiceBaseNoAutoMapper
  {
    Task<IAssessmentDoctorsUpdate> AddSelectedDoctors(IAssessmentDoctorsUpdate model);
    Task<AssessmentCreate> CreateAsync(AssessmentCreate model);
    Task<IEnumerable<Assessment>> GetAllFilterByAmhpUserIdAsync(
      int amhpUserId, bool asNoTracking, bool activeOnly);
    Task<Assessment> GetAvailableDoctorsAsync(int id, bool asNoTracking, bool activeOnly);
    Task<Assessment> GetByIdAsync(int id, bool activeOnly);
    Task<IAssessmentDoctorsUpdate> UpdateAllocatedDoctors(IAssessmentDoctorsUpdate model);
    Task<AssessmentUpdate> UpdateAsync(AssessmentUpdate model);
    Task<AssessmentOutcome> UpdateOutcomeAsync(AssessmentOutcome model);    
  }
}