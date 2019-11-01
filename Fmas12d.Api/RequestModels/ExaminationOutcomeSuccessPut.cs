using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels
{
  public class ExaminationOutcomeSuccessPut : ExaminationOutcomePut
  {
    public ExaminationOutcomeSuccessPut() {}
    public ExaminationOutcomeSuccessPut(BusinessModels.ExaminationOutcome model) : base(model)
    {
    }

    internal override BusinessModels.ExaminationOutcome MapToBusinessModel(int id)
    {
      BusinessModels.ExaminationOutcome model = base.MapToBusinessModel(id);
      model.IsSuccessful = true;
      return model;
    }
  }
}