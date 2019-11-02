using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("ExaminationDoctorsAudit")]
  public partial class ExaminationDoctorAudit : BaseAudit, IExaminationDoctor
  {
    public int? AttendanceConfirmedByUserId { get; set; }
    public int DoctorUserId { get; set; }    
    public int ExaminationId { get; set; }
    public int StatusId { get; set; }
  }
}
