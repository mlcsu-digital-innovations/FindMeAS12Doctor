using BusinessModels = Fmas12d.Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentOutcomePut : IValidatableObject
  {
    public AssessmentOutcomePut() {}
    public AssessmentOutcomePut(BusinessModels.AssessmentOutcome model)
    {
      AttendingDoctors = new List<AssessmentOutcomePutDoctor>();
      foreach (var doctor in model.AttendingDoctors)
      {
        AttendingDoctors.Add(new AssessmentOutcomePutDoctor()
        {
          Attended = doctor.Attended,
          Id = doctor.Id
        });
      }
      CompletedTime = model.CompletedTime;
    }

    [Required]
    public List<AssessmentOutcomePutDoctor> AttendingDoctors { get; set; }
    [Required]
    public DateTimeOffset? CompletedTime { get; set; }

    public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (!AttendingDoctors.Any())
      {
        yield return new ValidationResult(
            "The AttendingDoctors field must contain at least one item.",
            new[] { "AttendingDoctors" });
      } 
    }

    internal virtual BusinessModels.AssessmentOutcome MapToBusinessModel(int id)
    {
      BusinessModels.AssessmentOutcome model = new BusinessModels.AssessmentOutcome()
      {
        AttendingDoctors = new List<BusinessModels.AssessmentOutcomeDoctor>(),
        CompletedTime = (DateTimeOffset)CompletedTime,
        Id = id
      };
      AttendingDoctors.ForEach(doctor =>
      {
        model.AttendingDoctors.Add(doctor.MapToBusinessModel());
      });

      return model;
    } 
  }
}