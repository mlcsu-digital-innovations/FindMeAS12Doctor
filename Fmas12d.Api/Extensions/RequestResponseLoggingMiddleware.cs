using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Fmas12d.Business.Models;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Http;

namespace Fmas12d.Api.Extensions
{
  public class RequestResponseLoggingMiddleware
  {
    public const string ACTION_CONTROLLER_LOG_GET_REQUEST_RESPONSE = "GetRequestResponse";
    public const string CONTROLLER_LOG = "Log";
    private readonly RequestDelegate _next;
    public RequestResponseLoggingMiddleware(
      RequestDelegate next
    )
    {
      _next = next;
    }

    public async Task Invoke(
      HttpContext context,
      IRequestResponseLogService requestResponseLogService,
      IUserClaimsService userClaimsService
    )
    {
      context.Request.RouteValues.TryGetValue("action", out object action);
      context.Request.RouteValues.TryGetValue("controller", out object controller);

      string request = await FormatRequest(context.Request);
      DateTimeOffset requestAt = DateTimeOffset.Now;

      await _next(context);

      IRequestResponseLog requestResponseLog = new RequestResponseLog()
      {
        Action = action == null ? "Unknown" : action.ToString(),
        Controller = controller == null ? "Unknown" : controller.ToString(),
        Request = request,
        RequestAt = requestAt,
        Response = $"{context.Response.StatusCode}",
        ResponseAt = DateTimeOffset.Now,
        UserId = userClaimsService.GetUserId()
      };

      await requestResponseLogService.CreateAsync(requestResponseLog);
    }

    private async Task<string> FormatRequest(HttpRequest request)
    {
      request.EnableBuffering();
      byte[] buffer = new byte[Convert.ToInt32(request.ContentLength)];

      await request.Body.ReadAsync(buffer, 0, buffer.Length);

      string bodyAsText = Encoding.UTF8.GetString(buffer);

      request.Body.Seek(0, SeekOrigin.Begin);

      return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
    }
  }
}