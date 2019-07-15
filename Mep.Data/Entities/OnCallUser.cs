using System;

namespace Mep.Data.Entities
{
  public partial class OnCallUser : BaseEntity, IOnCallUser
  {
    public DateTimeOffset DateTimeEnd { get; set; }
    public DateTimeOffset DateTimeStart { get; set; }
    public virtual IUser User { get; set; }
    public int UserId { get; set; }
  }
}
