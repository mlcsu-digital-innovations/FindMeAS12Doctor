using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("AssessmentDoctorStatusesAudit")]
  public partial class AssessmentDoctorStatusAudit : 
    NameDescriptionAudit, IAssessmentDoctorStatus
  {
  }
}
