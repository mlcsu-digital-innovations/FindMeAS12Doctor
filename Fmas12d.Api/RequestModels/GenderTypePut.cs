using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class GenderTypePut : GenderType
  {
    [Required]
    public bool? IsActive { get; set; }
  }
}