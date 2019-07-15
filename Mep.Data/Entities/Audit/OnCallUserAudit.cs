using System;

namespace Mep.Data.Entities
{
  public partial class OnCallUserAudit : BaseAudit, IOnCallUser
  {
    public DateTimeOffset DateTimeEnd { get; set; }
    public DateTimeOffset DateTimeStart { get; set; }
    // public virtual UserAudit User { get; set; }
    public int UserId { get; set; }
  }
}
