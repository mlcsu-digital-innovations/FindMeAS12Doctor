using System.ComponentModel.DataAnnotations;

namespace Mep.Data.Entities.Audit
{
  public abstract class NameDescriptionAudit : BaseAudit, INameDescription
  {
    [MaxLength(200)]
    [Required]
    public string Name { get; set; }
    [MaxLength(2000)]
    [Required]
    public string Description { get; set; }
  }
}