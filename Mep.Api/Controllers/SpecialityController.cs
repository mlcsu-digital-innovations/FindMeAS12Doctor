using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class SpecialityController : ControllerBase
    {
        private readonly ISpecialityService _specialityService;
        private readonly IMapper _mapper;

        public SpecialityController(ISpecialityService specialityService, IMapper mapper)
        {
            _specialityService = specialityService;
            _mapper = mapper;
        }

        // DELETE api/speciality/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _specialityService.DeactivateAsync(id);
            return Ok();
        }

        // GET api/specialitys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewModels.Speciality>>> Get()
        {
            IEnumerable<BusinessModels.Speciality> specialityBusinessModels = 
                await _specialityService.GetSpecialitiesAsync(true);
            IEnumerable<ViewModels.Speciality> specialityViewModels = 
                _mapper.Map<IEnumerable<ViewModels.Speciality>>(specialityBusinessModels);
            return Ok(specialityViewModels);
        }

        // GET api/specialitys/GUID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ViewModels.Speciality>> Get(int id)
        {
            BusinessModels.Speciality specialityBusinessModel = await _specialityService.GetModelByIdAsync(id, true);
            ViewModels.Speciality specialityViewModel = _mapper.Map<ViewModels.Speciality>(specialityBusinessModel);
            return Ok(specialityViewModel);
        }

        // PATCH api/specialitys/5
        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id)
        {
            await _specialityService.UndeleteSpecialityAsync(id);
            return Ok();
        }   

        // POST api/specialitys
        [HttpPost]
        public async Task<ActionResult<ViewModels.Speciality>> Post([FromBody] RequestModels.PostSpeciality postModel)
        {
            if (ModelState.IsValid)
            {
                BusinessModels.Speciality businessModel = _mapper.Map<BusinessModels.Speciality>(postModel);
                businessModel = await _specialityService.CreateSpecialityAsync(businessModel);
                ViewModels.Speciality specialityViewModel = _mapper.Map<ViewModels.Speciality>(businessModel);
                //TODO: Change reposonce to 201 Created
                return Ok(specialityViewModel);
            }
            else
            {
                return BadRequest(ModelState);
            }            
        }

        // PUT api/specialitys/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ViewModels.Speciality>> Put(int id, [FromBody] RequestModels.PutSpeciality specialityRequestModel)
        {
            if (ModelState.IsValid)
            {
                BusinessModels.Speciality specialityBusinessModel = _mapper.Map<BusinessModels.Speciality>(specialityRequestModel);
                specialityBusinessModel.Id = id;
                specialityBusinessModel = await _specialityService.UpdateSpecialityAsync(specialityBusinessModel);
                ViewModels.Speciality specialityViewModel = _mapper.Map<ViewModels.Speciality>(specialityBusinessModel);
                return Ok(specialityViewModel);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }           
    }
}