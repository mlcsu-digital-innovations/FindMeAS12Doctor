using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Fmas12d.Business.Models
{
  public class NameDescription : BaseModel, INameDescription
  {
    public NameDescription() {}
    public NameDescription(Data.Entities.NameDescription entity) : base(entity)
    {
      if (entity == null) return;
      
      Description = entity.Description;
      Name = entity.Name;      
    }

    [StringLength(2000)]
    [Required]
    public string Description { get; set; }

    [StringLength(200)]
    [Required]
    public string Name { get; set; }

    public static Expression<Func<Data.Entities.NameDescription, NameDescription>> ProjectFromEntity
    {
      get
      {
        return entity => new NameDescription(entity);
      }
    }
  }
}