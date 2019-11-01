using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public class ExaminationDetailTypePut : ReferralStatus
  {
    [Required]
    public bool? IsActive { get; set; }
  }
}