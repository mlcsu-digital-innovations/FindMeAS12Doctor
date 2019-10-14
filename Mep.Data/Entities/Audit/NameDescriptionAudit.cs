using System.ComponentModel.DataAnnotations;

namespace Mep.Data.Entities
{
  public abstract class NameDescriptionAudit : BaseAudit, INameDescription
  {
    [MaxLength(2000)]
    [Required]
    public string Description { get; set; }    
    [MaxLength(200)]
    [Required]
    public string Name { get; set; }
  }
}