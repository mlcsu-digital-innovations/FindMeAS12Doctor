using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class UserAvailability : IValidatableObject
  {
    [Required]
    public DateTimeOffset? End { get; set; }
    [Required]
    public DateTimeOffset? Start { get; set; }

    internal virtual void MapToBusinessModel(Business.Models.IUserAvailability model)
    {
      if (model != null)
      {
        model.End = End.Value;
        model.Start = Start.Value;
      }
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (Start >= End)
      {
        yield return new ValidationResult(
            "The start field must be before the end field.",
            new[] { "Start" }
        );
      }
    }
  }
}