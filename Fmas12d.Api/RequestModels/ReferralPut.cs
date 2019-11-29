using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class ReferralPut
  {
    [Required]
    public int? LeadAmhpUserId { get; set; }

    internal virtual void MapToBusinessModel(Business.Models.ReferralUpdate model)
    {      
      model.LeadAmhpUserId = LeadAmhpUserId.Value;
    }    
  }
}