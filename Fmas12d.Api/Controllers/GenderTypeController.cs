using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fmas12d.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
  public class GenderTypeController : IdNameDescriptionBaseController
  {
    public GenderTypeController(
      IUserClaimsService userClaimsService,
      IGenderTypeService service)
      : base(userClaimsService, service)
    {
    }
  }
}