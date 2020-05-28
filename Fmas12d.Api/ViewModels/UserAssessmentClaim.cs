using System;

namespace Fmas12d.Api.ViewModels
{
  public class UserAssessmentClaim
  {
    public UserAssessmentClaim() { }

    public UserAssessmentClaim(Business.Models.UserAssessmentClaim model)
    {
      if (model == null) return;

      Assessment = new Assessment(model.Assessment);
      AssessmentPayment = model.AssessmentPayment;
      ClaimReference = model.ClaimReference;
      ClaimStatus = new ClaimStatus(model.ClaimStatus);
      ClaimStatusId = model.ClaimStatus.Id;
      Id = model.Id;
      LastUpdated = model.ModifiedAt;
      Mileage = model.Mileage;
      MileagePayment = model.MileagePayment;
    }

    public Assessment Assessment { get; set; }
    public decimal? AssessmentPayment { get; set; }
    public ClaimStatus ClaimStatus { get; set; }
    public int? ClaimStatusId { get; set; }
    public string ClaimReference { get; set; }
    public DateTimeOffset? ExportedDate { get; set; }
    public int Id { get; set; }
    public DateTimeOffset LastUpdated { get; set; }
    public decimal? Mileage { get; set; }
    public decimal? MileagePayment { get; set; }

    public static Func<Business.Models.UserAssessmentClaim, UserAssessmentClaim> ProjectFromModel
    {
      get
      {
        return userAssessmentClaim => new UserAssessmentClaim()
        {
          Assessment = new Assessment(userAssessmentClaim.Assessment),
          AssessmentPayment = userAssessmentClaim.AssessmentPayment,
          ClaimReference = userAssessmentClaim.ClaimReference,
          ClaimStatus = new ClaimStatus(userAssessmentClaim.ClaimStatus),
          Id = userAssessmentClaim.Id,
          Mileage = userAssessmentClaim.Mileage,
          MileagePayment = userAssessmentClaim.MileagePayment
        };
      }
    }
  }
}