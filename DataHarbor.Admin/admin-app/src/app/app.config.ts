import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import * as api from '../api/api';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { provideKeycloakAngular } from '../authentication/keycloak.config';
import { authInterceptor } from '../authentication/http-auth-interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withInterceptors([authInterceptor])),
    provideKeycloakAngular(),
    api.ConfigurationService,
    api.DeclarationService,
    api.ProcessService
  ]
};
