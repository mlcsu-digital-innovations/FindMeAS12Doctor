using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mep.Api.Controllers
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