namespace Mep.Data.Entities
{
  public interface IExaminationDoctor
  {
    int? AttendanceConfirmedByUserId { get; set; }
    User AttendanceConfirmedByUser { get; set; }
    User DoctorUser { get; set; }
    int DoctorUserId { get; set; }    
    Examination Examination { get; set; }
    int ExaminationId { get; set; }
    ExaminationDoctorStatus Status { get; set; }
    int StatusId { get; set; }
  }
}