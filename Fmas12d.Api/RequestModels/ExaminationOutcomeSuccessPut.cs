using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels
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