using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class AssessmentDoctorSeeder : SeederBase<AssessmentDoctor>
  {
    internal AssessmentDoctor Create(
      string doctorUserName,
      int statusId,
      int? attendanceConfirmedByUserId = null
    )
    {
      AssessmentDoctor assessmentDoctor = new AssessmentDoctor
      {
        AttendanceConfirmedByUserId = attendanceConfirmedByUserId,
        DoctorUserId = GetUserByDisplayName(doctorUserName).Id,
        StatusId = statusId 
      };
      PopulateActiveAndModifiedWithSystemUser(assessmentDoctor);

      return assessmentDoctor;
    }
  }
}