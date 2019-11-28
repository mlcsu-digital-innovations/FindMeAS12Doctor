using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentListSearch : IValidatableObject
  {
    [Range(1, int.MaxValue)]
    public int? AmhpUserId { get; set; }

    [Range(1, int.MaxValue)]
    public int? DoctorUserId { get; set; }

    [Range(1, int.MaxValue)]
    public int? DoctorStatusId { get; set; }
    
    [Range(1, int.MaxValue)]
    public int? ReferralStatusId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (!AmhpUserId.HasValue && !DoctorUserId.HasValue)
      {
        yield return new ValidationResult(
            "The AmhpUserId or DoctorUserId field is required.",
            new[] { "AmhpUserId", "DoctorUserId" });
      }
    }
  }
}