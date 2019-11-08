using System;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentPutEmergency : AssessmentPostPut
  {
    public AssessmentPutEmergency() { }

    public override void MapToBusinessModel(Business.Models.IAssessmentUpdate model)
    {
      if (model == null) return;
      base.MapToBusinessModel(model);

      model.MustBeCompletedBy = MustBeCompletedBy;
    }

    [Required]
    public DateTimeOffset? MustBeCompletedBy { get; set; }
  }
}