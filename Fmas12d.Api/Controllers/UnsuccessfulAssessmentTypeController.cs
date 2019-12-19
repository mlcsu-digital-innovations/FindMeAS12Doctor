using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fmas12d.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
  
  public class UnsuccessfulAssessmentTypeController : IdNameDescriptionBaseController
  {
    public UnsuccessfulAssessmentTypeController(
      IUnsuccessfulAssessmentTypeService service,
      IUserClaimsService userClaimsService)
      : base(userClaimsService, service)
    {
    }
  }
}