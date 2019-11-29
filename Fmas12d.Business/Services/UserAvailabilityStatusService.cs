using Fmas12d.Business.Models;

namespace Fmas12d.Business.Services
{
  public class UserAvailabilityStatusService : 
    NameDescriptionBaseService<Data.Entities.UserAvailabilityStatus>,
    IUserAvailabilityStatusService
  {
    public UserAvailabilityStatusService(
      ApplicationContext context,
      IAppClaimsPrincipal appClaimsPrincipal)
      : base(context, appClaimsPrincipal)
    {
    }
  }
}