// using System.Threading.Tasks;
// using AutoMapper;
// using Fmas12d.Business.Services;
// using Microsoft.AspNetCore.Mvc;
// using Fmas12d.Api.RequestModels;
// using Fmas12d.Api.SearchModels;
// using System.Collections.Generic;
// using SearchModel = Fmas12d.Business.Models.SearchModels;
// using BusinessModel = Fmas12d.Business.Models;


// namespace Fmas12d.Api.Controllers
// {
//   [Route("api/[controller]")]
//   [ApiController]
//   public class AvailableDoctorController : ControllerBase
//   {
//     protected readonly IMapper _mapper;
//     protected readonly IModelSimpleSearchService<BusinessModel.AvailableDoctor, SearchModel.AvailableDoctorSearch> _service;

//     public AvailableDoctorController(IModelSimpleSearchService<BusinessModel.AvailableDoctor, SearchModel.AvailableDoctorSearch> service, IMapper mapper)
//     {
//       _mapper = mapper;
//       _service = service;
//     }

//     [HttpPost]
//     public async Task<ActionResult<IEnumerable<AvailableDoctor>>> Post([FromBody] AvailableDoctorSearch searchModel)
//     {
//       if (ModelState.IsValid)
//       {
//         SearchModel.AvailableDoctorSearch businessModel = _mapper.Map<SearchModel.AvailableDoctorSearch>(searchModel);
//         IEnumerable<Business.Models.AvailableDoctor> results = await _service.SearchAsync(businessModel);
//         IEnumerable<AvailableDoctor> viewModel = _mapper.Map<IEnumerable<AvailableDoctor>>(results);

//         return Ok(viewModel);
//       }
//       else
//       {
//         return BadRequest(ModelState);
//       }
//     }
//   }
// }