import { Routes } from '@angular/router';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';

export const AppRoutes: Routes = [

  {
    path: '**',
    component: PageNotFoundComponent
  }

];
