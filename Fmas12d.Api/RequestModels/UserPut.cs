using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class UserPut : User
  {
    [Required]
    public bool? IsActive { get; set; }
  }

}