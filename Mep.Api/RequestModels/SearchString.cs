using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
    public class SearchString
    {
      [Required]
      public string Search { get; set; }
    }
}