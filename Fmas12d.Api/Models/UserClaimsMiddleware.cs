using Fmas12d.Api;
using Fmas12d.Business.Migrations.Seeds;
using Fmas12d.Business.Models;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fmas12d.Models
{
  public class UserClaimsMiddleware
  {
    private const string CLAIM_AZURE_AD_IDENITIFER =
      "http://schemas.microsoft.com/identity/claims/objectidentifier";


    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _environment;

    public UserClaimsMiddleware(
      IWebHostEnvironment environment,
      RequestDelegate next)
    {
      _environment = environment;
      _next = next;
    }

    public async Task InvokeAsync(
      HttpContext httpContext,
      IMemoryCache memoryCache,
      IUserService userService)
    {
      bool isSpoofingSystemAdmin = false;
      if (httpContext.User != null)
      {
        if (!httpContext.User.Identity.IsAuthenticated &&
            (_environment.IsEnvironment(Startup.ENV_DISABLEAUTHENTICATION)))
        {
          SpoofSystemAdminUserForDevelopmentWithNoAuthentication(httpContext);
          isSpoofingSystemAdmin = true;
        }

        if (httpContext.User.Identity.IsAuthenticated ||
            isSpoofingSystemAdmin)
        {

          string identityServerIdentifier = httpContext
            .User
            .FindFirst(c => c.Type == CLAIM_AZURE_AD_IDENITIFER)
            ?.Value;

          if (identityServerIdentifier == null)
          {
            httpContext.Response.StatusCode = 400;
            await httpContext.Response.WriteAsync(
              $"Missing User Claim {CLAIM_AZURE_AD_IDENITIFER}"
            );
            return;
          }
          else
          {
            string cacheUserIdKey = $"_Claim_UserId_{identityServerIdentifier}";
            string cacheProfileTypeIdKey = $"_Claim_ProfileTypeId_{identityServerIdentifier}";

            if (!memoryCache.TryGetValue(cacheUserIdKey, out int userId) ||
                !memoryCache.TryGetValue(cacheProfileTypeIdKey, out int profileTypeId))
            {
              User user = await userService.GetByIdentityServerIdentifierAsync(
                identityServerIdentifier
              );

              if (user == null)
              {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync(
                  $"Invalid User Claim {identityServerIdentifier}"
                );
                return;
              }

              profileTypeId = user.ProfileTypeId;
              userId = user.Id;

              memoryCache.Set(cacheUserIdKey, userId, new TimeSpan(0, 30, 0));
              memoryCache.Set(cacheProfileTypeIdKey, profileTypeId, new TimeSpan(0, 30, 0));
            }

            List<Claim> userClaims = new List<Claim>
            {
              new Claim(UserClaimsService.CLAIM_PROFILETYPEID, profileTypeId.ToString()),
              new Claim(UserClaimsService.CLAIM_TYPE_USERID, userId.ToString())
            };

            ClaimsIdentity appIdentity = new ClaimsIdentity(userClaims);
            httpContext.User.AddIdentity(appIdentity);
          }
        }
      }

      await _next(httpContext);
    }

    private void SpoofSystemAdminUserForDevelopmentWithNoAuthentication(HttpContext httpContext)
    {
      List<Claim> bogusSystemAdminUserClaims = new List<Claim>
      {
        new Claim(CLAIM_AZURE_AD_IDENITIFER,
                  UserSeeder.IDENTITY_SERVER_IDENTIFIER_SYSTEM_ADMIN)
      };

      ClaimsIdentity appIdentity = new ClaimsIdentity(bogusSystemAdminUserClaims);
      httpContext.User.AddIdentity(appIdentity);
    }
  }

  public static class UserClaimsMiddlewareExtensions
  {
    public static IApplicationBuilder UseUserClaims(
        this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<UserClaimsMiddleware>();
    }
  }
}