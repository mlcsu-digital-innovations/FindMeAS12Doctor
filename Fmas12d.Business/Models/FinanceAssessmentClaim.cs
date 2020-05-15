using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Models
{
  public class FinanceAssessmentClaim : UserAssessmentClaim
  {
    public FinanceAssessmentClaim(Data.Entities.UserAssessmentClaim entity) : base(entity) {
      ClaimStatusId = entity.ClaimStatusId;
      Ccg = new Ccg(entity.Assessment.Ccg);
      Claimant = new User(entity.User);
      Claimant.BankDetails = Claimant.BankDetails?.Where(bd => bd.CcgId == Ccg.Id).ToList();
      LastUpdated = entity.ModifiedAt;     
    }

    public User Claimant { get; set; }
    public Ccg Ccg { get; set; }

    public static new Expression<Func<Data.Entities.UserAssessmentClaim, FinanceAssessmentClaim>> ProjectFromEntity
    {
      get
      {
        return entity => new FinanceAssessmentClaim(entity);
      }
    }
  }
}