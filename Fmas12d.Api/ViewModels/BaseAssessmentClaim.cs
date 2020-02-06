using System;

namespace Fmas12d.Api.ViewModels
{
  public class BaseAssessmentClaim
  {
    public BaseAssessmentClaim() { }

    public BaseAssessmentClaim(Business.Models.UserAssessmentClaim model)
    {
      if (model == null) return;

      Assessment = new Assessment(model.Assessment);
      AssessmentPayment = model.AssessmentPayment;
      ClaimReference = model.ClaimReference;
      ClaimStatus = model.ClaimStatus.Name;
      ClaimStatusId = model.ClaimStatus.Id;
      Id = model.Id;
      LastUpdated = model.ModifiedAt;
      Mileage = model.Mileage;
      MileagePayment = model.MileagePayment;
    }

    public Assessment Assessment { get; set; }
    public decimal? AssessmentPayment { get; set; }
    public string ClaimStatus { get; set; }
    public int? ClaimStatusId { get; set; }
    public int? ClaimReference { get; set; }
    public int Id { get; set; }
    public DateTimeOffset LastUpdated { get; set; }
    public int? Mileage { get; set; }
    public decimal? MileagePayment { get; set; }

    public static Func<Business.Models.BaseAssessmentClaim, BaseAssessmentClaim> ProjectFromModel
    {
      get
      {
        return baseAssessmentClaim => new BaseAssessmentClaim()
        {
          Assessment = new Assessment(baseAssessmentClaim.Assessment),
          AssessmentPayment = baseAssessmentClaim.AssessmentPayment,
          ClaimReference = baseAssessmentClaim.ClaimReference,
          ClaimStatus = baseAssessmentClaim.ClaimStatus.Name,
          Id = baseAssessmentClaim.Id,
          Mileage = baseAssessmentClaim.Mileage,
          MileagePayment = baseAssessmentClaim.MileagePayment
        };
      }
    }
  }
}