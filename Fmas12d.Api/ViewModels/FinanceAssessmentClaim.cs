using System;
namespace Fmas12d.Api.ViewModels
{
  public class FinanceAssessmentClaim : UserAssessmentClaim
  {
    public FinanceAssessmentClaim(Business.Models.FinanceAssessmentClaim model) : base(model) {
      if (model == null) return;

      Ccg = new Ccg(model.Ccg);
      ClaimStatus = new ClaimStatus(model.ClaimStatus);
      Claimant = new User(model.Claimant);
    }

    public FinanceAssessmentClaim() {}

    public new ClaimStatus ClaimStatus { get; set; }
    public virtual User Claimant { get; set; }
    public virtual Ccg Ccg { get; set; }

    public static new Func<Business.Models.FinanceAssessmentClaim, FinanceAssessmentClaim> ProjectFromModel
    {
      get
      {
        return financeAssessmentClaim => new FinanceAssessmentClaim()
        {
          Assessment = new Assessment(financeAssessmentClaim.Assessment),
          AssessmentPayment = financeAssessmentClaim.AssessmentPayment,
          Ccg = new Ccg(financeAssessmentClaim.Ccg),
          ClaimReference = financeAssessmentClaim.ClaimReference,
          ClaimStatus = new ClaimStatus(financeAssessmentClaim.ClaimStatus),
          ClaimStatusId = financeAssessmentClaim.ClaimStatusId,
          Claimant = new User(financeAssessmentClaim.Claimant),
          Id = financeAssessmentClaim.Id,
          LastUpdated = financeAssessmentClaim.LastUpdated,
          Mileage = financeAssessmentClaim.Mileage,
          MileagePayment = financeAssessmentClaim.MileagePayment,
          ExportedDate = financeAssessmentClaim.ExportedDate
        };
      }
    }
  }
}