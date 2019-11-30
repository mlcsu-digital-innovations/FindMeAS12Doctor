using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fmas12d.Api.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
  public class UserController : ModelControllerNoAutoMapper
  {
    public UserController(
      IUserClaimsService userClaimsService,
      IUserService service)
      : base(userClaimsService, service)
    {
    }

    [Route("search")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ViewModels.IdResultText>>> GetSearch(
      [FromQuery] RequestModels.UserSearch search)
    {
      IEnumerable<Business.Models.User> models = null;

      if (search.IsByAmhpName)
      {
        models = await Service.GetAllByAmhpName(search.AmhpName, activeOnly: false);
      }
      else if (search.IsByDoctorName)
      {
        models = await Service.GetAllByDoctorName(search.DoctorName, activeOnly: false);
      }
      else if (search.IsByGmcNumber)
      {
        models = await Service.GetAllByGmcNumber(search.GmcNumber, activeOnly: false);
      }

      if (models.Any())
      {
        IEnumerable<ViewModels.IdResultText> viewModels = 
          models.Select(ViewModels.IdResultText.ProjectFromUserModel).ToList();
        return Ok(viewModels);
      }
      else
      {
        return NoContent();
      }
    }

    private IUserService Service { get { return _service as IUserService; } }
  }
}