using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public class PutUser
  {
    [Required]
    public bool? IsActive { get; set; }
  }
}