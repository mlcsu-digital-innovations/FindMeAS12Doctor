using Mep.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
  public class ExaminationDetailTypeController : IdNameDescriptionBaseController
  {
    public ExaminationDetailTypeController( IExaminationDetailTypeService service)
      : base(service)
    {
    }
  }
}