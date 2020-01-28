namespace Fmas12d.Api.RequestModels
{
  public class UserAssessmentClaimPost
  {
    public bool OwnPatient { get; set; }
    public string EndPostcode { get; set; }
    public string StartPostcode { get; set; }

    internal virtual void MapToBusinessModel(Business.Models.UserAssessmentClaimCreate model)
    {
      model.EndPostcode = EndPostcode;
      model.OwnPatient = OwnPatient;
      model.StartPostcode = StartPostcode;
    }
  }
}