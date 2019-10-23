using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public class ExaminationListSearch : IValidatableObject
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