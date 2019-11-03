using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class PatientPut : Patient
  {
    [Required]
    public bool? IsActive { get; set; }
  }
}