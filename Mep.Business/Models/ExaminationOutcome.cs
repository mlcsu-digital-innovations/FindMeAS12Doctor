using System;
using System.Collections.Generic;

namespace Mep.Business.Models
{
  public class ExaminationOutcome
  {
    public IList<ExaminationOutcomeDoctor> AttendingDoctors { get; set; }
    public DateTimeOffset CompletedTime { get; set; }
    public int Id { get; set; }
    public bool IsSuccessful { get; set; }
    public int? UnsuccessfulExaminationTypeId { get; set; }
  }
}