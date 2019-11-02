namespace Fmas12d.Data.Entities
{
  public interface IExaminationDoctor
  {
    int? AttendanceConfirmedByUserId { get; set; }
    int DoctorUserId { get; set; }    
    int ExaminationId { get; set; }
    int StatusId { get; set; }
  }
}