using System;

namespace Fmas12d.Data.Entities
{
  public interface IDoctorStatus
  {
    DateTimeOffset AvailabilityEnd { get; set; }
    DateTimeOffset AvailabilityStart { get; set; }
    DateTimeOffset? ExtendedAvailabilityEnd1 { get; set; }
    DateTimeOffset? ExtendedAvailabilityEnd2 { get; set; }
    DateTimeOffset? ExtendedAvailabilityEnd3 { get; set; }
    DateTimeOffset? ExtendedAvailabilityStart1 { get; set; }
    DateTimeOffset? ExtendedAvailabilityStart2 { get; set; }
    DateTimeOffset? ExtendedAvailabilityStart3 { get; set; }
    decimal Latitude { get; set; }
    decimal Longitude { get; set; }
    decimal? ExtendedAvailabilityLatitude1 { get; set; }
    decimal? ExtendedAvailabilityLongitude1 { get; set; }
    decimal? ExtendedAvailabilityLatitude2 { get; set; }
    decimal? ExtendedAvailabilityLongitude2 { get; set; }
    decimal? ExtendedAvailabilityLatitude3 { get; set; }
    decimal? ExtendedAvailabilityLongitude3 { get; set; }
    int UserId { get; set; }
  }
}