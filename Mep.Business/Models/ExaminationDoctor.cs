namespace Mep.Business.Models
{
  public class ExaminationDoctor : BaseModel
  {
    public int? AttendanceConfirmedByUserId { get; set; }
    public virtual User AttendanceConfirmedByUser { get; set; }
    public virtual User DoctorUser { get; set; }
    public int DoctorUserId { get; set; }
    public virtual Examination Examination { get; set; }
    public int ExaminationId { get; set; }
    public bool IsAttendanceConfirmed { get; set; }
    public virtual ExaminationDoctorStatus Status { get; set; }
    public int StatusId { get; set; }
  }
}