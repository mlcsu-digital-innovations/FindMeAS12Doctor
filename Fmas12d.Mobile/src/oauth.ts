export const OAuthSettings = {
  appId: '9a667831-799d-4a8a-bce2-c168424cdabe',
  authority: 'https://login.microsoftonline.com/f47807cf-afbc-4184-a579-8678bea3019a',
  clientSecret: 'PG1Tx/8O:HvN]4NShi2UdYJEMuiO=Up4',
  consentScopes: [ 'openid', 'profile', 'email', 'api://9a667831-799d-4a8a-bce2-c168424cdabe/Fmas12d.Scope'],
  redirectUrl: 'msal9a667831-799d-4a8a-bce2-c168424cdabe://auth',
  scopes: [
    'openid', 'profile', 'email', 'api://9a667831-799d-4a8a-bce2-c168424cdabe/Fmas12d.Scope'
  ]
};

export const OAuthSettingsMSAL = {
  scopes: [
    'api://9a667831-799d-4a8a-bce2-c168424cdabe/Fmas12d.Scope'
  ],
  authorities : [
    {
      type: 'AAD',
      audience: 'AzureADMyOrg',
      default: true,
      authority_url: 'https://login.microsoftonline.com/common',
    }
  ],
  authorizationUserAgent: 'WEBVIEW'
};

