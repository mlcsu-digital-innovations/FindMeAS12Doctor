import { Routes } from '@angular/router';
import { DoctorSelectComponent } from './doctor-select/doctor-select.component';
import { DoctorAcceptComponent } from './doctor-accept/doctor-accept.component';

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
