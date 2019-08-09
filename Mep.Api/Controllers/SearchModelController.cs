using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using Mep.Api.SearchModels;
using Mep.Business.Models.SearchModels;


namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public abstract class SearchModelController<BusinessModel,
                                        ViewModel,
                                        PutRequestModel,
                                        PostRequestModel,
                                        ApiSearchModel,
                                        BusinessSearchModel>
      : ModelController<BusinessModel,
                                        ViewModel,
                                        PutRequestModel,
                                        PostRequestModel> where BusinessModel : Business.Models.BaseModel
                       where ViewModel : class
                       where PutRequestModel : class
                       where PostRequestModel : class
                       where ApiSearchModel : class
                       where BusinessSearchModel : Business.Models.SearchModels.BaseSearchModel
  {

    new protected readonly IModelSearchService<BusinessModel, BusinessSearchModel> _service;

    public SearchModelController(IModelSearchService<BusinessModel, BusinessSearchModel> service, IMapper mapper) : base(service, mapper) 
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

        IEnumerable<ViewModel> viewModels = _mapper.Map<IEnumerable<ViewModel>>(results);
        return Ok(viewModels);
      }
      else
      {
        return BadRequest(ModelState);
      }
    }
  }
}