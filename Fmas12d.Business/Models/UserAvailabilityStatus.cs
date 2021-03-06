using System.Collections.Generic;
namespace Fmas12d.Business.Models
{
  public class UserAvailabilityStatus : NameDescription, IUserAvailabilityStatus
  {
    public const int AVAILABLE = 1;
    public const int UNAVAILABLE = 2;
    public const int ON_CALL = 3;

    public bool IsOnCall
    {
      get
      {
        return Id == Business.Models.UserAvailabilityStatus.ON_CALL;
      }
    }
    public virtual IList<UserAvailability> UserAvailabilities { get; set; }

    public UserAvailabilityStatus(Data.Entities.UserAvailabilityStatus userAvailabilityStatus)
      : base(userAvailabilityStatus)
    {

    }
  }
}