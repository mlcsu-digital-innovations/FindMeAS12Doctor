export const environment = {
  production: true,
  apiEndpoint: 'https://findmeans12doctor.co.uk/api',
  redirectUri: 'http://localhost:8100/home',
  postLogoutRedirectUrl: 'http://localhost:8100/home'
};

export const ProtectedResourceMap: [string, string[]][] = 
[  
  [ "https://localhost:5001", ["User.Read", "offline_access"] ] 
];