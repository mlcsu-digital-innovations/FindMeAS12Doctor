using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Mep.Api.Models;
using Microsoft.AspNetCore.Authorization;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ConfigController : ControllerBase
  {

    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;
    private OidcWellKnown _wellKnown;
    private JwtKs _jwtKs;

    public ConfigController(IConfiguration config, IWebHostEnvironment env)
    {
      _configuration = config;
      _env = env;
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
        client.BaseAddress = new Uri(wellKnown.jwks_uri);
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
      string jwks_uri = $"{protocol}{Request.Host.ToUriComponent()}/api/config/discovery/keys";
      var wellKnown = await GetWellKnownAsync();
      wellKnown.jwks_uri = jwks_uri;
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

      string protocol = Request.IsHttps ? "https://" : "http://";
      config.stsServer = $"{protocol}{Request.Host.ToUriComponent()}/api/config";

      return config;
    }
  }
}