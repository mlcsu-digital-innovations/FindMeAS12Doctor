import { Routes } from '@angular/router';
import { DoctorSelectComponent } from './doctor-select/doctor-select.component';
import { DoctorAcceptComponent } from './doctor-accept/doctor-accept.component';

export const DoctorRoutes: Routes = [
  {
    path: 'examination/:examinationId/select-doctors',
    component: DoctorSelectComponent
  },
  {
    path: 'examination/:examinationId/accept-doctors',
    component: DoctorAcceptComponent
  }
];
