using Mep.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
  public class CcgController : SearchBaseController<Business.Models.Ccg>
  {
    public CcgController(ICcgService service)
      : base(service)
    {
    }
  }
}