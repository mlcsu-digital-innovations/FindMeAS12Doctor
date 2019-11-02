using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("UnsuccessfulExaminationTypesAudit")]
  public partial class UnsuccessfulExaminationTypeAudit : 
    NameDescriptionAudit, IUnsuccessfulExaminationType
  {
  }
}
