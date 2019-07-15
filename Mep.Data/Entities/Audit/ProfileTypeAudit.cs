using System.Collections.Generic;

namespace Mep.Data.Entities.Audit
{
  public class ProfileTypeAudit : NameDescription, IProfileType
  {
    public virtual IList<IUser> Users { get; set; }
  }
}