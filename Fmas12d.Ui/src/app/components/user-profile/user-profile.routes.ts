import { AuthorizationGuard } from 'src/app/authorization.guard';
import { DoctorClaimListComponent } from './doctor-claims-list/doctor-claim-list.component';
// import { ClaimViewComponent } from './claim-view/claim-view.component';
import { Routes } from '@angular/router';

export const UserProfileRoutes: Routes = [
  {
    path: 'user',
    pathMatch: 'full',
    redirectTo: 'doctor/claims/list'
  },
  {
    path: 'doctor/claims/list',
    component: DoctorClaimListComponent,
    canActivate: [AuthorizationGuard]
  },
  // {
  //   path: 'user/claim/:claimId',
  //   component: ClaimViewComponent,
  //   canActivate: [AuthorizationGuard]
  // }
];
