using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mep.Api.Controllers
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