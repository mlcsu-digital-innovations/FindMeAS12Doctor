using System;
using System.Linq.Expressions;

namespace Fmas12d.Business.Models
{
  public class UserAvailability : BaseModel, IUserAvailability
  {
    public UserAvailability() { }
    public UserAvailability(Data.Entities.IUserAvailability entity) : base(entity)
    {
      if (entity == null) return;

      ContactDetailId = entity.ContactDetailId;
      End = entity.End;
      Latitude = entity.Latitude;
      Longitude = entity.Longitude;
      Postcode = entity.Postcode;
      Start = entity.Start;
      // TODO Status = ;
      StatusId = entity.UserAvailabilityStatusId;
      // TODO User = new User(entity.User);
      UserId = entity.UserId;
    }

    public int? ContactDetailId { get; set; }
    public DateTimeOffset End { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string Postcode { get; set; }
    public DateTimeOffset Start { get; set; }
    public IUserAvailabilityStatus Status { get; set; }
    public int StatusId { get; set; }
    public IUser User { get; set; }
    public int UserId { get; set; }

    public void MapToEntity(Data.Entities.IUserAvailability entity)
    {
      if (entity == null) return;

      entity.ContactDetailId = ContactDetailId;
      entity.End = End;      
      entity.Latitude = Latitude ?? 0;
      entity.Longitude = Longitude ?? 0;
      entity.Postcode = Postcode;
      entity.Start = Start;
      entity.UserAvailabilityStatusId = StatusId;
      entity.UserId = UserId;
    }

    public static Expression<Func<Data.Entities.IUserAvailability, UserAvailability>> ProjectFromEntity
    {
      get
      {
        return entity => new UserAvailability(entity);
      }
    }
  }
}