using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Mep.Api.ViewModels
{
  public class GpPractice : BaseViewModel
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