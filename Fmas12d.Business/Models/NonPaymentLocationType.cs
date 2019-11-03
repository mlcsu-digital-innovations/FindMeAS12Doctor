using System.Collections.Generic;
namespace Fmas12d.Business.Models
{
  public class NonPaymentLocationType : NameDescription
  {
    internal const int GP_PRACTICE = 1;
    internal const int HOSPITAL = 2;
    public virtual IList<NonPaymentLocation> NonPaymentLocations { get; set; }
  }
}