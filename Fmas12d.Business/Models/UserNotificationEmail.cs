using System;
using System.Linq.Expressions;

namespace Fmas12d.Business.Models
{
  public class UserNotificationEmail : BaseModel
  {
    public UserNotificationEmail() {}
    public UserNotificationEmail(Data.Entities.UserNotificationEmail entity) 
      : base(entity)
    {
      if (entity == null) return;

      NotificationEmail = new NotificationEmail(entity.NotificationEmail);
      NotificationEmailId = entity.NotificationEmailId;
      DateAdded = entity.DateAdded;
      DateSent = entity.DateSent;
      User = entity.User == null ? null : new User(entity.User);
      UserId = entity.UserId;
    }

    public int AssessmentId { get; set; }
    public virtual NotificationEmail NotificationEmail { get; set; }
    public int NotificationEmailId { get; set; }
    public DateTimeOffset DateAdded { get; set; }
    public DateTimeOffset? DateSent { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }

    public static Expression<Func<Data.Entities.UserNotificationEmail, UserNotificationEmail>> ProjectFromEntity
    {
      get
      {
        return entity => new UserNotificationEmail(entity);
      }
    }
    internal Data.Entities.UserNotificationEmail MapToEntity()
    {
      Data.Entities.UserNotificationEmail entity = new Data.Entities.UserNotificationEmail()
      {
        UserId = UserId,
        NotificationEmailId = NotificationEmailId,
        DateAdded = DateAdded
      };

      BaseMapToEntity(entity);
      return entity;
    }
  }
}