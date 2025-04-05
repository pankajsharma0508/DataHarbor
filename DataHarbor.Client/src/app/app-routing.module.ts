import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginFormComponent, ResetPasswordFormComponent, CreateAccountFormComponent, ChangePasswordFormComponent } from './shared/components';
import { AuthGuardService } from './shared/services';
import { HomeComponent } from './pages/home/home.component';
import { TasksComponent } from './pages/tasks/tasks.component';
import { DxDataGridModule, DxFormModule } from 'devextreme-angular';
import { ConfigurationsComponent } from './pages/configurations/configurations.component';
import { DeclarationsComponent } from './pages/declarations/declarations.component';
import { InsightsComponent } from './pages/insights/insights.component';

const routes: Routes = [
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
  imports: [RouterModule.forRoot(routes, { useHash: true }), DxDataGridModule, DxFormModule],
  providers: [AuthGuardService],
  exports: [RouterModule],
  declarations: [
    HomeComponent,
    TasksComponent,
    ConfigurationsComponent,
    DeclarationsComponent,
    InsightsComponent,
  ]
})
export class AppRoutingModule { }
