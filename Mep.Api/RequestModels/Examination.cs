using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public class Examination : IValidatableObject
  {
    [Required]
    [MaxLength(200)]
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Address4 { get; set; }
    [Range(1, int.MaxValue)]
    public int AmhpUserId { get; set; }
    public List<int> DetailTypeIds { get; set; }
    [Required]
    public bool? IsPlanned { get; set; }
    [MaxLength(2000)]
    public string MeetingArrangementComment { get; set; }
    public DateTimeOffset? MustBeCompletedBy { get; set; }
    [Required]
    [MaxLength(10)]
    public string Postcode { get; set; }
    [Range(1, int.MaxValue)]
    public int? PreferredDoctorGenderTypeId { get; set; }
    [Range(1, int.MaxValue)]
    public int ReferralId { get; set; }
    public DateTimeOffset? ScheduledTime { get; set; }
    [Range(1, int.MaxValue)]
    public int? SpecialityId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if ((bool)IsPlanned && MustBeCompletedBy.HasValue)
      {
        yield return new ValidationResult(
            "The MustBeCompletedBy field should not be provided when the examination is planned.",
            new[] { "MustBeCompletedBy" });
      }   
      if ((bool)IsPlanned && !ScheduledTime.HasValue)
      {
        yield return new ValidationResult(
            "The ScheduledTime field is required when the examination is planned.",
            new[] { "ScheduledTime" });
      }
      if (!(bool)IsPlanned && ScheduledTime.HasValue)
      {
        yield return new ValidationResult(
            "The ScheduledTime field should not be provided when the examination is unplanned.",
            new[] { "ScheduledTime" });
      }      
      if (!(bool)IsPlanned && !MustBeCompletedBy.HasValue)
      {
        yield return new ValidationResult(
            "The MustBeCompletedBy field is required when the examination is unplanned.",
            new[] { "MustBeCompletedBy" });
      }
    }
  }
}