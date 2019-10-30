import { Routes } from '@angular/router';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { WelcomeComponent } from './components/welcome/welcome.component';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized.component';

export const AppRoutes: Routes = [
  {
    path: '',
    component: WelcomeComponent
  },
  {
    path: 'unauthorized',
    component: UnauthorizedComponent
  },  
  {
    path: '**',
    component: PageNotFoundComponent
  }
];
