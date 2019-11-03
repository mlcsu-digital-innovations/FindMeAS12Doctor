using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentOutcomeSuccessPut : AssessmentOutcomePut
  {
    public AssessmentOutcomeSuccessPut() {}
    public AssessmentOutcomeSuccessPut(BusinessModels.AssessmentOutcome model) : base(model)
    {
    }

    internal override BusinessModels.AssessmentOutcome MapToBusinessModel(int id)
    {
      BusinessModels.AssessmentOutcome model = base.MapToBusinessModel(id);
      model.IsSuccessful = true;
      return model;
    }
  }
}