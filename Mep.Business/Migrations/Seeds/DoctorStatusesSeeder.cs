using Mep.Data.Entities;
using System.Linq;
using System;

namespace Mep.Business.Migrations.Seeds
{
  internal class DoctorStatusesSeeder : SeederBase
  {

    internal DoctorStatusesSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      // DoctorStatus doctorStatus;

      // if ((doctorStatus = _context
      //  .DoctorStatuses
      //   .SingleOrDefault(g => g.Name == REFERRAL_STATUS_NAME_NEW_REFERRAL)) 
      //    == null)
      // {
      //   doctorStatus = new DoctorStatus();
      //   _context.Add(doctorStatus);
      // }
      // doctorStatus.IsActive = true;
      // doctorStatus.ModifiedAt = _now;
      // doctorStatus.ModifiedByUser = GetSystemAdminUser();
      // doctorStatus.AvailabilityStart = _now;
      // doctorStatus.AvailabilityEnd = _now;
      // doctorStatus.ExtendedAvailabilityEnd1 = _now;
      // doctorStatus.ExtendedAvailabilityStart1 = _now;
      // doctorStatus.Latitude = LATITUDE;
      // doctorStatus.Longitude = LONGITUDE;
      // doctorStatus.ExtendedAvailabilityLatitude1 = LATITUDE;
      // doctorStatus.ExtendedAvailabilityLongitude1 = LONGITUDE;
    }
  }
}