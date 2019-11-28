using System;
using System.Threading.Tasks;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Http;

namespace Fmas12d.Business.Models
{
  public class AppClaimsPrincipal : IAppClaimsPrincipal
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;

    private const string CLAIM_AZURE_AD_IDENITIFER =
      "http://schemas.microsoft.com/identity/claims/objectidentifier";

    public AppClaimsPrincipal(
      IHttpContextAccessor httpContextAccessor,
      IUserService userService
    )
    {
      _httpContextAccessor = httpContextAccessor;
      _userService = userService;
    }

    public async Task<User> GetCurrentUserAsync()
    {
      string identityServerIdentifier = _httpContextAccessor
        ?.HttpContext
        ?.User
        .FindFirst(c => c.Type == CLAIM_AZURE_AD_IDENITIFER)
        ?.Value;

      if (identityServerIdentifier == null)
      {
        throw new Exception($"Unable to find user claim {CLAIM_AZURE_AD_IDENITIFER}");
      }

      User user = await _userService.GetByIdentityServerIdentifier(identityServerIdentifier);

      if (user == null)
      {
        throw new Exception(
          "Unable to find an active user with an IdentityServerIdentifier of " +
          $"{identityServerIdentifier}");
      }

      return user;
    }
  }
}