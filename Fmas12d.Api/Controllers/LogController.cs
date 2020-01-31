using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fmas12d.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  [Authorize(Policy = "User")]
  public class LogController : ModelControllerBase
  {
    public LogController(
      IRequestResponseLogService service,
      IUserClaimsService userClaimsService)
      : base(userClaimsService, service)
    {
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Business.Models.IRequestResponseLog>>> GetRequestResponse(
      [FromQuery] DateTimeOffset? start,
      [FromQuery] DateTimeOffset? end
    )
    {
      end = end.HasValue ? end : new DateTimeOffset(new DateTime(2050, 1, 1));
      start = start.HasValue ? start : new DateTimeOffset(new DateTime(2020, 1, 1));
      return Ok(await Service.Get(start.Value, end.Value));
    }

    private IRequestResponseLogService Service
    {
      get { return _service as IRequestResponseLogService; }
    }
  }
}