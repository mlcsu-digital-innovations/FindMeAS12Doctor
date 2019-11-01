using System.Threading.Tasks;
using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using Mep.Api.RequestModels;
using Mep.Api.SearchModels;
using System.Collections.Generic;
using SearchModel = Mep.Business.Models.SearchModels;
using BusinessModel = Mep.Business.Models;
using Microsoft.AspNetCore.Authorization;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
  public class AvailableDoctorController : ControllerBase
  {
    protected readonly IMapper _mapper;
    protected readonly IModelSimpleSearchService<BusinessModel.AvailableDoctor, SearchModel.AvailableDoctorSearch> _service;

    public AvailableDoctorController(IModelSimpleSearchService<BusinessModel.AvailableDoctor, SearchModel.AvailableDoctorSearch> service, IMapper mapper)
    {
      _mapper = mapper;
      _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<AvailableDoctor>>> Post([FromBody] AvailableDoctorSearch searchModel)
    {
      if (ModelState.IsValid)
      {
        SearchModel.AvailableDoctorSearch businessModel = _mapper.Map<SearchModel.AvailableDoctorSearch>(searchModel);
        IEnumerable<Business.Models.AvailableDoctor> results = await _service.SearchAsync(businessModel);
        IEnumerable<AvailableDoctor> viewModel = _mapper.Map<IEnumerable<AvailableDoctor>>(results);

        return Ok(viewModel);
      }
      else
      {
        return BadRequest(ModelState);
      }
    }
  }
}