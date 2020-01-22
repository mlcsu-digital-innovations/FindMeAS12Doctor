using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class UserOnCallPutConfirmation : IValidatableObject
  {
    [Required]
    public bool? IsConfirmed { get; set; }

    [MaxLength(4000)]
    public string Reason { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (IsConfirmed.HasValue)
      {
        if (!IsConfirmed.Value && string.IsNullOrWhiteSpace(Reason))
        {
          yield return new ValidationResult(
              "The reason field must be supplied if isConfirmed is false.",
              new[] { "Reason" }
          );
        }
      }
    }
  }

}