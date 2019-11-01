using System;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class ExaminationPostPlanned : Examination
  {
    public ExaminationPostPlanned() {}
    public ExaminationPostPlanned(Business.Models.ExaminationCreate model) : base(model)
    {
      ScheduledTime = model.ScheduledTime;
    }

    internal override Business.Models.ExaminationCreate MapToBusinessModel()
    {
      Business.Models.ExaminationCreate model = base.MapToBusinessModel();
      model.ScheduledTime = ScheduledTime;
      return model;
    }

    [Required]
    public DateTimeOffset? ScheduledTime { get; set; }
  }
}