export const OAuthSettings = {
  appId: '9a667831-799d-4a8a-bce2-c168424cdabe',
  authority: 'https://login.microsoftonline.com/common',
  clientSecret: 'PG1Tx/8O:HvN]4NShi2UdYJEMuiO=Up4',
  consentScopes: [ 'api://9a667831-799d-4a8a-bce2-c168424cdabe/Fmas12d.Scope'],
  redirectUrl: 'msauth://uk.nhs.fmas12d/IkVJg6dEM%2Fdu%2Bl%2Bg8RfIw9UZJAg%3D',
  scopes: [
    'api://9a667831-799d-4a8a-bce2-c168424cdabe/Fmas12d.Scope'
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

