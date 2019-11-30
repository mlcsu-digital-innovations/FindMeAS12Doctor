using System;
using System.Threading.Tasks;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fmas12d.Api.Controllers
{
  [Route("api/user")]
  [ApiController]
  [Authorize(Policy="User")]
  
  public class UserAvailabilityController : ModelControllerNoAutoMapper
  {
    public UserAvailabilityController(      
      IUserAvailabilityService service,
      IUserClaimsService userClaimsService)
      : base(userClaimsService, service)
    {
    }

    [HttpPost]
    [Route("{userId:int}/availability/contactdetail")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PostAvailableContactDetail(
      int userId,
      [FromBody] RequestModels.UserAvailabilityPostContactDetail requestModel)
    {
      return await Create(
        userId, 
        Business.Models.UserAvailabilityStatus.AVAILABLE,
        requestModel);
    }

    [HttpPost]
    [Route("{userId:int}/availability/location")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PostAvailableLocation(
      int userId,
      [FromBody] RequestModels.UserAvailabilityPostLocation requestModel)
    {
      return await Create(
        userId, 
        Business.Models.UserAvailabilityStatus.AVAILABLE,
        requestModel);
    }

    [HttpPost]
    [Route("{userId:int}/availability/postcode")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PostAvailablePostcode(
      int userId,
      [FromBody] RequestModels.UserAvailabilityPostPostcode requestModel)
    {
      return await Create(
        userId, 
        Business.Models.UserAvailabilityStatus.AVAILABLE,
        requestModel);
    }

    [HttpPost]
    [Route("{userId:int}/availability/unavailable")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PostUnavailable(
      int userId,
      [FromBody] RequestModels.UserAvailability requestModel)
    {
      return await Create(
        userId, 
        Business.Models.UserAvailabilityStatus.UNAVAILABLE,
        requestModel);
    }       

    private async Task<ActionResult<ViewModels.UserAvailability>> Create(
      int userId,
      int statusId,
      RequestModels.UserAvailability requestModel)
    {
      try
      {
        Business.Models.IUserAvailability businessModel = requestModel.MapToBusinessModel(userId);
        businessModel.StatusId = statusId;
        businessModel = await Service.Create(businessModel);
        ViewModels.UserAvailability viewModel = new ViewModels.UserAvailability(businessModel);

        return Ok(viewModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    private IUserAvailabilityService Service
    {
      get { return _service as IUserAvailabilityService; }
    }
  }
}