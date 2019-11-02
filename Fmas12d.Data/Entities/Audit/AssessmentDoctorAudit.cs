using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("AssessmentDoctorsAudit")]
  public partial class AssessmentDoctorAudit : BaseAudit, IAssessmentDoctor
  {
    public int? AttendanceConfirmedByUserId { get; set; }
    public int DoctorUserId { get; set; }    
    public int AssessmentId { get; set; }
    public int StatusId { get; set; }
  }
}
