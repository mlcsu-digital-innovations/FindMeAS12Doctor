using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class DoctorStatusesSeeder : SeederBase<DoctorStatus>
  {
    internal void SeedData()
    {
      DoctorStatus doctorStatus;

      if ((doctorStatus = _context.DoctorStatuses
        .SingleOrDefault(g => g.UserId == GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_FEMALE).Id))== null)
      {
        doctorStatus = new DoctorStatus();
        _context.Add(doctorStatus);
      }

      doctorStatus.AvailabilityEnd = _now.AddHours(8);
      doctorStatus.AvailabilityStart = _now;
      doctorStatus.Latitude = CONTACT_DETAIL_LATITUDE_DOCTOR_FEMALE;
      doctorStatus.Longitude = CONTACT_DETAIL_LONGITUDE_DOCTOR_FEMALE;
      doctorStatus.UserId = GetSystemAdminUser().Id;

      PopulateActiveAndModifiedWithSystemUser(doctorStatus);
    }
  }
}