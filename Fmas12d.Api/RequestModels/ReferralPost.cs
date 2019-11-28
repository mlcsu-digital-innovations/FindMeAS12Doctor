using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class ReferralPost
  {
    internal virtual void MapToBusinessModel(Business.Models.ReferralCreate model)
    {
      model.LeadAmhpUserId = LeadAmhpUserId.Value;
      model.PatientId = PatientId.Value;
    }       

    [Required]
    public int? LeadAmhpUserId { get; set; }
    [Required]
    public int? PatientId { get; set; }
  }
}