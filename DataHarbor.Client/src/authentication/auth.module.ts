import { APP_INITIALIZER, makeEnvironmentProviders } from '@angular/core';
import { KeycloakService } from './keycloak.service';

export const provideAuth = () => {
  const keycloak = new KeycloakService();

  return makeEnvironmentProviders([
    { provide: KeycloakService, useValue: keycloak },
    {
      provide: APP_INITIALIZER,
      multi: true,
      useFactory: () => () => keycloak.init(),
    },
  ]);
};