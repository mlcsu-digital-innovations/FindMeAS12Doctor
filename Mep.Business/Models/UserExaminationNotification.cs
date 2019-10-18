using System;
namespace Mep.Business.Models
{
  public class UserExaminationNotification : BaseModel
  {
    public virtual Examination Examination { get; set; }
    public int ExaminationId { get; set; }
    public bool? HasAccepted { get; set; }
    public virtual NotificationText NotificationText { get; set; }
    public int NotificationTextId { get; set; }
    public DateTimeOffset? RespondedAt { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }

    public bool IsAmhp { get { return User.IsAmhp; } }
    public bool IsDoctor { get { return User.IsDoctor; } }
    public string UserName { get { return User?.DisplayName; } }


  }
}