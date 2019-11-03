using System;
using System.Collections.Generic;

namespace Fmas12d.Business.Models
{
  public class AssessmentOutcome
  {
    public IList<AssessmentOutcomeDoctor> AttendingDoctors { get; set; }
    public DateTimeOffset CompletedTime { get; set; }
    public int Id { get; set; }
    public bool IsSuccessful { get; set; }
    public int? UnsuccessfulAssessmentTypeId { get; set; }
  }
}