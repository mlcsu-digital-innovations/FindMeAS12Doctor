import { AuthorizationGuard } from 'src/app/authorization.guard';
import { DoctorAllocateComponent } from './doctor-allocate/doctor-allocate.component';
import { DoctorSelectComponent } from './doctor-select/doctor-select.component';
import { Routes } from '@angular/router';

export const DoctorRoutes: Routes = [
  {
    path: 'assessment/:assessmentId/select-doctors',
    component: DoctorSelectComponent,
    canActivate: [AuthorizationGuard]
  },
  {
    path: 'assessment/:assessmentId/allocate-doctors',
    component: DoctorAllocateComponent,
    canActivate: [AuthorizationGuard]
  }
];
