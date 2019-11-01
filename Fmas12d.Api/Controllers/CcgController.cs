using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CcgController : SearchBaseController<Business.Models.Ccg>
  {
    public CcgController(ICcgService service)
      : base(service)
    {
    }
  }
}