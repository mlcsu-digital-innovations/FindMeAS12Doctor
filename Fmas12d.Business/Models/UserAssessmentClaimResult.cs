namespace Fmas12d.Business.Models
{
  public class UserAssessmentClaimResult
  {
    public decimal AssessmentPayment { get; set; }
    public string EndPostcode { get; set; }
    public bool IsValidClaim { get; set; }
    public int Mileage { get; set; }
    public decimal MileagePayment { get; set; }
    public string StartPostcode { get; set; }
  }
}