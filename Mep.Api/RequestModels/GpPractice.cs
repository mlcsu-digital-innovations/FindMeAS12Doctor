using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public abstract class GpPractice
  {
    // public virtual Ccg Ccg { get; set; }
    public int CcgId { get; set; }
    [MaxLength(10)]
    [Required]
    public string GpPracticeCode { get; set; }
    [MaxLength(200)]
    [Required]
    public string Name { get; set; }
    // public virtual IList<Patient> Patients { get; set; }
    [MaxLength(10)]
    [Required]
    public string Postcode { get; set; }
  }
}