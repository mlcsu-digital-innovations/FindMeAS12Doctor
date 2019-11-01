using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class ReferralPut : Referral
  {
    [Required]
    public bool? IsActive { get; set; }
  }
}