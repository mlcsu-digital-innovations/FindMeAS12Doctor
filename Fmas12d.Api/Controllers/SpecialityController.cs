using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]  
  public class SpecialityController : IdNameDescriptionBaseController
  {
    public SpecialityController(ISpecialityService service)
      : base (service)
    {
    }
 }
}