using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public class GenderTypePut : GenderType
  {
    [Required]
    public bool? IsActive { get; set; }
  }
}