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
  public class AssessmentClaimController : ModelControllerDeletePatchBase
  {
    private IUserAssessmentClaimService Service { 
      get 
      { 
        return _service as IUserAssessmentClaimService; 
      }
    }

    public AssessmentClaimController(
      IUserClaimsService userClaimsService,
      IUserAssessmentClaimService service)
      : base(userClaimsService, service)
    {
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<IEnumerable<ViewModels.UserAssessmentClaim>>> GetUserAssessmentClaims(
      int id)
    {
      try
      {
        Business.Models.UserAssessmentClaim businessModel =
          await Service.GetAssessmentClaimAsync(id);

        if (businessModel == null)
        {
          return NoContent();
        }
        else
        {
          ViewModels.UserAssessmentClaim viewModel =
            new ViewModels.UserAssessmentClaim(businessModel);

          return Ok(viewModel);
        }
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<ViewModels.UserAssessmentClaim>> GetAssessmentClaim(
      int id)
    {
      try
      {
        Business.Models.UserAssessmentClaim businessModel =
          await Service.GetAssessmentClaimAsync(id);

        if (businessModel == null)
        {
          return NoContent();
        }
        else
        {
          ViewModels.UserAssessmentClaim viewModel =
            new ViewModels.UserAssessmentClaim(businessModel);

          return Ok(viewModel);
        }
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpGet]
    [Route("{assessmentId:int}/assessmentandlocations")]
    public async Task<ActionResult<ViewModels.UserAssessmentClaimDetail>> GetAssessmentAndLocations(
      int assessmentId)
    {

      try {
        Business.Models.UserAssessmentClaimDetail businessModel = 
          await Service.GetAssessmentAndContactAsync(assessmentId, GetUserId());

        if (businessModel == null)
        {
          return NoContent();
        }
        else 
        {
          ViewModels.UserAssessmentClaimDetail viewModel = 
            new ViewModels.UserAssessmentClaimDetail(businessModel);

          return Ok(viewModel);
        }
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpPost]
    [Route("{assessmentId:int}/validate")]
    public async Task<ActionResult<ViewModels.UserAssessmentClaim>> CreateUserAssessmentClaim(
      int assessmentId,
      [FromBody] RequestModels.UserAssessmentClaimPost requestModel
    )
    {
      try {

        Business.Models.UserAssessmentClaimCreate businessModel =
          new Business.Models.UserAssessmentClaimCreate();

        requestModel.MapToBusinessModel(businessModel);

        Business.Models.UserAssessmentClaimResult newClaim =
          await Service.ValidateAssessmentClaimAsync(assessmentId, GetUserId(), businessModel);

        if (newClaim == null)
        {
          return NoContent();
        } 
        else
        {
          return Ok(newClaim);
        }
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpPost]
    [Route("{assessmentId:int}/confirm")]
    public async Task<ActionResult<ViewModels.UserAssessmentClaim>> ConfirmUserAssessmentClaim(
      int assessmentId,
      [FromBody] RequestModels.UserAssessmentClaimPost requestModel
    )
    {
      try {

        Business.Models.UserAssessmentClaimCreate businessModel =
          new Business.Models.UserAssessmentClaimCreate();

        requestModel.MapToBusinessModel(businessModel);

        Business.Models.UserAssessmentClaim viewModel =
          await Service.ConfirmAssessmentClaimAsync(assessmentId, GetUserId(), businessModel);

        return Created(GetCreatedModelUri(viewModel.Id), viewModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }
  }
}