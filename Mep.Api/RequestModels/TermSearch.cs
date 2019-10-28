using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public class TermSearch
  {
    [Required]
    public string Term { get; set; }

  }
}