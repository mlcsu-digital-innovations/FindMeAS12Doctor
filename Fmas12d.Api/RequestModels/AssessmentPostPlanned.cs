using System;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentPostPlanned : AssessmentPostPut
  {
    public AssessmentPostPlanned() { }

    public override void MapToBusinessModel(Business.Models.IAssessmentUpdate model)
    {
      if (model == null) return;
      base.MapToBusinessModel(model);

      (model as Business.Models.IAssessmentCreate).ReferralId = ReferralId;
      model.ScheduledTime = ScheduledTime;      
    }

    [Range(1, int.MaxValue)]
    public int ReferralId { get; set; } 
    [Required]
    public DateTimeOffset? ScheduledTime { get; set; }
  }
}