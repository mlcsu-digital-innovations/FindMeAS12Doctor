import { AuthorizationGuard } from './authorization.guard';
import { AutoLoginComponent } from './components/auto-login/auto-login.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { Routes } from '@angular/router';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized.component';
import { WelcomeComponent } from './components/welcome/welcome.component';

export const AppRoutes: Routes = [
  {
    path: '',
    component: WelcomeComponent,
    pathMatch: 'full'
  },
  {
    path: 'autologin',
    component: AutoLoginComponent
  },
  {
    path: 'unauthorized',
    component: UnauthorizedComponent
  },
  {
    path: 'welcome',
    component: WelcomeComponent,
    canActivate: [AuthorizationGuard]
  },
  {
    path: '**',
    component: PageNotFoundComponent
  }
];