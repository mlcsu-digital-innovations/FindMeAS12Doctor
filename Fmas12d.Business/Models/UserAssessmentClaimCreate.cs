namespace Fmas12d.Business.Models
{
  public class UserAssessmentClaimCreate
  {
    public string EndPostcode { get; set; }
    public bool IsWithinContract { get; set; }
    public string StartPostcode { get; set; }
    public int? NextAssessmentId { get; set; }
    public int? PreviousAssessmentId { get; set; }
  }
}