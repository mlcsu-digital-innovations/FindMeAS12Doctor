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
    [Route("list")]
    public async Task<ActionResult<IEnumerable<ViewModels.FinanceAssessmentClaimList>>> GetFinanceAssessmentClaimList()
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
          IEnumerable<ViewModels.FinanceAssessmentClaimList> viewModels =
              businessModels
              .Select(ViewModels.FinanceAssessmentClaimList.ProjectFromModel).ToList();

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