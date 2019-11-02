import { ExaminationCreateComponent } from './assessment-create/assessment-create.component';
import { ExaminationEditComponent } from './assessment-edit/assessment-edit.component';
import { ExaminationListComponent } from './assessment-list/assessment-list.component';
import { ExaminationViewComponent } from './assessment-view/assessment-view.component';
import { Routes } from '@angular/router';

export const ExaminationRoutes: Routes = [
  {
    path: 'examination',
    pathMatch: 'full',
    redirectTo: 'examination/list'
  },
  {
    path: 'examination/edit/:referralId',
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
