using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Mep.Api.RequestModels
{
  public class SearchString
  {
    [Required]
    public string Criteria { get; set; }

    public bool IsNumeric { get { return Criteria?.All(char.IsNumber) ?? false; } }
  }
}