using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class UserAvailability : IValidatableObject
  {
    protected Business.Models.UserAvailability _model;

    [Required]
    public DateTimeOffset? End { get; set; }
    [Required]
    public DateTimeOffset? Start { get; set; }

    internal virtual Business.Models.UserAvailability MapToBusinessModel(int userId)
    {
      _model = new Business.Models.UserAvailability
      {
        End = End.Value,
        Location = new Business.Models.Location(),
        Start = Start.Value,
        UserId = userId
      };
      return _model;
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