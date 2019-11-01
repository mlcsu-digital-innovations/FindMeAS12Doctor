using System;

namespace Fmas12d.Data.Entities
{
  public partial class OnCallUser : BaseEntity, IOnCallUser
  {
    public DateTimeOffset DateTimeEnd { get; set; }
    public DateTimeOffset DateTimeStart { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }
  }
}
