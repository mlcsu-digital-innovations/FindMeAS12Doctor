using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class GpPracticePut : GpPractice
  {
    [Required]
    public bool? IsActive { get; set; }
  }
}