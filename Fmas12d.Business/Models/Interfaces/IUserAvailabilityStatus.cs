using System.Collections.Generic;
namespace Fmas12d.Business.Models
{
  public interface IUserAvailabilityStatus
  {
    IList<UserAvailability> UserAvailabilities { get; set; }

    string Name { get; set; }
  }
}