import { AuthorizationGuard } from './authorization.guard';
import { AutoLoginComponent } from './components/auto-login/auto-login.component';
import { OnCallDoctorListComponent } 
  from './components/on-call-doctor/on-call-doctor-list/on-call-doctor-list.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { Routes } from '@angular/router';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
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
    path: 'user/profile',
    component: UserProfileComponent
  },
  {
    path: 'user/oncall',
    component: OnCallDoctorListComponent
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
