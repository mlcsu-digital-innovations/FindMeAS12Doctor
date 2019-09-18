using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public abstract class Patient
  {
    [MaxLength(200)]
    public string AlternativeIdentifier { get; set; }
    public virtual Ccg Ccg { get; set; }
    public int? CcgId { get; set; }
    public virtual GpPractice GpPractice { get; set; }
    public int? GpPracticeId { get; set; }
    public long? NhsNumber { get; set; }
    [MaxLength(10)]
    public string ResidentialPostcode { get; set; }
  }
}