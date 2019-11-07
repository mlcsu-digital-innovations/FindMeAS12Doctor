namespace Fmas12d.Api.ViewModels
{
  public class AssessmentPost : AssessmentPut
  {
    public AssessmentPost() {}
    public AssessmentPost(Business.Models.IAssessmentCreate model) : base(model)
    {
      if (model == null) return ;

      ReferralId = model.ReferralId;
    }
    public int ReferralId { get; set; }    
  }
}