using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
  
  public class SpecialityController : IdNameDescriptionBaseController
  {
    public SpecialityController(
      ISpecialityService service,
      IUserClaimsService userClaimsService)
      : base(userClaimsService, service)
    {
    }
 }
}