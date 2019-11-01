using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ExaminationDetailTypeController : IdNameDescriptionBaseController
  {
    public ExaminationDetailTypeController( IExaminationDetailTypeService service)
      : base(service)
    {
    }
  }
}