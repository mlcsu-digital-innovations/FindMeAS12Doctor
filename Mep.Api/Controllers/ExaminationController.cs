using AutoMapper;
using BusinessModels = Mep.Business.Models;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ExaminationController :
    ModelController<BusinessModels.Examination,
                    ViewModels.Examination,
                    RequestModels.ExaminationPut,
                    RequestModels.ExaminationPost>
  {
    public ExaminationController(
      IModelService<BusinessModels.Examination> service,
      IMapper mapper)
      : base(service, mapper)
    {
    }

    [HttpGet]
    [Route("list")]
    public async Task<ActionResult<IEnumerable<ViewModels.ExaminationList>>> GetList([FromQuery]
      RequestModels.ExaminationListSearch examinationListSearch)
    {

      IEnumerable<BusinessModels.Examination> businessModels = null;
      if (examinationListSearch.AmhpUserId.HasValue)
      {
        businessModels = await (_service as ExaminationService)
          .GetAllFilterByAmhpUserIdAsync((int)examinationListSearch.AmhpUserId, true, false);
      }

      IEnumerable<ViewModels.ExaminationList> viewModels =
          _mapper.Map<IEnumerable<ViewModels.ExaminationList>>(businessModels);

      if (viewModels.Any())
      {
        return Ok(viewModels);
      }
      else
      {
        return NoContent();
      }
    }
  }
}