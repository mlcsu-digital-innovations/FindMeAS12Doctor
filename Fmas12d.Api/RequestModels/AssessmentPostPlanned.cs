using System;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentPostPlanned : Assessment
  {
    public AssessmentPostPlanned() {}
    public AssessmentPostPlanned(Business.Models.AssessmentCreate model) : base(model)
    {
      ScheduledTime = model.ScheduledTime;
    }

    internal override Business.Models.AssessmentCreate MapToBusinessModel()
    {
      Business.Models.AssessmentCreate model = base.MapToBusinessModel();
      model.ScheduledTime = ScheduledTime;
      return model;
    }

    [Required]
    public DateTimeOffset? ScheduledTime { get; set; }
  }
}