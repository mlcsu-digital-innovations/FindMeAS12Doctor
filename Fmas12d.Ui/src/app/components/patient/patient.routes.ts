import { PatientEditComponent } from './patient-edit/patient-edit.component';
import { Routes } from '@angular/router';
import { AuthorizationGuard } from 'src/app/authorization.guard';

export const PatientRoutes: Routes = [
  {
    path: 'patient',
    pathMatch: 'full',
    redirectTo: 'patient/list'
  },
  {
    path: 'patient/edit/:patientId',
    component: PatientEditComponent,
    canActivate: [AuthorizationGuard] 
  }
];
