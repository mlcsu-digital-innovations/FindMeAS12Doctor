using Mep.Api.RequestModels;
using Mep.Api.SearchModels;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GpPracticeController :
    ModelControllerNoAutoMapper<Business.Models.GpPractice>
  {
    public GpPracticeController(
      IGpPracticeService service)
      : base(service)
    {
    }

    [HttpGet]
    [Route("search")]
    public async Task<ActionResult<IEnumerable<GeneralSearchResult>>> Get(
      [FromQuery] TermSearch termSearch )
    {
      IEnumerable<Business.Models.IdResultText> businessModels = 
        await Service.SearchAsync(termSearch.Term);
        
      if (businessModels.Any())
      {
        IEnumerable<ViewModels.IdResultText> viewModels = 
          businessModels.Select(ViewModels.IdResultText.ProjectFromModel).ToList();

        return Ok(viewModels);
      }
      else
      {
        return NoContent();
      }      
    }
    private GpPracticeService Service { get { return _service as GpPracticeService; } }
  }
}