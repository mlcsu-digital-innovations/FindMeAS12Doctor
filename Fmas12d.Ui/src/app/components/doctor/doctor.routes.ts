import { Routes } from '@angular/router';
import { DoctorSelectComponent } from './doctor-select/doctor-select.component';
import { DoctorAcceptComponent } from './doctor-accept/doctor-accept.component';
import { AuthorizationGuard } from 'src/app/authorization.guard';

export const DoctorRoutes: Routes = [
  {
    path: 'assessment/:assessmentId/select-doctors',
    component: DoctorSelectComponent
  },
  {
    path: 'assessment/:assessmentId/accept-doctors',
    component: DoctorAcceptComponent
  }
];
