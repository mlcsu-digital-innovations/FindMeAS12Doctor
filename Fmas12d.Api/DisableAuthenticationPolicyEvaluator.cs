using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace Fmas12d.Api
{
  public class DisableAuthenticationPolicyEvaluator : IPolicyEvaluator
  {
    public async Task<AuthenticateResult> AuthenticateAsync(
      AuthorizationPolicy policy, 
      HttpContext context)
    {
      var authenticationTicket = new AuthenticationTicket(
        new ClaimsPrincipal(), 
        new AuthenticationProperties(), 
        JwtBearerDefaults.AuthenticationScheme);

      return await Task.FromResult(AuthenticateResult.Success(authenticationTicket));
    }

    public async Task<PolicyAuthorizationResult> AuthorizeAsync(
      AuthorizationPolicy policy, 
      AuthenticateResult authenticationResult, 
      HttpContext context, 
      object resource)
    {
      return await Task.FromResult(PolicyAuthorizationResult.Success());
    }
  }
}