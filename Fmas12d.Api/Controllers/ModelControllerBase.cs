using Fmas12d.Business.Models;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Fmas12d.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  [Authorize(Policy = "User")]

  public abstract class ModelControllerBase : ControllerBase
  {
    protected readonly IUserClaimsService _userClaimsService;
    protected readonly IServiceBase _service;
    protected ModelControllerBase(
        IUserClaimsService userClaimsService,
        IServiceBase service
    )
    {
      _userClaimsService = userClaimsService;
      _service = service;
    }

    protected string GetCreatedModelUri(int id)
    {
      return $"{this.Request.Scheme}://{this.Request.Host.Value}" +
             $"{this.Request.PathBase.Value}{this.Request.Path.Value}/view/{id}";
    } 

    protected int GetUserId()
    {
      return _userClaimsService.GetUserId();
    }

    protected ActionResult ProcessException(Exception exception)
    {
      if (exception is Business.Exceptions.ModelStateException)
      {
        return ProcessModelStateException(exception as Business.Exceptions.ModelStateException);
      }
      if (exception is Business.Exceptions.MissingSearchParameterException)
      {
        Serilog.Log.Information(exception, exception.Message);
        return StatusCode(StatusCodes.Status400BadRequest, exception.Message);
      }
      if (exception is Business.Exceptions.EntityNotFoundException)
      {
        Serilog.Log.Warning(exception, exception.Message);
        return StatusCode(StatusCodes.Status404NotFound, exception.Message);
      }
      if (exception is UnauthorizedAccessException)
      {
        Serilog.Log.Information(exception, exception.Message);
        return StatusCode(StatusCodes.Status403Forbidden, exception.Message);
      }      
      if (exception is Business.Exceptions.SerilogException serilogEx)
      {
        Serilog.Log.Error(serilogEx, serilogEx.MessageTemplate, serilogEx.PropertyValues);
        if (exception is Business.Exceptions.AssessmentAlreadyHasOutcomeException ex)
        {
          return StatusCode(StatusCodes.Status409Conflict, ex.Message);
        }
        else
        {
          return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);    
        }
      }
      
      Serilog.Log.Error(exception, exception.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
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