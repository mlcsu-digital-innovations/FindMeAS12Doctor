using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Models
{
  public class ExaminationDoctor : BaseModel
  {
    public ExaminationDoctor() {}
    public ExaminationDoctor(Data.Entities.ExaminationDoctor doctor) : base (doctor)
    {
      AttendanceConfirmedByUserId = doctor.Id;
      // TODO AttendanceConfirmedByUser =
      DoctorUser = doctor.DoctorUser == null ? null : new User(doctor.DoctorUser);
      DoctorUserId = doctor.DoctorUserId;
      // TODO Examination =
      ExaminationId = doctor.ExaminationId;
      // TODO Status =
      StatusId = doctor.StatusId;
    }

    public int? AttendanceConfirmedByUserId { get; set; }
    public virtual User AttendanceConfirmedByUser { get; set; }
    public virtual User DoctorUser { get; set; }
    public int DoctorUserId { get; set; }
    public virtual Examination Examination { get; set; }
    public int ExaminationId { get; set; }
    public virtual ExaminationDoctorStatus Status { get; set; }
    public int StatusId { get; set; }
  }
}