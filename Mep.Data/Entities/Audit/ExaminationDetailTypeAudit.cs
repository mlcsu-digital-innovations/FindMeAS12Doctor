using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("ExaminationDetailTypesAudit")]
  public class ExaminationDetailTypeAudit :
    NameDescriptionAudit, IExaminationDetailType
  {
  }
}