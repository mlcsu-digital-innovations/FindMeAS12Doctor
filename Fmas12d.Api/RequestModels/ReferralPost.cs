using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class ReferralPost : ReferralPut
  {
    internal virtual void MapToBusinessModel(Business.Models.ReferralCreate model)
    {
      model.LeadAmhpUserId = LeadAmhpUserId.Value;
      model.PatientId = PatientId.Value;
    }       

    [Required]
    public int? PatientId { get; set; }
  }
}