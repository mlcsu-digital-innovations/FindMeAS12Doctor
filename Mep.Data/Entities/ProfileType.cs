using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public class ProfileType : NameDescription, IProfileType
    {
    public virtual IList<User> Users { get; set; }
  }
}