using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Fmas12d.Api.Controllers
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

    protected ModelController(
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
    public async Task<ActionResult<IEnumerable<ViewModels.BaseViewModel>>> Get()
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
    public virtual async Task<ActionResult<ViewModel>> Post([FromBody] PostRequestModel postModel)
    {
      try
      {
        BusinessModel businessModel = _mapper.Map<BusinessModel>(postModel);
        businessModel = await _service.CreateAsync(businessModel);
        ViewModel viewModel = _mapper.Map<ViewModel>(businessModel);

        return Created(GetCreatedModelUri(businessModel.Id), viewModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    protected string GetCreatedModelUri(int id)
    {
      return $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}" +
             $"{this.Request.PathBase.Value.ToString()}{this.Request.Path.Value}/{id}";
    }

    protected ActionResult ProcessException(Exception exception)
    {
      if (exception is Business.Exceptions.SerilogException serilogEx)
      {
        Log.Error(serilogEx, serilogEx.MessageTemplate, serilogEx.PropertyValues);
        if (exception is Business.Exceptions.AssessmentAlreadyHasOutcomeException ex)
        {
          return StatusCode(StatusCodes.Status409Conflict, ex.Message);
        }
      }
      else
      {
        Log.Error(exception, exception.Message);
      }

      if (exception is Business.Exceptions.ModelStateException)
      {
        return ProcessModelStateException(exception as Business.Exceptions.ModelStateException);
      }
      else if (exception is Business.Exceptions.MissingSearchParameterException)
      {
        return StatusCode(StatusCodes.Status400BadRequest, exception.Message);
      }
      else if (exception is Business.Exceptions.EntityNotFoundException)
      {
        return StatusCode(StatusCodes.Status404NotFound, exception.Message);
      }
      else
      {
        return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
      }
    }

    protected ActionResult ProcessModelStateException(
      Business.Exceptions.ModelStateException modelStateException)
    {
      ModelState.AddModelError(modelStateException.Key, modelStateException.Message);
      Serilog.Log.Warning(
                  "Bad Request {ActionName}: {ModelStateErrors}",
                  ControllerContext.ActionDescriptor.ActionName,
                  ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)));
      return ValidationProblem(ModelState);
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