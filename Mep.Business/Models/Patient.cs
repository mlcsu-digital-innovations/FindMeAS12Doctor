using System.ComponentModel.DataAnnotations;
namespace Mep.Business.Models
{
  public class Patient : BaseModel
  {
    [MaxLength(200)]
    public string AlternativeIdentifier { get; set; }
    // public virtual Ccg Ccg { get; set; }
    public int? CcgId { get; set; }
    // public virtual GpPractice GpPractice { get; set; }
    public int? GpPracticeId { get; set; }
    public int? NhsNumber { get; set; }
    [MaxLength(10)]
    [Required]
    public string ResidentialPostcode { get; set; }
  }
}