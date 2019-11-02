using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UnsuccessfulAssessmentTypeController : IdNameDescriptionBaseController
  {
    public UnsuccessfulAssessmentTypeController(IUnsuccessfulAssessmentTypeService service)
      : base(service)
    {
    }
  }
}