using System;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentPostEmergency : AssessmentPostPut
  {
    public AssessmentPostEmergency() { }

    public override void MapToBusinessModel(Business.Models.IAssessmentUpdate model)
    {
      if (model == null) return;
      base.MapToBusinessModel(model);
      
      model.MustBeCompletedBy = MustBeCompletedBy;
      (model as Business.Models.IAssessmentCreate).ReferralId = ReferralId;
    }

    [Required]
    public DateTimeOffset? MustBeCompletedBy { get; set; }
    [Range(1, int.MaxValue)]
    public int ReferralId { get; set; }
  }
}