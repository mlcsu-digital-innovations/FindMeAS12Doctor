using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public class UserPut : User
  {
    [Required]
    public bool? IsActive { get; set; }
  }

}