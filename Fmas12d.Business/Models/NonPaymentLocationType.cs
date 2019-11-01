using System.Collections.Generic;
namespace Mep.Business.Models
{
  public class NonPaymentLocationType : NameDescription
  {
    internal const int GP_PRACTICE = 1;
    internal const int HOSPITAL = 2;
    public virtual IList<NonPaymentLocation> NonPaymentLocations { get; set; }
  }
}