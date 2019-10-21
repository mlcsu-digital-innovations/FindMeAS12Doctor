using System.ComponentModel.DataAnnotations;

namespace Mep.Api.ViewModels
{
  public class GpPractice : BaseViewModel
  {
    public int CcgId { get; set; }
    [MaxLength(10)]
    [Required]
    public string GpPracticeCode { get; set; }
    [MaxLength(200)]
    [Required]
    public string Name { get; set; }
    [MaxLength(10)]
    [Required]
    public string Postcode { get; set; }
  }
}