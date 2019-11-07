using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Fmas12d.Api.Controllers
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
      try
      {
        await _service.DeactivateAsync(id);
        return Ok();
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    protected string GetCreatedModelUri(int id)
    {
      return $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}" +
             $"{this.Request.PathBase.Value.ToString()}{this.Request.Path.Value}/view/{id}";
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> Patch(int id)
    {
      try
      {
        await _service.ActivateAsync(id);
        return Ok();
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
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
      foreach (string key in modelStateException.Keys)
      {
        ModelState.AddModelError(key, modelStateException.Message);
      }
      Serilog.Log.Warning(
                  "Bad Request {ActionName}: {ModelStateErrors}",
                  ControllerContext.ActionDescriptor.ActionName,
                  ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)));

      return ValidationProblem(ModelState);
    }
  }
}