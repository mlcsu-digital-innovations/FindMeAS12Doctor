using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentListSearch
  {
    [Range(1, int.MaxValue)]
    public int? DoctorStatusId { get; set; }
    
    [Range(1, int.MaxValue)]
    public int? ReferralStatusId { get; set; }
  }
}