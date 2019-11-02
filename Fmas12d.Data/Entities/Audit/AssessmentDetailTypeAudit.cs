using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("AssessmentDetailTypesAudit")]
  public class AssessmentDetailTypeAudit :
    NameDescriptionAudit, IAssessmentDetailType
  {
  }
}