using System;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Business.Models
{
  public abstract class BaseModel : IBaseModel
  {
    protected BaseModel() {}

    protected BaseModel(Data.Entities.IBaseEntity entity)
    {
      if (entity == null) return;
      
      Id = entity.Id;
      ModifiedAt = entity.ModifiedAt;
      //ModifiedByUser = entity.ModifiedByUser == null ? null : new User(entity.ModifiedByUser);
      ModifiedByUserId = entity.ModifiedByUserId;      
      IsActive = entity.IsActive;
    }

    [Required]
    public int Id { get; set; }
    [Required]
    public DateTimeOffset ModifiedAt { get; set; }
    [Required]
    public int ModifiedByUserId { get; set; }
    public virtual User ModifiedByUser {get; set;}
    [Required]
    public bool IsActive { get; set; }
    
    public virtual void BaseMapToEntity(Data.Entities.BaseEntity entity)
    {
      if (entity == null) return;

      entity.Id = Id;
      entity.ModifiedAt = ModifiedAt;
      //TODO entity.ModifiedByUser = ModifiedByUser
      entity.ModifiedByUserId = ModifiedByUserId;
      entity.IsActive = IsActive;
    }
  }
}