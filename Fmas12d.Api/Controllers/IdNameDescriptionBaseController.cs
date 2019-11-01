using Fmas12d.Api.RequestModels;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public abstract class IdNameDescriptionBaseController : ModelControllerNoAutoMapper
  {
    protected IdNameDescriptionBaseController(INameDescriptionBaseService service)
      : base(service)
    {
    }

    [HttpGet]
    [Route("list")]
    public async Task<ActionResult<IEnumerable<ViewModels.IdNameDescription>>> Get(
      [FromQuery] ListType listType)
    {
      IEnumerable<Business.Models.INameDescription> businessModels = null;

      if (listType.IsDropDownList)
      {
        businessModels = await Service.GetNameDescriptions();
      }

      if (businessModels == null || !businessModels.Any())
      {
        return NoContent();
      }
      else
      {
        IEnumerable<ViewModels.IdNameDescription> viewModels =
          businessModels.Select(ViewModels.IdNameDescription.ProjectFromModel).ToList();

        return Ok(viewModels);
      }
    }

    private INameDescriptionBaseService Service
    {
      get { return _service as INameDescriptionBaseService; }
    }
  }
}