using System;
using System.ComponentModel.DataAnnotations;

namespace Mep.Data.Entities
{
  public abstract class BaseEntity : IBaseEntity
  {
    [Key]
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
    public int? ModifiedByUserId { get; set; }
    public virtual User ModifiedByUser { get; set; }
  }
}