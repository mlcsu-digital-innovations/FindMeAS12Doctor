namespace Fmas12d.Api.RequestModels
{
  public class UserAssessmentClaimPost
  {
    public bool WithinContract { get; set; }
    public string EndPostcode { get; set; }
    public string StartPostcode { get; set; }
    public int? PreviousAssessmentId { get; set; }
    public int? NextAssessmentId { get; set; }

    internal virtual void MapToBusinessModel(Business.Models.UserAssessmentClaimCreate model)
    {
      model.EndPostcode = EndPostcode;
      model.WithinContract = WithinContract;
      model.StartPostcode = StartPostcode;
      model.PreviousAssessmentId = PreviousAssessmentId;
      model.NextAssessmentId = NextAssessmentId;
    }
  }
}