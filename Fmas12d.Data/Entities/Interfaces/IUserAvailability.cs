using System;

namespace Fmas12d.Data.Entities
{
  public interface IUserAvailability : IBaseEntity
  {
    int? ContactDetailId { get; set; }
    DateTimeOffset End { get; set; }
    DateTimeOffset Start { get; set; }
    decimal Latitude { get; set; }
    decimal Longitude { get; set; }
    string Postcode { get; set; }
    int UserAvailabilityStatusId { get; set; }
    int UserId { get; set; }
  }
}