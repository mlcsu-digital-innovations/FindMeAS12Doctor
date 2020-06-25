using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Fmas12d.Business.Models;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.IO;

namespace Fmas12d.Api.Extensions
{
  public class RequestResponseLoggingMiddleware
  {
    public const string ACTION_CONTROLLER_LOG_GET_REQUEST_RESPONSE = "GetRequestResponse";
    public const string CONTROLLER_LOG = "Log";
    private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
    private readonly RequestDelegate _next;

    public RequestResponseLoggingMiddleware(RequestDelegate next)
    {
      _next = next;
      _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
    }

    public async Task Invoke(
      HttpContext context,
      IRequestResponseLogService requestResponseLogService,
      IUserClaimsService userClaimsService)
    {
      context.Request.RouteValues.TryGetValue("action", out object action);
      context.Request.RouteValues.TryGetValue("controller", out object controller);
      string actionName = action == null ? "Unknown" : action.ToString();

      string bodyAsText;
      if (actionName.Contains("batch", StringComparison.InvariantCultureIgnoreCase))
        bodyAsText = $"batch update";      
      else
        bodyAsText = await GetBodyAsTest(context.Request);        

      string request = 
        $"{context.Request.Scheme} {context.Request.Host}{context.Request.Path} " + 
        $"{context.Request.QueryString} {bodyAsText}";

      DateTimeOffset requestAt = DateTimeOffset.Now;

      await _next(context);

      IRequestResponseLog requestResponseLog = new RequestResponseLog()
      {
        Action = actionName,
        Controller = controller == null ? "Unknown" : controller.ToString(),
        Request = request,
        RequestAt = requestAt,
        Response = $"{context.Response.StatusCode}",
        ResponseAt = DateTimeOffset.Now,
        UserId = userClaimsService.HasUserIdClaim() ? userClaimsService.GetUserId() : 0
      };

      await requestResponseLogService.CreateAsync(requestResponseLog);
    }

    private async Task<string> GetBodyAsTest(HttpRequest request)
    {
      request.EnableBuffering();
      await using var requestStream = _recyclableMemoryStreamManager.GetStream();
      await request.Body.CopyToAsync(requestStream);
      string bodyAsText = ReadStreamInChunks(requestStream);
      request.Body.Position = 0;
      return bodyAsText;      
    }

    private string ReadStreamInChunks(MemoryStream stream)
    {
      const int readChunkBufferLength = 4096;
      stream.Seek(0, SeekOrigin.Begin);
      using var textWriter = new StringWriter();
      using var reader = new StreamReader(stream);
      var readChunk = new char[readChunkBufferLength];
      int readChunkLength;
      do
      {
        readChunkLength = reader.ReadBlock(readChunk,
                                           0,
                                           readChunkBufferLength);
        textWriter.Write(readChunk, 0, readChunkLength);
      } while (readChunkLength > 0);
      return textWriter.ToString();
    }
  }
}