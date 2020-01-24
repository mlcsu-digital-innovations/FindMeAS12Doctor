using System;
using System.Linq.Expressions;

namespace Fmas12d.Business.Models
{
  public class UserOnCallConfirmation : UserOnCall
  {
    public UserOnCallConfirmation() {
    }
    
    public UserOnCallConfirmation(Data.Entities.UserAvailability entity) 
      : base()
    {
      if (entity == null) return;

      OnCallConfirmationReceivedAt = entity.OnCallConfirmationReceivedAt;
      OnCallConfirmationSentAt = entity.OnCallConfirmationSentAt;
      OnCallIsConfirmed = entity.OnCallIsConfirmed;
      OnCallRejectedReason = entity.OnCallRejectedReason;
    }

    public override void MapToEntity(Data.Entities.UserAvailability entity)
    {
      
      if (entity == null) return;

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