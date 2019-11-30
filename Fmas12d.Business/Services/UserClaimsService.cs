using Microsoft.AspNetCore.Http;
using System;

namespace Fmas12d.Business.Services
{
  public class UserClaimsService : IUserClaimsService
  {
    public const string CLAIM_USERID ="userId";
    private readonly IHttpContextAccessor _httpContextAccessor;    

    public UserClaimsService(
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