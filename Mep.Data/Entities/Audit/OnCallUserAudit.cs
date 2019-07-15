using System;

namespace Mep.Data.Entities.Audit
{
  public partial class OnCallUserAudit : BaseAudit, IOnCallUser
  {
    public DateTimeOffset DateTimeEnd { get; set; }
    public DateTimeOffset DateTimeStart { get; set; }
    public virtual IUser User { get; set; }
    public int UserId { get; set; }
  }
}
