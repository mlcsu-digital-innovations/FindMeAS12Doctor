using Mep.Api.RequestModels;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public abstract class SearchBaseController<BusinessModel> : ModelControllerNoAutoMapper
      where BusinessModel : Business.Models.BaseModel
  {
    protected SearchBaseController(ISearchService service) 
      : base(service) 
    {
    }
    
    [HttpGet]
    [Route("search")]
    public async Task<ActionResult<IEnumerable<ViewModels.IdResultText>>> Get(
      [FromQuery] TermSearch termSearch)
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

    private ISearchService Service { get { return _service as ISearchService; } }
  }
}