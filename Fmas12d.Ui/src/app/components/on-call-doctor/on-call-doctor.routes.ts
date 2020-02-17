import { OnCallDoctorListComponent } from './on-call-doctor-list/on-call-doctor-list.component';
import { Routes } from '@angular/router';
import { AuthorizationGuard } from 'src/app/authorization.guard';

export const OnCallDoctorRoutes: Routes = [  
  {
    path: 'user/oncall',
    component: OnCallDoctorListComponent    
  }
];
