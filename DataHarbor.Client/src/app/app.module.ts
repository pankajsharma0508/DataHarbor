import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { SideNavOuterToolbarModule, SideNavInnerToolbarModule, SingleCardModule } from './layouts';
import {
  FooterModule, ResetPasswordFormModule,
  CreateAccountFormModule, ChangePasswordFormModule, LoginFormModule
} from './shared/components';
import { AuthService, ScreenService, AppInfoService } from './shared/services';
import { UnauthenticatedContentModule } from './unauthenticated-content';
import { AppRoutingModule } from './app-routing.module';
import * as api from '../api/api';
import { HttpClientModule } from '@angular/common/http';
import { NotificationService } from './pages/services/notification.service';
import { KeycloakService } from 'keycloak-angular';
import { provideAuth } from '../authentication/auth.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    SideNavOuterToolbarModule,
    SideNavInnerToolbarModule,
    SingleCardModule,
    FooterModule,
    ResetPasswordFormModule,
    CreateAccountFormModule,
    ChangePasswordFormModule,
    LoginFormModule,
    UnauthenticatedContentModule,
    AppRoutingModule,
    HttpClientModule,
  ],
  providers: [
    AuthService,
    ScreenService,
    AppInfoService,
    NotificationService,
    api.ConfigurationService,
    api.DeclarationService,
    api.ProcessService,
    api.AccountsService,
    provideAuth(),
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
