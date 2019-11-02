using BusinessModels = Fmas12d.Business.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentOutcomeFailurePut : AssessmentOutcomePut
  {
    public AssessmentOutcomeFailurePut() {}

    public AssessmentOutcomeFailurePut(BusinessModels.AssessmentOutcome model)
      : base(model)
    {
      UnsuccessfulAssessmentTypeId = model.UnsuccessfulAssessmentTypeId;
    }

    [Required]
    [Range(1, int.MaxValue)]
    public int? UnsuccessfulAssessmentTypeId { get; set; }

    internal override BusinessModels.AssessmentOutcome MapToBusinessModel(int id)
    {
      BusinessModels.AssessmentOutcome model = base.MapToBusinessModel(id);
      model.UnsuccessfulAssessmentTypeId = UnsuccessfulAssessmentTypeId;
      model.IsSuccessful = false;
      return model;
    }
  }
}