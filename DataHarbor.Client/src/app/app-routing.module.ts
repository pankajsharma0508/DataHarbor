import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginFormComponent, ResetPasswordFormComponent, CreateAccountFormComponent, ChangePasswordFormComponent } from './shared/components';
import { HomeComponent } from './pages/home/home.component';
import { TasksComponent } from './pages/tasks/tasks.component';
import { DxDataGridModule, DxDropDownButtonModule, DxFormModule, DxLoadPanelModule, DxNumberBoxModule, DxPopupModule, DxSelectBoxModule, DxTextAreaModule, DxTextBoxModule, DxToastModule } from 'devextreme-angular';
import { ConfigurationsComponent } from './pages/configurations/configurations.component';
import { DeclarationsComponent } from './pages/declarations/declarations.component';
import { InsightsComponent } from './pages/insights/insights.component';
import { ConfigurationComponent } from './pages/configuration/configuration.component';
import { DeclarationComponent } from './pages/declaration/declaration.component';
import { TabsComponent } from './pages/tabs/tabs.component';
import { FileConfigurationComponent } from './pages/configuration/file-configuration/file-configuration.component';
import { MappingConfigurationComponent } from './pages/configuration/mapping-configuration/mapping-configuration.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { GeneralConfigurationComponent } from './pages/configuration/general-configuration/general-configuration.component';
import { AccountsComponent } from './pages/accounts/accounts.component';
import { AccountComponent } from './pages/account/account.component';
import { authGuard } from '../authentication/auth.guard';

const routes: Routes = [
  {
    path: 'pages/account/:id',
    component: AccountComponent,
    canActivate: [ authGuard ]
  },
  {
    path: 'pages/accounts',
    component: AccountsComponent,
    canActivate: [ authGuard ]
  },
  {
    path: 'pages/declaration/:id',
    component: DeclarationComponent,
    canActivate: [ authGuard ]
  },
  {
    path: 'pages/configuration/:id',
    component: ConfigurationComponent,
    canActivate: [ authGuard ]
  },
  {
    path: 'pages/insights',
    component: InsightsComponent,
    canActivate: [ authGuard ]
  },
  {
    path: 'pages/declarations',
    component: DeclarationsComponent,
    canActivate: [ authGuard ]
  },
  {
    path: 'pages/configurations',
    component: ConfigurationsComponent,
    canActivate: [ authGuard ]
  },
  {
    path: 'tasks',
    component: TasksComponent,
    canActivate: [ authGuard ]
  },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [ authGuard ]
  },
  {
    path: 'login-form',
    component: LoginFormComponent,
    canActivate: [ authGuard ]
  },
  {
    path: 'reset-password',
    component: ResetPasswordFormComponent,
    canActivate: [ authGuard ]
  },
  {
    path: 'create-account',
    component: CreateAccountFormComponent,
    canActivate: [ authGuard ]
  },
  {
    path: 'change-password/:recoveryCode',
    component: ChangePasswordFormComponent,
    canActivate: [ authGuard ]
  },
  {
    path: '**',
    redirectTo: 'home'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true }), DxDataGridModule, DxFormModule, 
    CommonModule, 
    FormsModule, DxNumberBoxModule, DxToastModule, DxPopupModule,
    DxTextBoxModule, DxTextAreaModule, DxDropDownButtonModule,
    DxSelectBoxModule],
  providers: [],
  exports: [RouterModule],
  declarations: [
    HomeComponent,
    TasksComponent,
    ConfigurationsComponent,
    DeclarationsComponent,
    InsightsComponent,
    ConfigurationComponent,
    TabsComponent,
    DeclarationComponent,
    FileConfigurationComponent,
    MappingConfigurationComponent,
    GeneralConfigurationComponent,
    AccountsComponent,
    AccountComponent
  ]
})
export class AppRoutingModule { }
