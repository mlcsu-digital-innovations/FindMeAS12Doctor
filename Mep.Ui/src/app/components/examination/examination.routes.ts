import { ExaminationCreateComponent } from './examination-create/examination-create.component';
import { ExaminationEditComponent } from './examination-edit/examination-edit.component';
import { ExaminationListComponent } from './examination-list/examination-list.component';
import { ExaminationViewComponent } from './examination-view/examination-view.component';
import { Routes } from '@angular/router';
import { AuthorizationGuard } from 'src/app/authorization.guard';

export const ExaminationRoutes: Routes = [
  {
    path: 'examination',
    pathMatch: 'full',
    redirectTo: 'examination/list'
  },
  {
    path: 'examination/edit/:referralId',
    component: ExaminationEditComponent,
    canActivate: [AuthorizationGuard] 
  },
  {
    path: 'examination/list',
    component: ExaminationListComponent,
    canActivate: [AuthorizationGuard] 
  },
  {
    path: 'examination/view/:referralId',
    component: ExaminationViewComponent,
    canActivate: [AuthorizationGuard] 
  },
  {
    path: 'examination/new/:referralId',
    component: ExaminationCreateComponent,
    canActivate: [AuthorizationGuard] 
  }
];
