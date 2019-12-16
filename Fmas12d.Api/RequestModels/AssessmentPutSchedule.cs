using System;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentPutSchedule
  {
    public AssessmentPutSchedule() { }

    public void MapToBusinessModel(Business.Models.IAssessmentUpdate model)
    {
      if (model == null) return;

      model.ScheduledTime = ScheduledTime;      
    }

    [Required]
    public DateTimeOffset? ScheduledTime { get; set; }
  }
}