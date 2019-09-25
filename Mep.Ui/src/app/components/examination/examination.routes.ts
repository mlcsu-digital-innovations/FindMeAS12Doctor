import { ExaminationListComponent } from './examination-list/examination-list.component';
import { Routes } from '@angular/router';

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
