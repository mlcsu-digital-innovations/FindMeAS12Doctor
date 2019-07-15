﻿using System;

namespace Mep.Data.Entities
{
  public partial class DoctorStatus : BaseEntity, IDoctorStatus
  {
    public DateTimeOffset AvailabilityStart { get; set; }
    public DateTimeOffset AvailabilityEnd { get; set; }
    public DateTimeOffset? ExtendedAvailabilityEnd1 { get; set; }
    public DateTimeOffset? ExtendedAvailabilityStart1 { get; set; }
    public DateTimeOffset? ExtendedAvailabilityEnd2 { get; set; }
    public DateTimeOffset? ExtendedAvailabilityStart2 { get; set; }
    public DateTimeOffset? ExtendedAvailabilityEnd3 { get; set; }
    public DateTimeOffset? ExtendedAvailabilityStart3 { get; set; }
    public int Latitude { get; set; }
    public int Longitude { get; set; }
  }
}
