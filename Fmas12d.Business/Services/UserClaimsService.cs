using Fmas12d.Business.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Fmas12d.Business.Services
{
  public class UserClaimsService : IUserClaimsService
  {
    public const string CLAIM_USERID ="userId";
    public const string CLAIM_PROFILETYPEID = "profileTypeId";
    private readonly IHttpContextAccessor _httpContextAccessor;    

    public UserClaimsService(
      IHttpContextAccessor httpContextAccessor
    )
    {
      _httpContextAccessor = httpContextAccessor;
    }
    public int GetUserId()
    {
      return GetClaimInt(CLAIM_USERID, CLAIM_USERID);
    }

    public bool HasUserIdClaim()
    {
      return _httpContextAccessor?.HttpContext?.User.HasClaim(c => c.Type == CLAIM_USERID) ?? false;
    }

    public bool IsUserAdmin()
    {
      int profileTypeId = GetClaimInt(CLAIM_PROFILETYPEID, CLAIM_PROFILETYPEID);

      return (profileTypeId == ProfileType.ADMIN || profileTypeId == ProfileType.SYSTEM);
    }

    private string GetClaim(string claimType, string claimTypeName)
    {
      string claimValue = _httpContextAccessor
        ?.HttpContext
        ?.User
        .FindFirst(c => c.Type == claimType)
        ?.Value;      

      if (claimValue == null)
      {
        throw new Exception($"Unable to find claim {claimTypeName}.");
      }

      return claimValue;
    }

    private int GetClaimInt(string claimType, string claimTypeName)
    {
      string claimValue = GetClaim(claimType, claimTypeName);
      if (int.TryParse(claimValue, out int claimValueInt))
      {
        return claimValueInt;
      }
      else
      {
        throw new Exception($"Invalid {claimTypeName} claim of {claimValue}.");
      }      
    }


  }
}