using System;
using System.Linq.Expressions;

namespace Fmas12d.Business.Models
{
  public class UserAvailability : BaseModel, IUserAvailability
  {
    public UserAvailability() { }
    public UserAvailability(Data.Entities.UserAvailability entity) : base(entity)
    {
      if (entity == null) return;

      End = entity.End;
      Location = new Location(entity);
      Start = entity.Start;
      // TODO Status = ;
      StatusId = entity.UserAvailabilityStatusId;
      User = new User(entity.User);
      UserId = entity.UserId;
    }

    public DateTimeOffset End { get; set; }
    public Location Location { get; set; }
    public DateTimeOffset Start { get; set; }
    public IUserAvailabilityStatus Status { get; set; }
    public int StatusId { get; set; }
    public IUser User { get; set; }
    public int UserId { get; set; }

    public void MapToEntity(Data.Entities.UserAvailability entity)
    {
      if (entity == null) return;

      entity.ContactDetailId = Location.ContactDetailId;
      entity.End = End;      
      entity.Latitude = Location.Latitude;
      entity.Longitude = Location.Longitude;
      entity.Postcode = Location.Postcode;
      entity.Start = Start;
      entity.UserAvailabilityStatusId = StatusId;
      entity.UserId = UserId;
    }

    public static Expression<Func<Data.Entities.UserAvailability, UserAvailability>> ProjectFromEntity
    {
      get
      {
        return entity => new UserAvailability(entity);
      }
    }
  }
}