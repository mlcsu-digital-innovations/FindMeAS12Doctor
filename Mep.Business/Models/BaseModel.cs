using System;
using System.ComponentModel.DataAnnotations;
using Mep.Data.Entities;

namespace Mep.Business.Models
{
  public abstract class BaseModel : IBaseModel
  {
    protected BaseModel() {}

    protected BaseModel(BaseEntity model)
    {
      Id = model.Id;
      ModifiedAt = model.ModifiedAt;
      // TODO ModifiedByUser = model.ModifiedByUser;
      ModifiedByUserId = model.ModifiedByUserId;      
      IsActive = model.IsActive;
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
  }
}