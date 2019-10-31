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

    private IConfiguration _configuration;
    private IWebHostEnvironment _env;

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
        var client = new HttpClient();
        client.BaseAddress = new Uri(_configuration["oidc:issuer"]);
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
        var client = new HttpClient();
        var wellKnown = await GetWellKnownAsync();
        client.BaseAddress = new Uri(wellKnown.jwks_uri);
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
    public async Task<ActionResult<OIDCConfig>> ConfigurationAsync()
    {
      OIDCConfig config = new OIDCConfig();
      OidcWellKnown wellKnown = await GetWellKnownAsync();

      string protocol = Request.IsHttps ? "https://" : "http://";
      config.stsServer = $"{protocol}{Request.Host.ToUriComponent()}/api/config";
      //config.redirect_url = $"{protocol}{Request.Host.ToUriComponent()}/";
      config.redirect_url = "http://localhost:4200/";
      config.client_id = _configuration["oidc:client_id"];
      config.response_type = "id_token token";
      if (!String.IsNullOrEmpty(_configuration["oidc:scope"]))
      {
        config.scope = _configuration["oidc:scope"];
      }
      else
      {
        config.scope = "openid profile email https://graph.microsoft.com/User.Read";
      }
      //config.post_logout_redirect_uri = $"{protocol}{Request.Host.ToUriComponent()}/";
      config.post_logout_redirect_uri = "http://localhost:4200/";
      config.post_login_route = "/";
      config.forbidden_route = "/welcome";
      config.unauthorized_route = "/welcome";
      config.auto_userinfo = true;
      config.log_console_warning_active = true;
      config.log_console_debug_active = _env.IsDevelopment();
      config.max_id_token_iat_offset_allowed_in_seconds = 1000;
      if (!String.IsNullOrEmpty(_configuration["oidc:resource"]))
      {
        config.additional_login_parameters["resource"] = _configuration["oidc:resource"];
      }
      config.additional_login_parameters["response_mode"] = "fragment";

      return config;
    }
  }
}