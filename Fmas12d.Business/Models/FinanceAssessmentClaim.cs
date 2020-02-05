using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Models
{
  public class FinanceAssessmentClaim
  {
    public FinanceAssessmentClaim(Data.Entities.UserAssessmentClaim entity) {
      Assessment = new Assessment(entity.Assessment);
      AssessmentPayment = entity.AssessmentPayment;
      Ccg = new Ccg(entity.Assessment.Ccg);
      Claimant = new User(entity.User);
      ClaimReference = entity.ClaimReference;
      ClaimStatus = new ClaimStatus(entity.ClaimStatus);
      Id = entity.Id;
      Mileage = entity.Mileage;
      MileagePayment = entity.MileagePayment;

      Claimant.BankDetails = Claimant.BankDetails.Where(bd => bd.CcgId == Ccg.Id).ToList();
    }

    public int? ClaimReference { get; set; }
    public Assessment Assessment { get; set; }
    public ClaimStatus ClaimStatus { get; set; }
    public int Id { get; set; }
    public User Claimant { get; set; }
    public Ccg Ccg { get; set; }

    public int? Mileage { get; set; }
    public decimal? MileagePayment { get; set; }
    public decimal? AssessmentPayment { get; set; }

    public static Expression<Func<Data.Entities.UserAssessmentClaim, FinanceAssessmentClaim>> ProjectFromEntity
    {
      get
      {
        return entity => new FinanceAssessmentClaim(entity);
      }
    }
  }
}