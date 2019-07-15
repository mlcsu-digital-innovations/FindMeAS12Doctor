using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("OnCallUsersAudit")]
  public partial class OnCallUserAudit : BaseAudit, IOnCallUser
  {
    public DateTimeOffset DateTimeEnd { get; set; }
    public DateTimeOffset DateTimeStart { get; set; }
    // public virtual UserAudit User { get; set; }
    public int UserId { get; set; }
  }
}
