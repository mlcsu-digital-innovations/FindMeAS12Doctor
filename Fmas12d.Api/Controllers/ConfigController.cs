using Fmas12d.Api;
using Fmas12d.Api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Mep.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class ConfigController : ControllerBase
  {

    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;
    private OidcWellKnown _wellKnown;
    private JwtKs _jwtKs;    

    public ConfigController(IConfiguration config, IWebHostEnvironment env)
    {
      _env = env;
      _configuration = config;
    }

    private async Task<OidcWellKnown> GetWellKnownAsync()
    {
      if (_wellKnown == null)
      {
        using HttpClient client = new HttpClient
        {
          BaseAddress = new Uri(_configuration["oidc:issuer"])
        };
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = await client.GetAsync(".well-known/openid-configuration");
        if (response.IsSuccessStatusCode)
        {
          _wellKnown = JsonConvert.DeserializeObject<OidcWellKnown>(
            await response.Content.ReadAsStringAsync());
        }
      }
      return _wellKnown;
    }

    private async Task<JwtKs> GetJwtKs()
    {
      if (_jwtKs == null)
      {
        using HttpClient client = new HttpClient();
        OidcWellKnown wellKnown = await GetWellKnownAsync();
        client.BaseAddress = new Uri(wellKnown.Jwks_uri);
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = await client.GetAsync("");
        if (response.IsSuccessStatusCode)
        {
          _jwtKs = JsonConvert.DeserializeObject<JwtKs>(
            await response.Content.ReadAsStringAsync());
        }
      }

      return _jwtKs;
    }


    [HttpGet(".well-known/openid-configuration")]
    public async Task<ActionResult<OidcWellKnown>> WellKnownAsync()
    {
      string protocol = Request.IsHttps ? "https://" : "http://";

      string apiPath = "";
      if (_env.IsEnvironment(Startup.ENV_AIMES_UAT))
      {
        apiPath = "/api";
      }

      string jwks_uri = $"{protocol}{Request.Host.ToUriComponent()}{apiPath}/config/discovery/keys";
      var wellKnown = await GetWellKnownAsync();
      wellKnown.Jwks_uri = jwks_uri;
      return wellKnown;
    }

    [HttpGet("discovery/keys")]
    public async Task<ActionResult<JwtKs>> KeysAsync()
    {
      return await GetJwtKs();
    }

    [HttpGet("configuration")]
    public ActionResult<OIDCConfig> ConfigurationAsync()
    {
      OIDCConfig config = new OIDCConfig();
      
      string apiPath = "";
      if (_env.IsEnvironment(Startup.ENV_AIMES_UAT))
      {
        apiPath = "/api";
      }

      string protocol = Request.IsHttps ? "https://" : "http://";
      config.StsServer = $"{protocol}{Request.Host.ToUriComponent()}{apiPath}/config";
      config.Env = _env.EnvironmentName;

      return config;
    }
  }
}