using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public abstract class NameDescription 
  {
    [StringLength(200)]
    [Required]
    public string Name { get; set; }
    [StringLength(2000)]
    [Required]
    public string Description { get; set; }
  }
}