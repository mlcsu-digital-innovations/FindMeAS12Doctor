using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("UnsuccessfulAssessmentTypesAudit")]
  public partial class UnsuccessfulAssessmentTypeAudit : 
    NameDescriptionAudit, IUnsuccessfulAssessmentType
  {
  }
}
