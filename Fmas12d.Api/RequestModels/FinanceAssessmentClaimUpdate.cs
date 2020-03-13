namespace Fmas12d.Api.RequestModels
{
  public class FinanceAssessmentClaimUpdate
  {
    public int ClaimStatusId { get; set; }
    public int[] ClaimIds { get; set; }

    internal virtual void MapToBusinessModel(Business.Models.FinanceAssessmentClaimUpdate model)
    {
      model.ClaimStatusId = ClaimStatusId;
      model.ClaimIds = ClaimIds;
    }
  }
}