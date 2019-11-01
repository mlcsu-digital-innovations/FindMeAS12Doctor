namespace Mep.Api.RequestModels
{
  public class ReferralPost : Referral
  {
    internal virtual Business.Models.ReferralCreate MapToBusinessModel()
    {
      Business.Models.ReferralCreate model = new Business.Models.ReferralCreate()
      {
        CreatedAt = CreatedAt,
        CreatedByUserId = (int)CreatedByUserId,
        PatientId = (int)PatientId,
        LeadAmhpUserId = LeadAmhpUserId
      };
      return model;
    }       
  }
}