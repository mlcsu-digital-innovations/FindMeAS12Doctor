using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public class ReferralPut : Referral
  {
    [Required]
    public bool? IsActive { get; set; }
  }
}