using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public class CcgPut : Ccg
  {
    [Required]
    public bool? IsActive { get; set; }
  }
}