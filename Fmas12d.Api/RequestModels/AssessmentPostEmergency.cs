using System;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentPostEmergency : Assessment
  {
    public AssessmentPostEmergency() {}
    public AssessmentPostEmergency(Business.Models.AssessmentCreate model) : base(model)
    {
      MustBeCompletedBy = model.MustBeCompletedBy;
    }

    internal override Business.Models.AssessmentCreate MapToBusinessModel()
    {
      Business.Models.AssessmentCreate model = base.MapToBusinessModel();
      model.MustBeCompletedBy = MustBeCompletedBy;
      return model;
    }

    [Required]
    public DateTimeOffset? MustBeCompletedBy { get; set; }
  }
}