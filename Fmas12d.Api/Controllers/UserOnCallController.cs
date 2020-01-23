using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fmas12d.Api.Controllers
{
  [Route("user/oncall")]
  [ApiController]
  [Authorize(Policy = "User")]

  public class UserOnCallController : ModelControllerDeletePatchBase
  {
    public UserOnCallController(
      IUserAvailabilityService service,
      IUserClaimsService userClaimsService)
      : base(userClaimsService, service)
    {
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<IEnumerable<ViewModels.UserOnCall>>> GetList(
      [FromQuery] DateTimeOffset? from = null,
      [FromQuery] DateTimeOffset? to = null
    )
    {
      return await GetListInternal(from, to);
    }

    [HttpPost]
    [Route("contactdetail")]
    public async Task<ActionResult<ViewModels.UserOnCall>> PostContactDetail(
      [FromBody] RequestModels.UserOnCallPostContactDetail requestModel
    )
    {
      return await Create(requestModel);
    }

    [HttpPost]
    [Route("postcode")]
    public async Task<ActionResult<ViewModels.UserOnCall>> PostPostcode(
      [FromBody] RequestModels.UserOnCallPostPostcode requestModel
    )
    {
      return await Create(requestModel);
    }

    [HttpPut]
    [Route("{id:int}/contactdetail")]
    public async Task<ActionResult<ViewModels.UserOnCall>> PutContactDetail(
      int id,
      [FromBody] RequestModels.UserOnCallPutContactDetail requestModel
    )
    {
      return await Update(id, requestModel);
    }

    [HttpPut]
    [Route("{id:int}/postcode")]
    public async Task<ActionResult<ViewModels.UserOnCall>> PutPutcode(
      int id,
      [FromBody] RequestModels.UserOnCallPutPostcode requestModel
    )
    {
      return await Update(id, requestModel);
    }

    private async Task<ActionResult<ViewModels.UserOnCall>> Create(
      RequestModels.UserOnCall requestModel
    )
    {
      try
      {
        Business.Models.IUserOnCall businessModel = new Business.Models.UserOnCall()
        {
          StatusId = Business.Models.UserAvailabilityStatus.ON_CALL
        };
        requestModel.MapToBusinessModel(businessModel);
        businessModel = await Service.CreateOnCallAsync(businessModel);
        ViewModels.UserOnCall viewModel = new ViewModels.UserOnCall(businessModel);

        return Ok(viewModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    private async Task<ActionResult<IEnumerable<ViewModels.UserOnCall>>> GetListInternal(
      DateTimeOffset? from,
      DateTimeOffset? to
    )
    {
      try
      {
        if (from == null)
        {
          from = DateTimeOffset.Now;
        }
        if (to == null)
        {
          to = DateTimeOffset.Now.AddMonths(1);
        }

        IEnumerable<Business.Models.IUserOnCall> businessModels =
          await Service.GetOnCallAsync(from.Value, to.Value, true, true);

        if (businessModels == null || !businessModels.Any())
        {
          return NoContent();
        }
        else
        {
          IEnumerable<ViewModels.UserOnCall> viewModels =
            businessModels.Select(ViewModels.UserOnCall.ProjectFromModel).ToList();

          return Ok(viewModels);
        }
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    private async Task<ActionResult<ViewModels.UserOnCall>> Update(
      int id,
      RequestModels.UserOnCall requestModel
    )
    {
      try
      {
        Business.Models.IUserOnCall businessModel = new Business.Models.UserOnCall()
        {
          Id = id,
          StatusId = Business.Models.UserAvailabilityStatus.ON_CALL
        };
        requestModel.MapToBusinessModel(businessModel);

        businessModel = await Service.UpdateOnCallAsync(businessModel);
        ViewModels.UserOnCall viewModel = new ViewModels.UserOnCall(businessModel);

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