using Fmas12d.Business.Models;

namespace Fmas12d.Business.Services
{
  public class GenderTypeService : 
    NameDescriptionBaseService<Data.Entities.GenderType>,
    IGenderTypeService
  {
    public GenderTypeService(
      ApplicationContext context,
      IAppClaimsPrincipal appClaimsPrincipal)
      : base(context, appClaimsPrincipal)
    {
    }
  }
}