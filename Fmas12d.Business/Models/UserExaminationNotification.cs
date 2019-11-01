using System;

namespace Fmas12d.Business.Models
{
  public class UserExaminationNotification : BaseModel
  {
    public UserExaminationNotification() {}
    public UserExaminationNotification(Data.Entities.UserExaminationNotification entity) 
      : base(entity)
    {
      if (entity == null) return;
      
      // TODO Examination
      ExaminationId = entity.ExaminationId;
      HasAccepted = entity.HasAccepted;
      // TODO NotificationText
      NotificationTextId = entity.NotificationTextId;
      RespondedAt = entity.RespondedAt;
      User = entity.User == null ? null : new User(entity.User);
      UserId = entity.UserId;
    }

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