using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mep.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public abstract class ModelController<BusinessModel, 
                                          ViewModel, 
                                          PutRequestModel, 
                                          PostRequestModel> 
        : ControllerBase where BusinessModel : Business.Models.BaseModel
                         where ViewModel : class
                         where PutRequestModel : class
                         where PostRequestModel : class
    {
        protected readonly IModelService<BusinessModel> _service;
        protected readonly IMapper _mapper;

        public ModelController(
            IModelService<BusinessModel> service, 
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeactivateAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewModels.Speciality>>> Get()
        {
            IEnumerable<BusinessModel> businessModels = 
                await _service.GetAllAsync(true);

            IEnumerable<ViewModel> viewModels = 
                _mapper.Map<IEnumerable<ViewModel>>(businessModels);

            return Ok(viewModels);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ViewModel>> Get(int id)
        {
            BusinessModel businessModel = await _service.GetByIdAsync(id, true);
            ViewModel viewModel = _mapper.Map<ViewModel>(businessModel);
            return Ok(viewModel);
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id)
        {
            await _service.ActivateAsync(id);
            return Ok();
        }   

        [HttpPost]
        public async Task<ActionResult<ViewModel>> Post([FromBody] PostRequestModel postModel)
        {
            if (ModelState.IsValid)
            {
                BusinessModel businessModel = _mapper.Map<BusinessModel>(postModel);
                businessModel = await _service.CreateAsync(businessModel);
                ViewModel specialityViewModel = _mapper.Map<ViewModel>(businessModel);
                //TODO: Change reposonce to 201 Created
                return Ok(specialityViewModel);
            }
            else
            {
                return BadRequest(ModelState);
            }            
        }

        // PUT api/speciality/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ViewModel>> Put(int id, [FromBody] PutRequestModel requestModel)
        {
            if (ModelState.IsValid)
            {
                BusinessModel businessModel = _mapper.Map<BusinessModel>(requestModel);
                businessModel.Id = id;
                businessModel = await _service.UpdateAsync(businessModel);
                ViewModel viewModel = _mapper.Map<ViewModel>(businessModel);
                return Ok(viewModel);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }           
    }
}