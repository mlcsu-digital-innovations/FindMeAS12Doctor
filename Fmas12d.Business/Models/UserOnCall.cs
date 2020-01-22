using System;
using System.Linq.Expressions;

namespace Fmas12d.Business.Models
{
  public class UserOnCall : UserAvailability, IUserOnCall
  {
    public UserOnCall() {
      Location = new Location();
    }
    
    public UserOnCall(Data.Entities.UserAvailability entity) 
      : base(entity)
    {
      if (entity == null) return;

      OnCallConfirmationReceivedAt = entity.OnCallConfirmationReceivedAt;
      OnCallConfirmationSentAt = entity.OnCallConfirmationSentAt;
      OnCallIsConfirmed = entity.OnCallIsConfirmed;
      OnCallRejectedReason = entity.OnCallRejectedReason;
    }

    public DateTimeOffset? OnCallConfirmationReceivedAt { get; set; }
    public DateTimeOffset? OnCallConfirmationSentAt { get; set; }
    public bool? OnCallIsConfirmed { get; set; }
    public string OnCallRejectedReason { get; set; }        

    public override void MapToEntity(Data.Entities.UserAvailability entity)
    {
      
      if (entity == null) return;

      base.MapToEntity(entity);
      entity.OnCallConfirmationReceivedAt = OnCallConfirmationReceivedAt;
      entity.OnCallConfirmationSentAt = OnCallConfirmationSentAt;
      entity.OnCallIsConfirmed = OnCallIsConfirmed;
      entity.OnCallRejectedReason = OnCallRejectedReason;      
    }

    public static new Expression<Func<Data.Entities.UserAvailability, UserOnCall>> ProjectFromEntity
    {
      get
      {
        return entity => new UserOnCall(entity);
      }
    }
  }
}