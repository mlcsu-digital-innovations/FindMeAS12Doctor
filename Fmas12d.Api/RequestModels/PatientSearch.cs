using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Fmas12d.Api.RequestModels
{
  public class PatientSearch : IValidatableObject
  {
    public string AlternativeIdentifier { get; set; }
    public string NhsNumber { get; set; }

    public long NhsNumberAsLong
    { get { return NhsNumber?.All(char.IsNumber) ?? false ? long.Parse(NhsNumber) : 0; } }

    public bool IsByNhsNumber { get { return !string.IsNullOrWhiteSpace(NhsNumber); } }

    public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (string.IsNullOrWhiteSpace(AlternativeIdentifier) && string.IsNullOrWhiteSpace(NhsNumber))
      {
        yield return new ValidationResult(
            "At least one search criteria must be non null.",
            new[] { "AlternativeIdentifier", "NhsNumber" }
        );
      }
      else if (!string.IsNullOrWhiteSpace(NhsNumber) &&
               !Business.Helpers.NhsNumber.IsValid(NhsNumber)
      )
      {
        yield return new ValidationResult(
            $"The provided NhsNumber {NhsNumber} is invalid.",
            new[] { "NhsNumber" }
        );
      }

    }

  }

}