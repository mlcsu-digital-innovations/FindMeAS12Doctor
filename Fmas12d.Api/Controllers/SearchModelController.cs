using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Mvc;


namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public abstract class SearchModelController<BusinessModel,
                                        ViewModel,
                                        PutRequestModel,
                                        PostRequestModel,
                                        ApiSearchModel,
                                        BusinessSearchModel,
                                        RequestedViewModel>
      : ModelController<BusinessModel,
                        ViewModel,
                        PutRequestModel,
                        PostRequestModel> 
                       where BusinessModel : Business.Models.BaseModel
                       where ViewModel : class
                       where PutRequestModel : class
                       where PostRequestModel : class
                       where ApiSearchModel : class
                       where BusinessSearchModel : Business.Models.SearchModels.BaseSearchModel
                       where RequestedViewModel : class
  {
    new protected readonly IModelSearchService<BusinessModel, BusinessSearchModel> _service;

    protected SearchModelController(IModelSearchService<BusinessModel, BusinessSearchModel> service, IMapper mapper) : base(service, mapper) 
    {
      _service = service;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<ViewModel>> Search([FromBody] ApiSearchModel searchModel)
    {
      if (ModelState.IsValid)
      {
        BusinessSearchModel businessSearchModel = _mapper.Map<BusinessSearchModel>(searchModel);
        IEnumerable<BusinessModel> results = await _service.SearchAsync(businessSearchModel);

        IEnumerable<RequestedViewModel> viewModels = _mapper.Map<IEnumerable<RequestedViewModel>>(results);
        return Ok(viewModels);
      }
      else
      {
        return BadRequest(ModelState);
      }
    }
  }
}