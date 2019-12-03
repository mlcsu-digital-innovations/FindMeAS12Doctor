using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Models;

namespace Fmas12d.Business.Services
{
  public class ReferralStatusService
    : ServiceBaseNoAutoMapper<Entities.ReferralStatus>,
      IReferralStatusService
  {
    public ReferralStatusService(
      ApplicationContext context,
      IUserClaimsService userClaimsService)
      : base(context, userClaimsService)
    {
    }
  }
}