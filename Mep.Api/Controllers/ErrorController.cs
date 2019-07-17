using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mep.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
        private ILogger _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

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
                
                _logger.LogError(exceptionThatOccurred, routeWhereExceptionOccurred);

                return StatusCode(StatusCodes.Status500InternalServerError, exceptionThatOccurred.Message);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }            
        }
    }
}