using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public class DoctorStatusPut : DoctorStatus
  {
    [Required]
    public bool? IsActive { get; set; }
  }
}