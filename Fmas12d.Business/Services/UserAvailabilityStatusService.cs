namespace Fmas12d.Business.Services
{
  public class UserAvailabilityStatusService : 
    NameDescriptionBaseService<Data.Entities.UserAvailabilityStatus>,
    IUserAvailabilityStatusService
  {
    public UserAvailabilityStatusService(ApplicationContext context)
      : base(context)
    {
    }
  }
}