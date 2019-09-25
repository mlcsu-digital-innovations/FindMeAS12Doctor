import { Routes } from '@angular/router';
import { ExaminationListComponent } from './examination-list/examination-list.component';

export const ExaminationRoutes: Routes = [
  {
    path: 'examination',
    pathMatch: 'full',
    redirectTo: 'examination/list'
  },
  {
    path: 'examination/list',
    component: ExaminationListComponent
  }
];
