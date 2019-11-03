using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class CcgPut : Ccg
  {
    [Required]
    public bool? IsActive { get; set; }
  }
}