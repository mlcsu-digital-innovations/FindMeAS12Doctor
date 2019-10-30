import { Routes } from '@angular/router';
import { DoctorSelectComponent } from './doctor-select/doctor-select.component';
import { DoctorAcceptComponent } from './doctor-accept/doctor-accept.component';
import { AuthorizationGuard } from 'src/app/authorization.guard';

export const DoctorRoutes: Routes = [
  {
    path: 'examination/:examinationId/select-doctors',
    component: DoctorSelectComponent,
    canActivate: [AuthorizationGuard] 
  },
  {
    path: 'examination/:examinationId/accept-doctors',
    component: DoctorAcceptComponent,
    canActivate: [AuthorizationGuard]
  }
];
