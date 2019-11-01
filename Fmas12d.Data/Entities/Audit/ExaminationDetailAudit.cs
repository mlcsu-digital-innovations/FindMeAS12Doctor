using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("ExaminationDetailsAudit")]
  public class ExaminationDetailAudit : BaseAudit, IExaminationDetail
  {
    public int ExaminationId { get; set; }
    public int ExaminationDetailTypeId { get; set; }
  }
}