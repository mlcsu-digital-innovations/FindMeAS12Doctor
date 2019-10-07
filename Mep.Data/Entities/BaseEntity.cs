using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  public abstract class BaseEntity : IBaseEntity
  {
    [Key]
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
    [ForeignKey("ModifiedByUser")]
    public int? ModifiedByUserId { get; set; }
    public virtual User ModifiedByUser { get; set; }
  }
}