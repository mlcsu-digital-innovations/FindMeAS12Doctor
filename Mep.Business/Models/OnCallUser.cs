using System;
namespace Mep.Business.Models
{
  public class OnCallUser : BaseModel
  {
    public DateTimeOffset DateTimeEnd { get; set; }
    public DateTimeOffset DateTimeStart { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }
  }
}