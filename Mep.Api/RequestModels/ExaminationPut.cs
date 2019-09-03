using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public class ExaminationPut : Examination
  {
    [Required]
    public bool? IsActive { get; set; }
  }
}