using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mep.Api.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ModelControllerNoAutoMapper
  {
    public UserController(IUserService service)
      : base(service)
    {
    }

    [Route("search")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ViewModels.IdResultText>>> GetSearch(
      [FromQuery] RequestModels.UserSearch search)
    {
      IEnumerable<Business.Models.User> models;
      if (search.IsByAmhpName)
      {
        models = await Service.GetAllByAmhpName(search.AmhpName, activeOnly: false);
      }
      else
      {
        models = await Service.GetAllByDoctorName(search.DoctorName, activeOnly: false);
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