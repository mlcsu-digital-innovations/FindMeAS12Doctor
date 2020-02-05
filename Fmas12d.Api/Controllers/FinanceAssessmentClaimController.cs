using Fmas12d.Business.Models;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fmas12d.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  [Authorize(Policy = "User")]
  public class FinanceAssessmentClaimController : ModelControllerDeletePatchBase
  {
    private IFinanceAssessmentClaimService Service { 
      get 
      { 
        return _service as IFinanceAssessmentClaimService; 
      }
    }

    public FinanceAssessmentClaimController(
      IUserClaimsService userClaimsService,
      IFinanceAssessmentClaimService service)
      : base(userClaimsService, service)
    {
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<ViewModels.FinanceAssessmentClaim>> GetFinanceAssessmentClaim(int id)
    {
      try
      {
        Business.Models.FinanceAssessmentClaim businessModel = await Service.GetClaimByIdAsync(id);

        if (businessModel == null)
        {
          return NoContent();
        }
        else
        {
          ViewModels.FinanceAssessmentClaim viewModel =
            new ViewModels.FinanceAssessmentClaim(businessModel);
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
    public async Task<ActionResult<IEnumerable<ViewModels.FinanceAssessmentClaim>>> GetFinanceAssessmentClaimList()
    {
      try
      {
        IEnumerable<Business.Models.FinanceAssessmentClaim> businessModels =
          await Service.GetListAsync();

        if (businessModels == null || !businessModels.Any())
        {
          return NoContent();
        }
        else
        {
          IEnumerable<ViewModels.FinanceAssessmentClaim> viewModels =
              businessModels
              .Select(ViewModels.FinanceAssessmentClaim.ProjectFromModel).ToList();

          return Ok(viewModels);
        }
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
      [FromBody] RequestModels.FinanceAssessmentClaimUpdate requestModel)
    {
      try
      {
        Business.Models.FinanceAssessmentClaimUpdate businessModel =
          new Business.Models.FinanceAssessmentClaimUpdate();
        requestModel.MapToBusinessModel(businessModel);
        businessModel.Id = id;
        Business.Models.FinanceAssessmentClaim updateModel = await Service.UpdateClaimStatusAsync(businessModel);
        ViewModels.FinanceAssessmentClaim viewModel = new ViewModels.FinanceAssessmentClaim(updateModel);

        return Ok(viewModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }
  }
}