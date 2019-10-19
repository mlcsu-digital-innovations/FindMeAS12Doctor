using System.Collections.Generic;
namespace Mep.Business.Models
{
  public class NonPaymentLocationType : NameDescription
  {
    public const int GP_PRACTICE = 1;
    public virtual IList<NonPaymentLocation> NonPaymentLocations { get; set; }
  }
}