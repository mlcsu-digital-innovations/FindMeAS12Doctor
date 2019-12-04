using System;

namespace Fmas12d.Business.Models
{
  public interface IUserAvailability : IBaseModel
  {
    DateTimeOffset End { get; set; }
    Location Location { get; set; }
    DateTimeOffset Start { get; set; }
    IUserAvailabilityStatus Status { get; set; }
    int StatusId { get; set; }
    IUser User { get; set; }
    int UserId { get; set; }

    void MapToEntity(Data.Entities.UserAvailability entity);
  }
}