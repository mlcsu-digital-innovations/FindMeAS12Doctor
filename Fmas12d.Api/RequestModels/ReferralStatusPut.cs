using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public class ReferralStatusPut : ReferralStatus
  {
    [Required]
    public bool? IsActive { get; set; }
  }
}