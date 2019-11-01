using System;
using System.Linq;
using System.Threading.Tasks;
using Mep.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public abstract class ModelControllerNoAutoMapper : ControllerBase
  {
    protected readonly IServiceBaseNoAutoMapper _service;

    protected ModelControllerNoAutoMapper(
        IServiceBaseNoAutoMapper service
    )
    {
      _service = service;
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
      await _service.DeactivateAsync(id);
      return Ok();
    }

    protected string GetCreatedModelUri(int id)
    {
      return $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}" +
             $"{this.Request.PathBase.Value.ToString()}{this.Request.Path.Value}/{id}";
    }    

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> Patch(int id)
    {
      await _service.ActivateAsync(id);
      return Ok();
    }

    protected ActionResult ProcessException(Exception exception)
    {
      if (exception is Business.Exceptions.SerilogException serilogEx)
      {
        Log.Error(serilogEx, serilogEx.MessageTemplate, serilogEx.PropertyValues);
        if (exception is Business.Exceptions.ExaminationAlreadyHasOutcomeException ex)
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
  }
}