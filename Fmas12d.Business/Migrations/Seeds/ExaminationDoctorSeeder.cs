using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class ExaminationDoctorSeeder : SeederBase<ExaminationDoctor>
  {
    internal ExaminationDoctor Create(
      string doctorUserName,
      int statusId,
      int? attendanceConfirmedByUserId = null
    )
    {
      ExaminationDoctor examinationDoctor = new ExaminationDoctor
      {
        AttendanceConfirmedByUserId = attendanceConfirmedByUserId,
        DoctorUserId = GetUserByDisplayName(doctorUserName).Id,
        StatusId = statusId 
      };
      PopulateActiveAndModifiedWithSystemUser(examinationDoctor);

      return examinationDoctor;
    }
  }
}