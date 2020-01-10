using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fmas12d.Api.Controllers
{

  [Route("[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
  public class UserController : ModelControllerDeletePatchBase
  {
    public UserController(
      IUserClaimsService userClaimsService,
      IUserService service)
      : base(userClaimsService, service)
    {
    }

    [Route("")]
    [HttpGet]
    public async Task<ActionResult<ViewModels.User>> Get()
    {
      Business.Models.User model = await Service.GetAsync(GetUserId(), true, true);
      
      if (model == null)
      {
        return NoContent();    
      }
      else
      {
        ViewModels.User viewModel = new ViewModels.User(model);        
        return Ok(viewModel);
      }
    }  

    [Route("{id:int}")]
    [HttpGet]
    public async Task<ActionResult<ViewModels.User>> Get(int id)
    {
      Business.Models.User model = await Service.GetAsync(id, true, true);
      
      if (model == null)
      {
        return NoContent();    
      }
      else
      {
        ViewModels.User viewModel = new ViewModels.User(model);        
        return Ok(viewModel);
      }
    }        

    [Route("identifier/{id}")]
    [HttpGet]
    public async Task<ActionResult<ViewModels.User>> Get(string id)
    {
      Business.Models.User model = await Service.GetByIdentityServerIdentifierAsync(id, true, true);
      
      if (model == null)
      {
        return NoContent();    
      }
      else
      {
        ViewModels.User viewModel = new ViewModels.User(model);        
        return Ok(viewModel);
      }
    } 

    [Route("search")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ViewModels.IdResultText>>> GetSearch(
      [FromQuery] RequestModels.UserSearch search)
    {
      IEnumerable<Business.Models.User> models = null;

      if (search.IsByAmhpName)
      {
        models = await Service.GetAllByAmhpNameAsync(search.AmhpName, activeOnly: false);
      }
      else if (search.IsByDoctorName)
      {
        models = await Service.GetAllByDoctorNameAsync(search.DoctorName, activeOnly: false, includeUnregisteredDoctors: search.IncludeUnregisteredDoctors);
      }
      else if (search.IsByGmcNumber)
      {
        models = await Service.GetAllByGmcNumberAsync(search.GmcNumber, activeOnly: false, includeUnregisteredDoctors: search.IncludeUnregisteredDoctors);
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