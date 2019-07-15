using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("GpPracticesAudit")]
  public partial class GpPracticeAudit : BaseAudit, IGpPractice
  {
    // public virtual CcgAudit Ccg { get; set; }
    public int CcgId { get; set; }
    [MaxLength(10)]
    [Required]
    public string GpPracticeCode { get; set; }
    [MaxLength(200)]
    [Required]
    public string Name { get; set; }
    // public virtual IList<PatientAudit> Patients { get; set; }
    [MaxLength(10)]
    [Required]
    public string Postcode { get; set; }
  }
}
