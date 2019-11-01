using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("UnsuccessfulExaminationTypesAudit")]
  public partial class UnsuccessfulExaminationTypeAudit : 
    NameDescriptionAudit, IUnsuccessfulExaminationType
  {
  }
}
