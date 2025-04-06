import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginFormComponent, ResetPasswordFormComponent, CreateAccountFormComponent, ChangePasswordFormComponent } from './shared/components';
import { AuthGuardService } from './shared/services';
import { HomeComponent } from './pages/home/home.component';
import { TasksComponent } from './pages/tasks/tasks.component';
import { DxDataGridModule, DxFormModule, DxNumberBoxModule, DxPopupModule, DxSelectBoxModule, DxTextAreaModule, DxTextBoxModule, DxToastModule } from 'devextreme-angular';
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

const routes: Routes = [
  {
    path: 'pages/account/:id',
    component: AccountComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'pages/accounts',
    component: AccountsComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'pages/declaration/:id',
    component: DeclarationComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'pages/configuration/:id',
    component: ConfigurationComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'pages/insights',
    component: InsightsComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'pages/declarations',
    component: DeclarationsComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'pages/configurations',
    component: ConfigurationsComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'tasks',
    component: TasksComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'login-form',
    component: LoginFormComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'reset-password',
    component: ResetPasswordFormComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'create-account',
    component: CreateAccountFormComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'change-password/:recoveryCode',
    component: ChangePasswordFormComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: '**',
    redirectTo: 'home'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true }), DxDataGridModule, DxFormModule, 
    CommonModule, 
    FormsModule, DxNumberBoxModule, DxToastModule, DxPopupModule, DxTextBoxModule, DxTextAreaModule,
    DxSelectBoxModule],
  providers: [AuthGuardService],
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
