using System;
namespace Fmas12d.Api.ViewModels
{
  public class FinanceAssessmentClaim
  {
    public FinanceAssessmentClaim(Business.Models.FinanceAssessmentClaim model) {
      if (model == null) return;

      Assessment = new Assessment(model.Assessment);
      ClaimReference = model.ClaimReference;
      Ccg = new Ccg(model.Ccg);
      ClaimStatus = new IdNameDescription{
        Id = model.ClaimStatus.Id,
        Name = model.ClaimStatus.Name,
        Description = model.ClaimStatus.Description
      };
      Claimant = new User(model.Claimant);
      Id = model.Id;
    }

    public FinanceAssessmentClaim() {}

    public virtual Assessment Assessment { get; set; }
    public int? ClaimReference { get; set; }
    public IdNameDescription ClaimStatus { get; set; }
    public virtual User Claimant { get; set; }
    public virtual Ccg Ccg { get; set; }

    public int Id { get; set; }

    public static Func<Business.Models.FinanceAssessmentClaim, FinanceAssessmentClaim> ProjectFromModel
    {
      get
      {
        return financeAssessmentClaim => new FinanceAssessmentClaim()
        {
          Assessment = new Assessment(financeAssessmentClaim.Assessment),
          Ccg = new Ccg(financeAssessmentClaim.Ccg),
          ClaimReference = financeAssessmentClaim.ClaimReference,
          ClaimStatus = new IdNameDescription{
            Id = financeAssessmentClaim.ClaimStatus.Id,
            Name = financeAssessmentClaim.ClaimStatus.Name,
            Description = financeAssessmentClaim.ClaimStatus.Description
          },
          Claimant = new User(financeAssessmentClaim.Claimant),
          Id = financeAssessmentClaim.Id
        };
      }
    }
  }
}