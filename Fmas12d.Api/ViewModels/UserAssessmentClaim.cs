namespace Fmas12d.Api.ViewModels
{
    public class UserAssessmentClaim
    {
      public UserAssessmentClaim(Business.Models.UserAssessmentClaim model)
      {
        if (model == null) return;

        Assessment = new Assessment(model.Assessment);
        ClaimReference = model.ClaimReference;
        MileagePayment = model.MileagePayment;
        AssessmentPayment = model.AssessmentPayment;
        Mileage = model.Mileage;

        

      // AssessmentId = model.AssessmentId;
      // NotificationTextId = model.NotificationTextId;
      // SentAt = model.SentAt;
      // UserId = model.UserId;
      } 

      public Assessment Assessment {get; set; }

      public int? ClaimReference {get; set;}

      public decimal? MileagePayment { get; set; }
      public decimal? AssessmentPayment { get; set; }

      public int? Mileage { get; set; }
    }
}