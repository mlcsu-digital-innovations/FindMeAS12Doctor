using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("ExaminationDoctorStatusesAudit")]
  public partial class ExaminationDoctorStatusAudit : 
    NameDescriptionAudit, IExaminationDoctorStatus
  {
  }
}
