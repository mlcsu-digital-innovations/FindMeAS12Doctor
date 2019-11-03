using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentListSearch : IValidatableObject
  {
    [Range(1, int.MaxValue)]
    public int? AmhpUserId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (!AmhpUserId.HasValue)
      {
        yield return new ValidationResult(
            "The AmhpUserId field is required.",
            new[] { "AmhpUserId" });
      }
    }
  }
}