using BusinessModels = Mep.Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Mep.Api.RequestModels
{
  public class ExaminationOutcomePut : IValidatableObject
  {
    public ExaminationOutcomePut() {}
    public ExaminationOutcomePut(BusinessModels.ExaminationOutcome model)
    {
      AttendingDoctors = new List<ExaminationOutcomePutDoctor>();
      foreach (var doctor in model.AttendingDoctors)
      {
        AttendingDoctors.Add(new ExaminationOutcomePutDoctor()
        {
          Attended = doctor.Attended,
          Id = doctor.Id
        });
      }
      CompletedTime = model.CompletedTime;
    }

    [Required]
    public List<ExaminationOutcomePutDoctor> AttendingDoctors { get; set; }
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

    internal virtual BusinessModels.ExaminationOutcome MapToBusinessModel(int id)
    {
      BusinessModels.ExaminationOutcome model = new BusinessModels.ExaminationOutcome()
      {
        AttendingDoctors = new List<BusinessModels.ExaminationOutcomeDoctor>(),
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