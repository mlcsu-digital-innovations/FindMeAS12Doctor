import { Routes } from '@angular/router';
import { DoctorSelectComponent } from './doctor-select/doctor-select.component';
import { DoctorAcceptComponent } from './doctor-accept/doctor-accept.component';

export const DoctorRoutes: Routes = [
  {
    path: 'examination/select-doctors/:examinationId',
    component: DoctorSelectComponent
  },
  {
    path: 'examination/accept-doctors/:examinationId',
    component: DoctorAcceptComponent
  }
];
