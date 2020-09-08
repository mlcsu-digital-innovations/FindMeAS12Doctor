import { AuthorizationGuard } from './authorization.guard';
import { AutoLoginComponent } from './components/auto-login/auto-login.component';
import { OnCallDoctorListComponent }
  from './components/on-call-doctor/on-call-doctor-list/on-call-doctor-list.component';
import { AboutComponent } from './components/about/about.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { Routes } from '@angular/router';
import { SignOutComponent } from './components/sign-out/signout.component';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { WelcomeComponent } from './components/welcome/welcome.component';
import { RegisterComponent } from './components/register/register.component';

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
    path: 'signout',
    component: SignOutComponent
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
    path: 'about',
    component: AboutComponent
  },
  {
    path: 'welcome',
    component: WelcomeComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: '**',
    component: PageNotFoundComponent
  }
];
