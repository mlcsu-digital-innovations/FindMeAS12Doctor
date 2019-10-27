using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using Mep.Api.SearchModels;
using business = Mep.Business.Models.SearchModels;
using Mep.Business.Models;
using Mep.Api.RequestModels;
using System.Linq;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public abstract class GeneralSearchController<BusinessModel> : ControllerBase where BusinessModel : BaseModel
  {
    protected readonly IModelGeneralSearchService<BusinessModel> _service;
    protected readonly IMapper _mapper;

    protected GeneralSearchController(IModelGeneralSearchService<BusinessModel> service, IMapper mapper)
    {
      _service = service;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GeneralSearchResult>>> Get([FromQuery] SearchString searchString )
    {
      IEnumerable<business.GeneralSearchResult> businessSearchResult = await _service.SearchAsync(searchString.Criteria);

      IEnumerable<GeneralSearchResult> searchResults = _mapper.Map<IEnumerable<GeneralSearchResult>>(businessSearchResult);

      if (searchResults.Any())
      {
        return Ok(searchResults);
      }
      else
      {
        return NoContent();
      }      
    }

  }
}