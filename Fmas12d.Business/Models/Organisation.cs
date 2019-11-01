using System.Collections.Generic;
namespace Fmas12d.Business.Models
{
  public class Organisation : NameDescription
  {
    public virtual IList<User> Users { get; set; }
  }
}