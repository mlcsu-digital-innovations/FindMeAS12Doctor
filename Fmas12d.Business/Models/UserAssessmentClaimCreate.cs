namespace Fmas12d.Business.Models
{
  public class UserAssessmentClaimCreate
  {
    public string EndPostcode { get; set; }
    public bool OwnPatient { get; set; }
    public string StartPostcode { get; set; }
    public int? NextAssessmentId { get; set; }
    public int? PreviousAssessmentId { get; set; }
  }
}