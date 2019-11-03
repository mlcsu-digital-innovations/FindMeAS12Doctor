namespace Fmas12d.Data.Entities
{
  public interface IAssessmentDoctor
  {
    int? AttendanceConfirmedByUserId { get; set; }
    int DoctorUserId { get; set; }    
    int AssessmentId { get; set; }
    int StatusId { get; set; }
  }
}