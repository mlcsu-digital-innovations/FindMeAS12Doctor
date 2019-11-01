using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public class PatientPut : Patient
  {
    [Required]
    public bool? IsActive { get; set; }
  }
}