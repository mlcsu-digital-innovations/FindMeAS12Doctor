namespace Fmas12d.Api.RequestModels
{
  public class ReferralPost : Referral
  {
    internal virtual Business.Models.ReferralCreate MapToBusinessModel()
    {
      Business.Models.ReferralCreate model = new Business.Models.ReferralCreate()
      {
        CreatedAt = CreatedAt,
        PatientId = PatientId.Value,
        LeadAmhpUserId = LeadAmhpUserId
      };
      return model;
    }       
  }
}