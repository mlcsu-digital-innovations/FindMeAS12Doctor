using System;

namespace Fmas12d.Business.Models
{
  public interface IUserAvailability : IBaseModel
  {
    int? ContactDetailId { get; set; }
    DateTimeOffset End { get; set; }
    decimal? Latitude { get; set; }
    decimal? Longitude { get; set; }
    string Postcode { get; set; }
    DateTimeOffset Start { get; set; }
    IUserAvailabilityStatus Status { get; set; }
    int StatusId { get; set; }
    IUser User { get; set; }
    int UserId { get; set; }

    void MapToEntity(Data.Entities.UserAvailability entity);
  }
}