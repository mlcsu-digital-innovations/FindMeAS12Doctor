using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class ExaminationDetailTypePut : ReferralStatus
  {
    [Required]
    public bool? IsActive { get; set; }
  }
}