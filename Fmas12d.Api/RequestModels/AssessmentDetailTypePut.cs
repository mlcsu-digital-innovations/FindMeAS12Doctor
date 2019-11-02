using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentDetailTypePut : ReferralStatus
  {
    [Required]
    public bool? IsActive { get; set; }
  }
}