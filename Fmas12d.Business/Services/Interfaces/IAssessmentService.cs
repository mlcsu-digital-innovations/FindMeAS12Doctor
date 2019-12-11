using Fmas12d.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public interface IAssessmentService : IServiceBase
  {
    Task<IAssessmentDoctorsUpdate> AddAllocatedDoctorsAsync(IAssessmentDoctorsUpdate model);
    Task<IAssessmentDoctorsUpdate> AddAllocatedDoctorDirectAsync(int id, int userId);
    Task<IAssessmentDoctorsUpdate> AddSelectedDoctorsAsync(IAssessmentDoctorsUpdate model);
    Task<AssessmentCreate> CreateAsync(AssessmentCreate model);
    Task<IEnumerable<Assessment>> GetListByUserIdAsync(
      int userId,
      int? doctorStatusId,
      int? referralStatusId, 
      bool asNoTracking, 
      bool activeOnly
    );
    Task<Assessment> GetAvailableDoctorsAsync(int id, bool asNoTracking, bool activeOnly);
    Task<Assessment> GetByIdAsync(int id, bool activeOnly, bool asNoTracking);    
    Task<Assessment> GetByIdForUserAsync(int id, int userId, bool asNoTracking, bool activeOnly);
    Task<Assessment> GetSelectedDoctorsAsync(int id, bool asNoTracking, bool activeOnly);
    Task<bool> Schedule(int id);
    Task<AssessmentDoctor> UpdateAssessmentDoctorAcceptance(AssessmentDoctor businessModel);
    Task<AssessmentUpdate> UpdateAsync(AssessmentUpdate model);
    Task<AssessmentOutcome> UpdateOutcomeAsync(AssessmentOutcome model);    
  }
}