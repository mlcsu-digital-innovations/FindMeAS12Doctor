using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fmas12d.Api.Controllers
{
  [Route("useravailabilities")]
  [ApiController]
  [Authorize(Policy="User")]
  
  public class UserAvailabilitiesController : ModelControllerDeletePatchBase
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
    [Route("{id:int}")]
    public async Task<ActionResult<IEnumerable<ViewModels.UserAvailability>>> Get(
      int id
    )
    {
      try
      {
        Business.Models.IUserAvailability businessModel = 
          await Service.GetAsync(id, true, true);

        if (businessModel == null)
        {
          return NoContent();
        }
        else
        {          
          ViewModels.UserAvailability viewModel = new ViewModels.UserAvailability(businessModel);
          return Ok(viewModel);
        }
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }  
    }

    [HttpGet]
    [Route("overlapping/{userId:int}")]
    public async Task<ActionResult<ViewModels.UserAvailabilityOverlapping>> CheckOverlapping(
      int userId,
      [FromQuery] int userAvailabilityId,
      [FromQuery] DateTimeOffset? start,
      [FromQuery] DateTimeOffset? end   
    )
    {
      try
      {
        if (userId == 0) {
          return BadRequest("Please select a doctor");
        }
        if (!start.HasValue) {
          return BadRequest("Please select a start date");
        }
        if (!end.HasValue) {
          return BadRequest("Please select an end date");
        }

        Business.Models.UserAvailabilityOverlapping businessModel = await Service
          .CheckOverlapWithExisting(userId, userAvailabilityId, start.Value, end.Value);
        ViewModels.UserAvailabilityOverlapping viewModel = 
          new ViewModels.UserAvailabilityOverlapping(businessModel);


        return Ok(viewModel);       
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      } 
    }

    [HttpPost]
    [Route("contactdetail")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PostAvailableContactDetail(
      [FromBody] RequestModels.UserAvailabilityPostContactDetail requestModel
    )
    {
      return await Create(
        GetUserId(),
        Business.Models.UserAvailabilityStatus.AVAILABLE,
        requestModel
      );
    }

    [HttpPost]
    [Authorize(Policy="Admin")]
    [Route("contactdetail/{userId:int}")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PostAvailableContactDetail(
      int userId,
      [FromBody] RequestModels.UserAvailabilityPostContactDetail requestModel
    )
    {
      return await Create(
        userId, 
        Business.Models.UserAvailabilityStatus.AVAILABLE,
        requestModel
      );
    }

    [HttpPost]
    [Route("location")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PostAvailableLocation(
      [FromBody] RequestModels.UserAvailabilityPostLocation requestModel
    )
    {
      return await Create(
        GetUserId(), 
        Business.Models.UserAvailabilityStatus.AVAILABLE,
        requestModel
      );
    }    

    [HttpPost]
    [Authorize(Policy="Admin")]
    [Route("location/{userId:int}")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PostAvailableLocation(
      int userId,
      [FromBody] RequestModels.UserAvailabilityPostLocation requestModel
    )
    {
      return await Create(
        userId, 
        Business.Models.UserAvailabilityStatus.AVAILABLE,
        requestModel
      );
    }

    [HttpPost]
    [Route("postcode")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PostAvailablePostcode(
      [FromBody] RequestModels.UserAvailabilityPostPostcode requestModel
    )
    {
      return await Create(
        GetUserId(), 
        Business.Models.UserAvailabilityStatus.AVAILABLE,
        requestModel
      );
    }

    [HttpPost]
    [Authorize(Policy="Admin")]
    [Route("postcode/{userId:int}")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PostAvailablePostcode(
      int userId,
      [FromBody] RequestModels.UserAvailabilityPostPostcode requestModel
    )
    {
      return await Create(
        userId, 
        Business.Models.UserAvailabilityStatus.AVAILABLE,
        requestModel
      );
    }    

    [HttpPost]
    [Route("unavailable")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PostUnavailable(
      [FromBody] RequestModels.UserAvailability requestModel
    )
    {
      return await Create(
        GetUserId(), 
        Business.Models.UserAvailabilityStatus.UNAVAILABLE,
        requestModel
      );
    }     

    [HttpPost]
    [Authorize(Policy="Admin")]
    [Route("unavailable/{userId:int}")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PostUnavailable(
      int userId,
      [FromBody] RequestModels.UserAvailability requestModel
    )
    {
      return await Create(
        userId, 
        Business.Models.UserAvailabilityStatus.UNAVAILABLE,
        requestModel
      );
    } 

    [HttpPut]
    [Route("{id:int}/contactdetail")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PutAvailableContactDetail(
      int id,
      [FromBody] RequestModels.UserAvailabilityPutContactDetail requestModel
    )
    {
      return await Update(
        id,
        GetUserId(),
        Business.Models.UserAvailabilityStatus.AVAILABLE,
        requestModel
      );
    }

    [HttpPut]
    [Authorize(Policy="Admin")]
    [Route("{id:int}/contactdetail/{userId:int}")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PutAvailableContactDetail(
      int id,
      int userId,
      [FromBody] RequestModels.UserAvailabilityPutContactDetail requestModel
    )
    {
      return await Update(
        id,
        userId, 
        Business.Models.UserAvailabilityStatus.AVAILABLE,
        requestModel
      );
    }

    [HttpPut]
    [Route("{id:int}/location")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PutAvailableLocation(
      int id,
      [FromBody] RequestModels.UserAvailabilityPutLocation requestModel
    )
    {
      return await Update(
        id,
        GetUserId(), 
        Business.Models.UserAvailabilityStatus.AVAILABLE,
        requestModel
      );
    }    

    [HttpPut]
    [Authorize(Policy="Admin")]
    [Route("{id:int}/location/{userId:int}")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PutAvailableLocation(
      int id,
      int userId,
      [FromBody] RequestModels.UserAvailabilityPutLocation requestModel
    )
    {
      return await Update(
        id,
        userId, 
        Business.Models.UserAvailabilityStatus.AVAILABLE,
        requestModel
      );
    }

    [HttpPut]
    [Route("{id:int}/postcode")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PutAvailablePutcode(
      int id,
      [FromBody] RequestModels.UserAvailabilityPutPostcode requestModel
    )
    {
      return await Update(
        id,
        GetUserId(), 
        Business.Models.UserAvailabilityStatus.AVAILABLE,
        requestModel
      );
    }

    [HttpPut]
    [Authorize(Policy="Admin")]
    [Route("{id:int}/postcode/{userId:int}")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PutAvailablePutcode(
      int id,
      int userId,
      [FromBody] RequestModels.UserAvailabilityPutPostcode requestModel
    )
    {
      return await Update(
        id,
        userId, 
        Business.Models.UserAvailabilityStatus.AVAILABLE,
        requestModel
      );
    }    

    [HttpPut]
    [Route("{id:int}/unavailable")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PutUnavailable(
      int id,
      [FromBody] RequestModels.UserAvailability requestModel
    )
    {
      return await Update(
        id,
        GetUserId(), 
        Business.Models.UserAvailabilityStatus.UNAVAILABLE,
        requestModel
      );
    }     

    [HttpPut]
    [Authorize(Policy="Admin")]
    [Route("{id:int}/unavailable/{userId:int}")]
    public async Task<ActionResult<ViewModels.UserAvailability>> PutUnavailable(
      int id,
      int userId,
      [FromBody] RequestModels.UserAvailability requestModel)
    {
      return await Update(
        id,
        userId, 
        Business.Models.UserAvailabilityStatus.UNAVAILABLE,
        requestModel
      );
    }            

    private async Task<ActionResult<ViewModels.UserAvailability>> Create(
      int userId,
      int statusId,
      RequestModels.UserAvailability requestModel
    )
    {
      try
      {
        Business.Models.IUserAvailability businessModel = new Business.Models.UserAvailability
        {
          UserId = userId,
          StatusId = statusId
        };
        requestModel.MapToBusinessModel(businessModel);
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
          await Service.GetListAsync(userId, from.Value, true, true);

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

    private async Task<ActionResult<ViewModels.UserAvailability>> Update(
      int id,
      int userId,      
      int statusId,
      RequestModels.UserAvailability requestModel
    )
    {
      try
      {
        Business.Models.IUserAvailability businessModel = new Business.Models.UserAvailability
        {
          Id = id,
          StatusId = statusId,
          UserId = userId
        };
        requestModel.MapToBusinessModel(businessModel);
        businessModel = await Service.UpdateAsync(businessModel);
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