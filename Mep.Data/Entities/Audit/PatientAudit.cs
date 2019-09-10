using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("PatientsAudit")]
  public partial class PatientAudit : BaseAudit, IPatient
  {
    [MaxLength(200)]
    public string AlternativeIdentifier { get; set; }
    public int? CcgId { get; set; }
    public int? GpPracticeId { get; set; }
    public long? NhsNumber { get; set; }
    [MaxLength(10)]
    public string ResidentialPostcode { get; set; }
  }
}
