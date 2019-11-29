using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
  public class ReferralController : ModelControllerNoAutoMapper
  {
    public ReferralController(IReferralService service)
      : base(service)
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
      try
      {
        IEnumerable<Business.Models.Referral> businessModels = await Service.GetListAsync(true);

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
        Business.Models.ReferralCreate businessModel = requestModel.MapToBusinessModel();
        Business.Models.Referral createdModel = await Service.CreateAsync(businessModel);
        ViewModels.ReferralPost viewModel = new ViewModels.ReferralPost(createdModel);

        return Created(GetCreatedModelUri(viewModel.Id), viewModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }     

  }
}