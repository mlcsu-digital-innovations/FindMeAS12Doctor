using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fmas12d.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  [Authorize(Policy = "User")]
  public class ReferralController : ModelControllerDeletePatchBase
   {
    public ReferralController(
      IReferralService service,
      IUserClaimsService userClaimsService)
      : base(userClaimsService, service)
    {     
    }

    private IReferralService Service { get { return _service as IReferralService; } }

    [HttpGet]
    [Route("edit/{id:int}")]
    public async Task<ActionResult<ViewModels.ReferralEdit>> GetEdit(int id)
    {
      try
      {
        Business.Models.Referral businessModel = await Service.GetEditByIdAsync(id);

        if (businessModel == null)
        {
          return NoContent();
        }
        else
        {
          ViewModels.ReferralEdit viewModel = new ViewModels.ReferralEdit(businessModel);
          return Ok(viewModel);
        }
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpGet]
    [Route("list")]
    public async Task<ActionResult<IEnumerable<ViewModels.ReferralList>>> GetList()
    {
      return await GetListInternal(null, null);
    }

    [HttpGet]
    [Route("list/closed")]
    public async Task<ActionResult<IEnumerable<ViewModels.ReferralList>>> GetListClosed()
    {
      return await GetListInternal(null, new List<int>{ Business.Models.ReferralStatus.CLOSED });
    }

    [HttpGet]
    [Route("list/open")]
    public async Task<ActionResult<IEnumerable<ViewModels.ReferralList>>> GetListOpen()
    {
      return await GetListInternal(new List<int>{ Business.Models.ReferralStatus.CLOSED }, null);
    }

    [HttpGet]
    [Route("view/{id:int}")]
    public async Task<ActionResult<ViewModels.ReferralView>> GetView(int id)
    {
      try
      {

        Business.Models.Referral businessModel = await Service.GetViewByIdAsync(id);

        if (businessModel == null)
        {
          return NoContent();
        }
        else
        {
          ViewModels.ReferralView viewModel = new ViewModels.ReferralView(businessModel);
          return Ok(viewModel);
        }
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpGet]
    [Route("view/{id:int}/summary")]
    public async Task<ActionResult<ViewModels.ReferralViewSummary>> GetViewSummary(int id)
    {
      try
      {

        Business.Models.Referral businessModel = await Service.GetViewByIdAsync(id);

        if (businessModel == null)
        {
          return NoContent();
        }
        else
        {
          ViewModels.ReferralViewSummary viewModel =
            new ViewModels.ReferralViewSummary(businessModel);
          return Ok(viewModel);
        }
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpPost]
    public virtual async Task<ActionResult<ViewModels.Referral>> Post(
      [FromBody] RequestModels.ReferralPost requestModel)
    {
      try
      {
        Business.Models.ReferralCreate businessModel = new Business.Models.ReferralCreate();
        requestModel.MapToBusinessModel(businessModel);
        Business.Models.Referral createdModel = await Service.CreateAsync(businessModel);
        ViewModels.ReferralPost viewModel = new ViewModels.ReferralPost(createdModel);

        return Created(GetCreatedModelUri(viewModel.Id), viewModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpPost]
    [Route("retrospective")]
    public virtual async Task<ActionResult<ViewModels.Referral>> PostRetrospective(
      [FromBody] RequestModels.ReferralRetrospectivePost requestModel)
    {
      try
      {
        Business.Models.ReferralCreate businessModel = new Business.Models.ReferralCreate();
        requestModel.MapToBusinessModel(businessModel);
        Business.Models.Referral createdModel =
          await Service.CreateRetrospectiveAsync(businessModel);
        ViewModels.ReferralPost viewModel = new ViewModels.ReferralPost(createdModel);

        return Created(GetCreatedModelUri(viewModel.Id), viewModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpPut]
    [Route("{id:int}")]
    public virtual async Task<ActionResult<ViewModels.Referral>> Put(
      int id,
      [FromBody] RequestModels.ReferralPut requestModel)
    {
      try
      {
        Business.Models.ReferralUpdate businessModel = new Business.Models.ReferralUpdate();
        requestModel.MapToBusinessModel(businessModel);
        businessModel.Id = id;
        Business.Models.Referral updateModel = await Service.UpdateAsync(businessModel);
        ViewModels.ReferralPost viewModel = new ViewModels.ReferralPost(updateModel);

        return Ok(viewModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpPut]
    [Route("{id:int}/close")]
    public virtual async Task<ActionResult<ViewModels.Referral>> PutClose(
      int id
    )
    {
      try
      {
        await Service.CloseAsync(id);
        return Ok();
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpPut]
    [Route("{id:int}/close/force")]
    public virtual async Task<ActionResult<ViewModels.Referral>> PutCloseForce(
      int id
    )
    {
      try
      {
        await Service.CloseForceAsync(id);
        return Ok();
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }


    [HttpPut]
    [Route("{id:int}/retrospective")]
    public virtual async Task<ActionResult<ViewModels.Referral>> PutRetrospective(
      int id,
      [FromBody] RequestModels.ReferralRetrospectivePut requestModel)
    {
      try
      {
        Business.Models.ReferralUpdate businessModel = new Business.Models.ReferralUpdate();
        requestModel.MapToBusinessModel(businessModel);
        businessModel.Id = id;
        Business.Models.Referral updatedModel =
          await Service.UpdateRetrospectiveAsync(businessModel);
        ViewModels.ReferralPost viewModel = new ViewModels.ReferralPost(updatedModel);

        return Ok(viewModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    private async Task<ActionResult<IEnumerable<ViewModels.ReferralList>>> GetListInternal(
      List<int> excludeStatusIds,
      List<int> includeStatusIds
    )
    {
      try
      {
        IEnumerable<Business.Models.Referral> businessModels =
          await Service.GetListAsync(excludeStatusIds, includeStatusIds, true, true);

        if (businessModels == null || !businessModels.Any())
        {
          return NoContent();
        }
        else
        {
          IEnumerable<ViewModels.ReferralList> viewModels =
              businessModels.Select(ViewModels.ReferralList.ProjectFromModel).ToList();

          return Ok(viewModels);

        }
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }
  }
}