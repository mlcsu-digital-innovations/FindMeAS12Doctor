using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Fmas12d.Api.RequestModels
{
  public class UserSearch : IValidatableObject
  {
    public string AmhpName { get; set; }
    public string DoctorName { get; set; }
    public int GmcNumber {get; set;}
    public bool IsByAmhpName { get { return !string.IsNullOrWhiteSpace(AmhpName); } }
    public bool IsByDoctorName { get { return !string.IsNullOrWhiteSpace(DoctorName);} }
    public bool IsByGmcNumber { get { return GmcNumber != default(int); } }

    public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (string.IsNullOrWhiteSpace(AmhpName) && string.IsNullOrWhiteSpace(DoctorName) && GmcNumber == default(int))
      {
        yield return new ValidationResult(
            "At least one search criteria must be non null.",
            new[] { "AmhpName", "DoctorName", "GmcNumber" }
        );
      }
    }
  }
}