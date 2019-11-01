using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class SpecialityPut : Speciality
  {
    [Required]
    public bool? IsActive { get; set; }

  }  
}