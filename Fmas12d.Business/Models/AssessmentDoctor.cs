namespace Fmas12d.Business.Models
{
  public class AssessmentDoctor : BaseModel
  {
    public AssessmentDoctor() { }
    public AssessmentDoctor(Data.Entities.AssessmentDoctor doctor) : base(doctor)
    {
      AttendanceConfirmedByUserId = doctor.Id;
      // TODO AttendanceConfirmedByUser =
      Distance = null;
      DoctorUser = doctor.DoctorUser == null ? null : new User(doctor.DoctorUser);
      DoctorUserId = doctor.DoctorUserId;
      // TODO Assessment =
      AssessmentId = doctor.AssessmentId;
      // TODO Status =
      StatusId = doctor.StatusId;
    }

    public virtual Assessment Assessment { get; set; }
    public int AssessmentId { get; set; }
    public int? AttendanceConfirmedByUserId { get; set; }
    public virtual User AttendanceConfirmedByUser { get; set; }
    public decimal? Distance { get; set; }
    public virtual User DoctorUser { get; set; }
    public int DoctorUserId { get; set; }
    public bool IsAvailable { get; set; }
    public bool HasAccepted { get; set; }
    public virtual AssessmentDoctorStatus Status { get; set; }
    public int StatusId { get; set; }

    public bool IsAllocated { get { return StatusId == AssessmentDoctorStatus.ALLOCATED; } }    
    public bool IsSelected { get { return StatusId == AssessmentDoctorStatus.SELECTED; } }

  }
}