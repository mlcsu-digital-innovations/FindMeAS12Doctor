using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("OnCallUsersAudit")]
  public partial class OnCallUserAudit : BaseAudit, IOnCallUser
  {
    public DateTimeOffset DateTimeEnd { get; set; }
    public DateTimeOffset DateTimeStart { get; set; }
    public int UserId { get; set; }
  }
}
