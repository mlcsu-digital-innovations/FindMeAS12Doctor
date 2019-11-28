using System;
using Fmas12d.Business.Helpers;
using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class AssessmentDoctorSeeder : SeederBase<AssessmentDoctor>
  {
    internal AssessmentDoctor Create(
      string doctorUserName,
      decimal latitude,
      decimal longitude,
      int statusId,
      int? attendanceConfirmedByUserId = null,
      int? contactDetailId = null,
      decimal? distance = null,
      bool? hasExcepted = null,
      string postcode = null,
      DateTimeOffset? respondedAt = null
    )
    {
      AssessmentDoctor assessmentDoctor = new AssessmentDoctor
      {
        AttendanceConfirmedByUserId = attendanceConfirmedByUserId,
        ContactDetailId = contactDetailId,
        DoctorUserId = GetUserByDisplayName(doctorUserName).Id,
        Distance = distance,
        HasAccepted = hasExcepted,
        Latitude = latitude,
        Longitude = longitude,
        Postcode = postcode,
        RespondedAt = respondedAt,        
        StatusId = statusId 
      };
      PopulateActiveAndModifiedWithSystemUser(assessmentDoctor);

      return assessmentDoctor;
    }
  }
}