using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public class SpecialityPut : Speciality
  {
    [Required]
    public bool? IsActive { get; set; }

  }  
}