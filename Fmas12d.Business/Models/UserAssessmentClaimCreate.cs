namespace Fmas12d.Business.Models
{
  public class UserAssessmentClaimCreate
  {
    public string EndPostcode { get; set; }
    public bool OwnPatient { get; set; }
    public string StartPostcode { get; set; }
  }
}