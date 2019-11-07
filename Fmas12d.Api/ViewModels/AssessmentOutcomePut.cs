using System;
using System.Collections.Generic;

namespace Fmas12d.Api.ViewModels
{
  public class AssessmentOutcomePut
  {
    public AssessmentOutcomePut() {}
    public AssessmentOutcomePut(Business.Models.AssessmentOutcome model)
    {
      AttendingDoctors = new List<AssessmentOutcomePutDoctor>();
      foreach (Business.Models.AssessmentOutcomeDoctor doctor in model.AttendingDoctors)
      {
        AttendingDoctors.Add(new AssessmentOutcomePutDoctor(doctor));
      }
      CompletedTime = model.CompletedTime;
    }

    public List<AssessmentOutcomePutDoctor> AttendingDoctors { get; set; }
    public DateTimeOffset? CompletedTime { get; set; }

  }
}