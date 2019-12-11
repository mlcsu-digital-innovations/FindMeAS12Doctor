using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels
{
  public abstract class Patient : IValidatableObject
  {
    protected Patient() { }

    protected Patient(Business.Models.Patient model)
    {
      if (model == null) return;

      AlternativeIdentifier = model.AlternativeIdentifier;
      CcgId = model.CcgId;
      GpPracticeId = model.GpPracticeId;
      NhsNumber = model.NhsNumber;
      ResidentialPostcode = model.ResidentialPostcode;
    }

    [MaxLength(200)]
    public string AlternativeIdentifier { get; set; }
    [Range(1, int.MaxValue)]
    public int? CcgId { get; set; }
    [Range(1, int.MaxValue)]
    public int? GpPracticeId { get; set; }
    [Range(1000000000, 9999999999)]
    public long? NhsNumber { get; set; }
    [MaxLength(10)]
    public string ResidentialPostcode { get; set; }

    internal virtual void MapToBusinessModel(Business.Models.Patient model)
    {
      model.AlternativeIdentifier = AlternativeIdentifier;
      model.CcgId = CcgId;
      model.GpPracticeId = GpPracticeId;
      model.NhsNumber = NhsNumber;
      model.ResidentialPostcode = ResidentialPostcode;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (NhsNumber.HasValue)
      {
        if (!Business.Helpers.NhsNumber.IsValid((long)NhsNumber))
        {
          yield return new ValidationResult(
              "The NhsNumber field has an invalid value that fails the modulus 11 check.",
              new[] { "NhsNumber" });
        }
      }
      else
      {
        if (string.IsNullOrWhiteSpace(AlternativeIdentifier))
        {
          yield return new ValidationResult(
              "At least one of the NhsNumber or AlternativeIdentifier fields must be provided.",
              new[] { "AlternativeIdentifier", "NhsNumber" });
        }
        else
        {
          if (!AlternativeIdentifier.Any(char.IsLetter) ||
              !AlternativeIdentifier.Any(char.IsNumber))
          {
            yield return new ValidationResult(
                "The AlternativeIdentifier field must contain at least one letter and one number.",
                new[] { "AlternativeIdentifier" });
          }
        }
      }

      if (GpPracticeId.HasValue &&
               !string.IsNullOrWhiteSpace(ResidentialPostcode) &&
               CcgId.HasValue)
      {
        yield return new ValidationResult(
            "The GpPractice and ResidentialPostcode fields must not be provided when the CcgId field is provided.",
            new[] { "GpPracticeId", "ResidentialPostcode" });
      }
      else if (!string.IsNullOrWhiteSpace(ResidentialPostcode) &&
               CcgId.HasValue)
      {
        yield return new ValidationResult(
            "The ResidentialPostcode field must not be provided when the CcgId field is provided.",
            new[] { "ResidentialPostcode" });
      }
      else if (GpPracticeId.HasValue &&
               CcgId.HasValue)
      {
        yield return new ValidationResult(
            "The GpPracticeId field must not be provided when the CcgId field is provided.",
            new[] { "GpPracticeId" });
      }
      else if (GpPracticeId.HasValue &&
               !string.IsNullOrWhiteSpace(ResidentialPostcode))
      {
        yield return new ValidationResult(
            "The GpPractice field must not be provided when the ResidentialPostcode field is provided.",
            new[] { "GpPracticeId" });
      }

    }
  }
}