using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Mep.Api.RequestModels
{
  public class UserSearch : IValidatableObject
  {
    public string AmhpName { get; set; }
    public string DoctorName { get; set; }

    public bool IsByAmhpName { get { return !string.IsNullOrWhiteSpace(AmhpName); } }

    public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (string.IsNullOrWhiteSpace(AmhpName) && string.IsNullOrWhiteSpace(DoctorName))
      {
        yield return new ValidationResult(
            "At least one search criteria must be non null.",
            new[] { "AmhpName", "DoctorName" }
        );
      }
    }
  }
}