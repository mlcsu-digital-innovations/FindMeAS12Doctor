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
        .SingleOrDefault(g => g.UserId == 1))== null)
      {
        doctorStatus = new DoctorStatus();
        _context.Add(doctorStatus);
      }

      doctorStatus.AvailabilityEnd = _now.AddHours(8);
      doctorStatus.AvailabilityStart = _now;
      doctorStatus.Latitude = CONTACT_DETAIL_DOCTOR_FEMALE_LATITUDE;
      doctorStatus.Longitude = CONTACT_DETAIL_DOCTOR_FEMALE_LONGITUDE;
      doctorStatus.UserId = GetSystemAdminUser().Id;

      PopulateActiveAndModifiedWithSystemUser(doctorStatus);
    }
  }
}