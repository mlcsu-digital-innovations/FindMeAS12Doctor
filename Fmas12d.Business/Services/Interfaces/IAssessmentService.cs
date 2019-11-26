using Fmas12d.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public interface IAssessmentService : IServiceBaseNoAutoMapper
  {
    Task<IAssessmentDoctorsUpdate> AddAllocatedDoctors(IAssessmentDoctorsUpdate model);
    Task<IAssessmentDoctorsUpdate> AddSelectedDoctors(IAssessmentDoctorsUpdate model);
    Task<AssessmentCreate> CreateAsync(AssessmentCreate model);
    Task<IEnumerable<Assessment>> GetAllFilterByAmhpUserIdAsync(
      int amhpUserId, bool asNoTracking, bool activeOnly);
    Task<IEnumerable<Assessment>> GetAllFilterByDoctorUserIdAsync(
      int amhpUserId, bool asNoTracking, bool activeOnly);
    Task<Assessment> GetAvailableDoctorsAsync(int id, bool asNoTracking, bool activeOnly);
    Task<Assessment> GetByIdAsync(int id, bool activeOnly);    
    Task<Assessment> GetSelectedDoctorsAsync(int id, bool asNoTracking, bool activeOnly);
    Task<AssessmentDoctor> UpdateAssessmentDoctorAcceptance(AssessmentDoctor businessModel);
    Task<AssessmentUpdate> UpdateAsync(AssessmentUpdate model);
    Task<AssessmentOutcome> UpdateOutcomeAsync(AssessmentOutcome model);
  }
}