using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class DoctorStatusPut : DoctorStatus
  {
    [Required]
    public bool? IsActive { get; set; }
  }
}