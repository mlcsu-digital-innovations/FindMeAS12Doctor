using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Fmas12d.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
  public class UserProfileController : ModelControllerDeletePatchBase
  {
    public UserProfileController(
      IUserClaimsService userClaimsService,
      IUserService service)
      : base(userClaimsService, service)
    {
    }

    [Route("")]
    [HttpGet]
    public async Task<ActionResult<ViewModels.UserProfile>> Get()
    {
      Business.Models.User model = await Service.GetAsync(GetUserId(), true, true);
      
      if (model == null)
      {
        return NoContent();    
      }
      else
      {
        ViewModels.UserProfile viewModel = new ViewModels.UserProfile(model);        
        return Ok(viewModel);
      }
    }

    private IUserService Service { get { return _service as IUserService; } }
  }
}