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
    public async Task<ActionResult<ViewModels.UserAssessmentClaim>> GetAssessmentClaim(
      int id)
    {
      try
      {
        Business.Models.UserAssessmentClaim businessModel =
          await Service.GetAssessmentClaim(id);

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
  }
}