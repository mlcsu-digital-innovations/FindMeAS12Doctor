using Microsoft.AspNetCore.Http;
using System;

namespace Fmas12d.Business.Models
{
  public class AppClaimsPrincipal : IAppClaimsPrincipal
  {
    public const string CLAIM_USERID ="userId";
    private readonly IHttpContextAccessor _httpContextAccessor;    

    public AppClaimsPrincipal(
      IHttpContextAccessor httpContextAccessor
    )
    {
      _httpContextAccessor = httpContextAccessor;
    }
    public int GetUserId()
    {
      string userIdString = _httpContextAccessor
        ?.HttpContext
        ?.User
        .FindFirst(c => c.Type == CLAIM_USERID)
        ?.Value;      

      if (userIdString == null)
      {
        throw new Exception("Unable to find userId claim");
      }

      if (int.TryParse(userIdString, out int userId))
      {
        return userId;
      }
      else
      {
        throw new Exception($"Invalid userId claim {userId}");
      }
    }
  }
}