import { PatientEditComponent } from './patient-edit/patient-edit.component';
import { Routes } from '@angular/router';

export const PatientRoutes: Routes = [
  {
    path: 'patient',
    pathMatch: 'full',
    redirectTo: 'patient/list'
  },
  {
    path: 'patient/edit/:patientId',
    component: PatientEditComponent
  }
];
