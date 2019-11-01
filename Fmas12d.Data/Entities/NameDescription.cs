using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Data.Entities
{
  public abstract class NameDescription : BaseEntity, INameDescription
  {
    [MaxLength(2000)]
    [Required]
    public string Description { get; set; }    
    [MaxLength(200)]
    [Required]
    public string Name { get; set; }
  }
}