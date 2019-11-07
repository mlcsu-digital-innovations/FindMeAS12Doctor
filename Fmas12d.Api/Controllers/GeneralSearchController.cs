using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Mvc;
using Fmas12d.Api.SearchModels;
using business = Fmas12d.Business.Models.SearchModels;
using Fmas12d.Business.Models;
using Fmas12d.Api.RequestModels;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
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
      IEnumerable<Business.Models.SearchModels.GeneralSearchResult> businessSearchResult = 
        await _service.SearchAsync(searchString.Criteria);

      IEnumerable<GeneralSearchResult> searchResults = 
        _mapper.Map<IEnumerable<GeneralSearchResult>>(businessSearchResult);

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