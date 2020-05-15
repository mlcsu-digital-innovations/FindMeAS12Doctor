using System.Collections.Generic;
namespace Fmas12d.Business.Models
{
  public class Organisation : NameDescription
  {
    public Organisation() {}
    public Organisation(Data.Entities.Organisation entity) : base(entity)
    {

    }
    public virtual IList<User> Users { get; set; }
  }
}