import {
    provideKeycloak,
    createInterceptorCondition,
    IncludeBearerTokenCondition,
    INCLUDE_BEARER_TOKEN_INTERCEPTOR_CONFIG,
    withAutoRefreshToken,
    AutoRefreshTokenService,
    UserActivityService
  } from 'keycloak-angular';
  
  const localhostCondition = createInterceptorCondition<IncludeBearerTokenCondition>({
    urlPattern: /^(http:\/\/localhost:8080)(\/.*)?$/i
  });
  
  export const provideKeycloakAngular = () =>
    provideKeycloak({
      config: {
        realm: 'DataHarbor',
        url: 'http://localhost:8080/',
        clientId: 'DataHarborClient'
      },
      initOptions: {
        onLoad: 'login-required',
        // silentCheckSsoRedirectUri: window.location.origin + '/silent-check-sso.html',
        // enableLogging: true, 
        // redirectUri: window.location.origin + '/',
        checkLoginIframe: false,
        checkLoginIframeInterval: 2500
      },
      features: [
        // withAutoRefreshToken({
        //   onInactivityTimeout: 'logout',
        //   sessionTimeout: 60000
        // })
      ],
      providers: [
        //AutoRefreshTokenService,
        UserActivityService,
        {
          provide: INCLUDE_BEARER_TOKEN_INTERCEPTOR_CONFIG,
          useValue: [localhostCondition]
        }
      ]
    });