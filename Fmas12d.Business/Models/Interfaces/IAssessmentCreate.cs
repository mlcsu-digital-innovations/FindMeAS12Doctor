namespace Fmas12d.Business.Models
{
  public interface IAssessmentCreate : IAssessmentUpdate
  {
    int ReferralId { get; set; }
  }
}