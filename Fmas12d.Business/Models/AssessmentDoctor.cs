using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Models
{
  public class AssessmentDoctor : BaseModel
  {
    public AssessmentDoctor() {}
    public AssessmentDoctor(Data.Entities.AssessmentDoctor doctor) : base (doctor)
    {
      AttendanceConfirmedByUserId = doctor.Id;
      // TODO AttendanceConfirmedByUser =
      DoctorUser = doctor.DoctorUser == null ? null : new User(doctor.DoctorUser);
      DoctorUserId = doctor.DoctorUserId;
      // TODO Assessment =
      AssessmentId = doctor.AssessmentId;
      // TODO Status =
      StatusId = doctor.StatusId;
    }

    public int? AttendanceConfirmedByUserId { get; set; }
    public virtual User AttendanceConfirmedByUser { get; set; }
    public virtual User DoctorUser { get; set; }
    public int DoctorUserId { get; set; }
    public virtual Assessment Assessment { get; set; }
    public int AssessmentId { get; set; }
    public virtual AssessmentDoctorStatus Status { get; set; }
    public int StatusId { get; set; }
  }
}