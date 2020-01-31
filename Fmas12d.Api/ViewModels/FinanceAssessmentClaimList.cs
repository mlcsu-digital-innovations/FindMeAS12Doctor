using System;
namespace Fmas12d.Api.ViewModels
{
  public class FinanceAssessmentClaimList
  {
    public virtual Assessment Assessment { get; set; }
    public int? ClaimReference { get; set; }
    public IdNameDescription ClaimStatus { get; set; }
    public virtual User Claimant { get; set; }
    public virtual Ccg Ccg { get; set; }

    public static Func<Business.Models.FinanceAssessmentClaim, FinanceAssessmentClaimList> ProjectFromModel
    {
      get
      {
        return financeAssessmentClaim => new FinanceAssessmentClaimList()
        {
          Assessment = new Assessment(financeAssessmentClaim.Assessment),
          Ccg = new Ccg(financeAssessmentClaim.Ccg),
          ClaimReference = financeAssessmentClaim.ClaimReference,
          ClaimStatus = new IdNameDescription{
            Id = financeAssessmentClaim.ClaimStatus.Id,
            Name = financeAssessmentClaim.ClaimStatus.Name,
            Description = financeAssessmentClaim.ClaimStatus.Description
          },
          Claimant = new User(financeAssessmentClaim.Claimant)
        };
      }
    }
  }
}