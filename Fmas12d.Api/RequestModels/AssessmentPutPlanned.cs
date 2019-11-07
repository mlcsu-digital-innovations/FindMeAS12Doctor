using System;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentPutPlanned : AssessmentPostPut
  {
    public AssessmentPutPlanned() { }

    public override void MapToBusinessModel(Business.Models.IAssessmentUpdate model)
    {
      if (model == null) return;
      base.MapToBusinessModel(model);

      model.ScheduledTime = ScheduledTime;      
    }

    [Required]
    public DateTimeOffset? ScheduledTime { get; set; }
  }
}