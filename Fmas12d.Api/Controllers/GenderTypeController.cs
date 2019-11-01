using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GenderTypeController : IdNameDescriptionBaseController
  {
    public GenderTypeController(IGenderTypeService service)
      : base(service)
    {
    }
  }
}