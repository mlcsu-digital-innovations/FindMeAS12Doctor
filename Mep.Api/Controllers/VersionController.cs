using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
  public class VersionController : ControllerBase
  {
    public VersionController()
    { }

    [HttpGet]
    public ActionResult<string> Get()
    {
      string version = GetType().Assembly.GetName().Version.ToString();
      return Ok(version);
    }
  }
}