using System.ComponentModel.DataAnnotations;
using Mep.Api.SharedModels;

namespace Mep.Api.RequestModels
{
  public class PutSpeciality : NameDescription
  {
    [Required]
    public bool? IsActive { get; set; }
  }
}