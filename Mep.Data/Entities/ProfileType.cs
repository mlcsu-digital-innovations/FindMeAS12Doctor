using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public class ProfileType : NameDescription, IProfileType
    {
    public virtual IList<IUser> Users { get; set; }
  }
}