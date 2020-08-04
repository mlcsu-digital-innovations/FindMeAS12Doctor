using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
    public class MicrosoftGraphClient : IGraphClientService
    {
      protected readonly IConfiguration _configuration;
      private static GraphServiceClient graphClient;
      private static string clientId;
      private static string clientSecret;
      private static string tenantId;
      private static string aadInstance;
      private static string graphResource;
      private static string graphAPIEndpoint;
      private static string authority;

      public MicrosoftGraphClient(
        IConfiguration config
      )
       {
          _configuration = config;
          SetAzureADOptions();
       } 

      private void SetAzureADOptions()
      {
        clientId = _configuration.GetValue("graph:ClientId", "");
        clientSecret = _configuration.GetValue("graph:ClientSecret", "");
        tenantId = _configuration.GetValue("graph:TenantId", "");
        aadInstance = _configuration.GetValue("graph:Instance", "");
        graphResource = _configuration.GetValue("graph:GraphResource", "");

        graphAPIEndpoint =
          $"{graphResource}{_configuration.GetValue("graph:GraphResourceEndPoint", "")}";
        authority = $"{aadInstance}{tenantId}";
      }

      public async Task<GraphServiceClient> GetGraphServiceClient()
      {
        // Get Access Token and Microsoft Graph Client using access token and
        // microsoft graph v1.0 endpoint
        var delegateAuthProvider = await GetAuthProvider();
        // Initializing the GraphServiceClient
        graphClient = new GraphServiceClient(graphAPIEndpoint, delegateAuthProvider);
        return graphClient;
      }
      public async Task<DelegateAuthenticationProvider> GetAuthProvider()
      {
        AuthenticationContext authenticationContext = new AuthenticationContext(authority);
        ClientCredential clientCredentials = new ClientCredential(clientId, clientSecret);
        // ADAL includes an in memory cache, so this call will only send a message to the
        // server if the cached token is expired.
        AuthenticationResult authenticationResult =
          await authenticationContext.AcquireTokenAsync(graphResource, clientCredentials);
        var token = authenticationResult.AccessToken;
        var delegateAuthProvider =
          new DelegateAuthenticationProvider((requestMessage) => {
            requestMessage.Headers.Authorization =
              new AuthenticationHeaderValue("bearer", token.ToString()); 
            return Task.FromResult(0);
          });
        return delegateAuthProvider;
      }
    }
}