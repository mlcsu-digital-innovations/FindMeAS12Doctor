using BusinessModels = Fmas12d.Business.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace Fmas12d.Api.RequestModels
{
  public class ExaminationOutcomeFailurePut : ExaminationOutcomePut
  {
    public ExaminationOutcomeFailurePut() {}

    public ExaminationOutcomeFailurePut(BusinessModels.ExaminationOutcome model)
      : base(model)
    {
      UnsuccessfulExaminationTypeId = model.UnsuccessfulExaminationTypeId;
    }

    [Required]
    [Range(1, int.MaxValue)]
    public int? UnsuccessfulExaminationTypeId { get; set; }

    internal override BusinessModels.ExaminationOutcome MapToBusinessModel(int id)
    {
      BusinessModels.ExaminationOutcome model = base.MapToBusinessModel(id);
      model.UnsuccessfulExaminationTypeId = UnsuccessfulExaminationTypeId;
      model.IsSuccessful = false;
      return model;
    }
  }
}