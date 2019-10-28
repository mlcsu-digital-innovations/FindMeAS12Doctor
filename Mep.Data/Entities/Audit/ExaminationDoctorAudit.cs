using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("ExaminationDoctorsAudit")]
  public partial class ExaminationDoctorAudit : BaseAudit, IExaminationDoctor
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
