using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UnsuccessfulExaminationTypeController : IdNameDescriptionBaseController
  {
    public UnsuccessfulExaminationTypeController(IUnsuccessfulExaminationTypeService service)
      : base(service)
    {
    }
  }
}