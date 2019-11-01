using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Fmas12d.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ErrorController : ControllerBase
  {
    public ErrorController()
    { }

    [Route("")]
    [AllowAnonymous]
    public IActionResult Get()
    {
      // TODO: Once specific exceptions are create add them in here
      // Get the details of the exception that occurred
      var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

      if (exceptionFeature != null)
      {
        // Get which route the exception occurred at
        string routeWhereExceptionOccurred = exceptionFeature.Path;

        // Get the exception that occurred
        Exception exceptionThatOccurred = exceptionFeature.Error;

        Log.Error(
          exceptionThatOccurred,
          exceptionThatOccurred.Message,
          routeWhereExceptionOccurred);

        return StatusCodeAndErrorMessage(exceptionThatOccurred);

      }
      else
      {
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    private IActionResult StatusCodeAndErrorMessage(Exception exceptionThatOccurred)
    {
      if (exceptionThatOccurred is Business.Exceptions.MissingSearchParameterException) {
        return StatusCode(StatusCodes.Status400BadRequest, exceptionThatOccurred.Message);
      }
      else if (exceptionThatOccurred is Business.Exceptions.ExaminationAlreadyHasOutcomeException) {
        return StatusCode(StatusCodes.Status409Conflict, exceptionThatOccurred.Message);
      }      
      else if (exceptionThatOccurred is Business.Exceptions.EntityNotFoundException) {
        return StatusCode(StatusCodes.Status404NotFound, exceptionThatOccurred.Message);
      }      
      else {
        return StatusCode(StatusCodes.Status500InternalServerError, exceptionThatOccurred.Message);
      }
    }
  }
}