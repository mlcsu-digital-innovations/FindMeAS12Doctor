export const environment = {
  production: true,
  apiEndpoint: 'https://fmas12d-api-dev.azurewebsites.net',
  redirectUri: 'http://localhost:8100/home',
  postLogoutRedirectUrl: 'http://localhost:8100/home'
};

export const ProtectedResourceMap: [string, string[]][] = 
[  
  [ "https://localhost:5001", ["User.Read", "offline_access"] ] 
];