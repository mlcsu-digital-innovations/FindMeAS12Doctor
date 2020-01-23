using System;

namespace Fmas12d.Api.ViewModels
{
    public class UserAssessmentClaim
    {
      public UserAssessmentClaim(Business.Models.UserAssessmentClaim model)
      {
        if (model == null) return;

        Assessment = new Assessment(model.Assessment);
        AssessmentPayment = model.AssessmentPayment;
        ClaimReference = model.ClaimReference;
        ClaimStatus = model.ClaimStatus.Name;
        LastUpdated = model.ModifiedAt;
        Mileage = model.Mileage;
        MileagePayment = model.MileagePayment;
      } 

      public Assessment Assessment {get; set; }
      public decimal? AssessmentPayment { get; set; }
      public string ClaimStatus {get; set;}
      public int? ClaimReference {get; set;}
      public DateTimeOffset LastUpdated { get; set; }
      public int? Mileage { get; set; }
      public decimal? MileagePayment { get; set; }
    }
}