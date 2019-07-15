using System.ComponentModel.DataAnnotations;

namespace Mep.Business.Models
{
  public abstract class NameDescription : BaseModel, INameDescription
  {
    [StringLength(200)]
    [Required]
    public string Name { get; set; }
    [StringLength(2000)]
    [Required]
    public string Description { get; set; }
  }
}