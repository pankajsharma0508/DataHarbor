import { Routes } from '@angular/router';
import { ConfigurationListComponent } from './configuration-list/configuration-list.component';
import { HomeComponent } from './home/home.component';
import { DeclarationsListComponent } from './declarations-list/declarations-list.component';
import { ConfigurationComponent } from './configuration/configuration.component';
import { DeclarationComponent } from './declaration/declaration.component';
import { canActivateAuthRole } from '../authentication/auth.guard';

export const routes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'configurations', component: ConfigurationListComponent },
    { path: 'configurations/:id', component: ConfigurationComponent },
    { path: 'declarations', component: DeclarationsListComponent },
    { path: 'declarations/:id', component: DeclarationComponent },
    { path: '', redirectTo: 'home', pathMatch: 'full' }
];
