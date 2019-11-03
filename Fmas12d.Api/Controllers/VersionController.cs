using Microsoft.AspNetCore.Mvc;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
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