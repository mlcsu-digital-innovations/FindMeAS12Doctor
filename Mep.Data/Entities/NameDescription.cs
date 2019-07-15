using System.ComponentModel.DataAnnotations;

namespace Mep.Data.Entities
{
  public abstract class NameDescription : BaseEntity, INameDescription
  {
    [MaxLength(200)]
    [Required]
    public string Name { get; set; }
    [MaxLength(2000)]
    [Required]
    public string Description { get; set; }
  }
}