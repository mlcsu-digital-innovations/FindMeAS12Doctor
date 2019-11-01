using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class TermSearch
  {
    [Required]
    public string Term { get; set; }

  }
}