using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class DoctorStatusesSeeder : SeederBase
  {
    internal void SeedData()
    {
      DoctorStatus doctorStatus;

      if ((doctorStatus = _context
       .DoctorStatuses
        .SingleOrDefault(g => g.Id == DoctorStatus.AVAILABLE)) 
         == null)
      {
        doctorStatus = new DoctorStatus();
        _context.Add(doctorStatus);
      }

      doctorStatus.AvailabilityEnd = _now.AddHours(8);
      doctorStatus.AvailabilityStart = _now;
      doctorStatus.Latitude = LATITUDE;
      doctorStatus.Longitude = LONGITUDE;
      doctorStatus.UserId = GetSystemAdminUser().Id;

      PopulateActiveAndModifiedWithSystemUser(doctorStatus);
    }
  }
}