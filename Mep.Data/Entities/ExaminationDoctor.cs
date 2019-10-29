namespace Mep.Data.Entities
{
  public partial class ExaminationDoctor : BaseEntity, IExaminationDoctor
  {
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
