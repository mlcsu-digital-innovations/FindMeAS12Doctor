using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("AssessmentDetailsAudit")]
  public class AssessmentDetailAudit : BaseAudit, IAssessmentDetail
  {
    public int AssessmentId { get; set; }
    public int AssessmentDetailTypeId { get; set; }
  }
}