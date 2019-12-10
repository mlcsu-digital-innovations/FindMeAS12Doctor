using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
  
  public class AssessmentDetailTypeController : IdNameDescriptionBaseController
  {
    public AssessmentDetailTypeController(
      IUserClaimsService userClaimsService,
      IAssessmentDetailTypeService service)
      : base(userClaimsService, service)
    {
    }
  }
}