import { ExaminationCreateComponent } from './examination-create/examination-create.component';
import { ExaminationListComponent } from './examination-list/examination-list.component';
import { ExaminationViewComponent } from './examination-view/examination-view.component';
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
  },
  {
    path: 'examination/view/:examinationId',
    component: ExaminationViewComponent
  },
  {
    path: 'examination/new/:referralId',
    component: ExaminationCreateComponent
  }
];
