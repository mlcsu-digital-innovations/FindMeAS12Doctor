using System;
using System.Linq.Expressions;

namespace Fmas12d.Business.Models
{
  public class UserAssessmentNotification : BaseModel
  {
    public UserAssessmentNotification() {}
    public UserAssessmentNotification(Data.Entities.UserAssessmentNotification entity) 
      : base(entity)
    {
      if (entity == null) return;
      
      // TODO Assessment
      AssessmentId = entity.AssessmentId;
      NotificationText = new NotificationText(entity.NotificationText);
      NotificationTextId = entity.NotificationTextId;
      SentAt = entity.SentAt;
      User = entity.User == null ? null : new User(entity.User);
      UserId = entity.UserId;
    }

    public virtual Assessment Assessment { get; set; }
    public int AssessmentId { get; set; }
    public virtual NotificationText NotificationText { get; set; }
    public int NotificationTextId { get; set; }

    public DateTimeOffset? SentAt { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }

    public bool IsAmhp { get { return User.IsAmhp; } }
    public bool IsDoctor { get { return User.IsDoctor; } }
    public string UserName { get { return User?.DisplayName; } }

    public static Expression<Func<Data.Entities.UserAssessmentNotification, UserAssessmentNotification>> ProjectFromEntity
    {
      get
      {
        return entity => new UserAssessmentNotification(entity);
      }
    }
    internal Data.Entities.UserAssessmentNotification MapToEntity()
    {
      Data.Entities.UserAssessmentNotification entity = new Data.Entities.UserAssessmentNotification()
      {
        AssessmentId = AssessmentId,
        UserId = UserId,
        NotificationTextId = NotificationTextId
      };

      BaseMapToEntity(entity);
      return entity;
    }
  }
}