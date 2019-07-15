using System.Collections.Generic;

namespace Mep.Data.Entities.Audit
{
  public partial class Section12ApprovalStatusAudit : NameDescription, ISection12ApprovalStatus
  {
    public virtual IList<IUser> Users { get; set; }
  }
}
