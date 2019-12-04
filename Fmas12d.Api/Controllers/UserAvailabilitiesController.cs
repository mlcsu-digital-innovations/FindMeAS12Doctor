using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fmas12d.Api.Controllers
{
  [Route("api/useravailabilities")]
  [ApiController]
  [Authorize(Policy="User")]
  
  public class UserAvailabilitiesController : ModelControllerNoAutoMapper
  {
    public UserAvailabilitiesController(
      IUserAvailabilityService service,
      IUserClaimsService userClaimsService)
      : base(userClaimsService, service)
    {
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<IEnumerable<ViewModels.UserAvailability>>> GetList(
      [FromQuery] DateTimeOffset? from = null
    )
    {
      return await GetListInternal(GetUserId(), from);
    }

    [HttpGet]
    [Authorize(Policy="Admin")]
    [Route("{userId:int}")]
    public async Task<ActionResult<IEnumerable<ViewModels.UserAvailability>>> GetList(
      int userId,
      [FromQuery] DateTimeOffset? from = null
    )
    {
      return await GetListInternal(userId, from);
    }

    [HttpPost]
    [Route("contactdetail")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PostAvailableContactDetail(
      [FromBody] RequestModels.UserAvailabilityPostContactDetail requestModel)
    {
      return await Create(
        GetUserId(),
        Business.Models.UserAvailabilityStatus.AVAILABLE,
        requestModel);
    }

    [HttpPost]
    [Authorize(Policy="Admin")]
    [Route("contactdetail/{userId:int}")]
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
    [Route("location")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PostAvailableLocation(
      [FromBody] RequestModels.UserAvailabilityPostLocation requestModel)
    {
      return await Create(
        GetUserId(), 
        Business.Models.UserAvailabilityStatus.AVAILABLE,
        requestModel);
    }    

    [HttpPost]
    [Authorize(Policy="Admin")]
    [Route("location/{userId:int}")]
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
    [Route("postcode")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PostAvailablePostcode(
      [FromBody] RequestModels.UserAvailabilityPostPostcode requestModel)
    {
      return await Create(
        GetUserId(), 
        Business.Models.UserAvailabilityStatus.AVAILABLE,
        requestModel);
    }

    [HttpPost]
    [Authorize(Policy="Admin")]
    [Route("postcode/{userId:int}")]
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
    [Route("unavailable")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PostUnavailable(
      [FromBody] RequestModels.UserAvailability requestModel)
    {
      return await Create(
        GetUserId(), 
        Business.Models.UserAvailabilityStatus.UNAVAILABLE,
        requestModel);
    }     

    [HttpPost]
    [Authorize(Policy="Admin")]
    [Route("unavailable/{userId:int}")]
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
        businessModel = await Service.CreateAsync(businessModel);
        ViewModels.UserAvailability viewModel = new ViewModels.UserAvailability(businessModel);

        return Ok(viewModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    private async Task<ActionResult<IEnumerable<ViewModels.UserAvailability>>> GetListInternal(
      int userId,
      DateTimeOffset? from
    )
    {
      try
      {
        if (from == null)
        {
          from = DateTimeOffset.Now;
        }

        IEnumerable<Business.Models.IUserAvailability> businessModels = 
          await Service.GetAsync(userId, from.Value, true, true);

        if (businessModels == null || !businessModels.Any())
        {
          return NoContent();
        }
        else
        {          
          IEnumerable<ViewModels.UserAvailability> viewModels =
            businessModels.Select(ViewModels.UserAvailability.ProjectFromModel).ToList();

          return Ok(viewModels);
        }
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