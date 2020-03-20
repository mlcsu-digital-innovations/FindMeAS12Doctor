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
      try
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
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpPut]
    [Route("")]
    public async Task<ActionResult<ViewModels.UserProfile>> Put(
      [FromBody] RequestModels.UserProfilePut requestModel
    )
    {
      try
      {
        Business.Models.UserProfileUpdate businessModel = new Business.Models.UserProfileUpdate
        {
          Id = GetUserId()
        };
        requestModel.MapToBusinessModel(businessModel);
        Business.Models.User userModel = await Service.UpdateAsync(businessModel);
        ViewModels.UserProfile viewModel = new ViewModels.UserProfile(userModel);

        return Ok(viewModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }     
    }   

    [HttpPut]
    [Route("updatevsr")]
    public async Task<ActionResult<ViewModels.UserProfile>> UpdateVsr(
      [FromBody] RequestModels.VsrUpdate requestModel
    )
    {
      try
      {
        Business.Models.VsrUpdate businessModel = new Business.Models.VsrUpdate();

        requestModel.MapToBusinessModel(businessModel);

        Business.Models.User userModel = await Service.UpdateVsrAsync(businessModel);
        ViewModels.UserProfile viewModel = new ViewModels.UserProfile(userModel);

        return Ok(viewModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }     
    }  

    private IUserService Service { get { return _service as IUserService; } }
  }
}