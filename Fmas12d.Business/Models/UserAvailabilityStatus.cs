using System.Collections.Generic;
namespace Fmas12d.Business.Models
{
  public class UserAvailabilityStatus : NameDescription, IUserAvailabilityStatus
  {
    public const int AVAILABLE = 1;
    public const int UNAVAILABLE = 2;

    public virtual IList<UserAvailability> UserAvailabilities { get; set; }
  }
}