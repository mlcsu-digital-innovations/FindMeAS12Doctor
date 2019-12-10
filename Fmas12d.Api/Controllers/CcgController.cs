using Fmas12d.Business.Models;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
  public class CcgController : SearchBaseController<Ccg>
  {
    public CcgController(
      IUserClaimsService userClaimsService,
      ICcgService service)
      : base(userClaimsService, service)
    {
    }
  }
}