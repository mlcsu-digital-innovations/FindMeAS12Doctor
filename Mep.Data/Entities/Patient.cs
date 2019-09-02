using System.ComponentModel.DataAnnotations;
using System;

namespace Mep.Data.Entities
{
  public partial class Patient : BaseEntity, IPatient
  {
    [MaxLength(200)]
    public string AlternativeIdentifier { get; set; }
    public virtual Ccg Ccg { get; set; }
    public int? CcgId { get; set; }
    public virtual GpPractice GpPractice { get; set; }
    public int? GpPracticeId { get; set; }
    public Int64? NhsNumber { get; set; }
    [MaxLength(10)]
    public string ResidentialPostcode { get; set; }
  }
}
