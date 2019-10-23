import { ExaminationCreateComponent } from './examination-create/examination-create.component';
import { ExaminationEditComponent } from './examination-edit/examination-edit.component';
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
    path: 'examination/edit/:examinationId',
    component: ExaminationEditComponent
  },
  {
    path: 'examination/list',
    component: ExaminationListComponent
  },
  {
    path: 'examination/view/:referralId',
    component: ExaminationViewComponent
  },
  {
    path: 'examination/new/:referralId',
    component: ExaminationCreateComponent
  }
];
