using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("ExaminationDoctorStatusesAudit")]
  public partial class ExaminationDoctorStatusAudit : 
    NameDescriptionAudit, IExaminationDoctorStatus
  {
  }
}
