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

    [Route("s12register/{id}")]
    [HttpGet]
    public async Task<ActionResult<ViewModels.User>> GetS12(int id)
    {
      Business.Models.User model = await Service.GetS12Async(id, true, true);
      
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
    public async Task<ActionResult<IEnumerable<ViewModels.DoctorSearchResult>>> GetSearch(
      [FromQuery] RequestModels.UserSearch search)
    {
      IEnumerable<Business.Models.User> models = null;

      if (search.IsByAmhpName)
      {
        models = await Service.GetAllByAmhpNameAsync(search.AmhpName, activeOnly: false);
      }
      else if (search.IsByDoctorName)
      {
        models =
          await Service.GetAllByDoctorNameAsync(
            search.DoctorName,
            activeOnly: false,
            includeUnregisteredDoctors: search.IncludeUnregisteredDoctors
          );
      }
      else if (search.IsByGmcNumber)
      {
        models =
          await Service.GetAllByGmcNumberAsync(
            search.GmcNumber,
            activeOnly: false,
            includeUnregisteredDoctors: search.IncludeUnregisteredDoctors
          );
      }

      if (models.Any())
      {
        IEnumerable<ViewModels.DoctorSearchResult> viewModels = 
          models.Select(ViewModels.DoctorSearchResult.ProjectFromUserModel).ToList();
        return Ok(viewModels);
      }
      else
      {
        return NoContent();
      }
    }

    [HttpPut]
    [Route("refreshToken/{token}")]
    public async Task<ActionResult> Put(string token)
    {
      try
      {
        await Service.RefreshFcmToken(GetUserId(), token);

        return Ok();
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpPost]
    [Route("invite")]
    public async Task<ActionResult<ViewModels.UserAvailability>> InviteUser(
      [FromBody] string InvitedUserEmailAddress
    )
    {
      try
      {
        await Service.InviteUserToGroup(InvitedUserEmailAddress);
        return Ok();
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    private IUserService Service { get { return _service as IUserService; } }
  }
}