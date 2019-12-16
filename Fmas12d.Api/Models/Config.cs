using System.Collections.Generic;

namespace Fmas12d.Api.Models
{

  public class OIDCConfig
  {

    public OIDCConfig() { _additionalLoginParameters = new Dictionary<string, string>(); }

    public string StsServer { get; set; }
    public string Redirect_url { get; set; }
    public string Client_id { get; set; }
    public string Response_type { get; set; }
    public string Scope { get; set; }
    public string Post_logout_redirect_uri { get; set; }
    public string Post_login_route { get; set; }
    public bool Start_checksession { get; set; }
    public bool Silent_renew { get; set; }
    public string Silent_renew_url { get; set; }
    public string Startup_route { get; set; }
    public string Forbidden_route { get; set; }
    public string Unauthorized_route { get; set; }
    public bool Auto_userinfo { get; set; }
    public bool Log_console_warning_active { get; set; }
    public bool Log_console_debug_active { get; set; }
    public int Max_id_token_iat_offset_allowed_in_seconds { get; set; }

    private readonly Dictionary<string, string> _additionalLoginParameters;
    public Dictionary<string, string> Additional_login_parameters => _additionalLoginParameters;
  }

  public class OidcWellKnown
  {
    public string Authorization_endpoint { get; set; }
    public string Token_endpoint { get; set; }
    public List<string> Token_endpoint_auth_methods_supported { get; set; }
    public string Jwks_uri { get; set; }
    public List<string> Response_modes_supported { get; set; }
    public List<string> Subject_types_supported { get; set; }
    public List<string> Id_token_signing_alg_values_supported { get; set; }
    public bool Http_logout_supported { get; set; }
    public bool Frontchannel_logout_supported { get; set; }
    public string End_session_endpoint { get; set; }
    public List<string> Response_types_supported { get; set; }
    public List<string> Scopes_supported { get; set; }
    public string Issuer { get; set; }
    public List<string> Claims_supported { get; set; }
    public bool Request_uri_parameter_supported { get; set; }
    public string Tenant_region_scope { get; set; }
    public string Cloud_instance_name { get; set; }
    public string Cloud_graph_host_name { get; set; }
    public string Msgraph_host { get; set; }
    public string Rbac_url { get; set; }
  }

  public class JwtKey
  {
    public string Kty { get; set; }
    public string Use { get; set; }
    public string Kid { get; set; }
    public string X5t { get; set; }
    public string N { get; set; }
    public string E { get; set; }
    public List<string> X5c { get; set; }
    public string Issuer { get; set; }
  }

  public class JwtKs
  {
    public List<JwtKey> Keys { get; set; }
  }
}