using System.ComponentModel.DataAnnotations;

namespace Mep.Data.Entities.Audit
{
  public partial class PatientAudit : BaseAudit, IPatient
  {
    [MaxLength(200)]
    public string AlternativeIdentifier { get; set; }
    public virtual ICcg Ccg { get; set; }
    public int? CcgId { get; set; }
    public virtual IGpPractice GpPractice { get; set; }
    public int? GpPracticeId { get; set; }
    public int? NhsNumber { get; set; }
    [MaxLength(10)]
    [Required]
    public string ResidentialPostcode { get; set; }
  }
}
