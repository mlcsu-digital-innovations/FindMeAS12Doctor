using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("ExaminationDetailTypesAudit")]
  public class ExaminationDetailTypeAudit :
    NameDescriptionAudit, IExaminationDetailType
  {
  }
}