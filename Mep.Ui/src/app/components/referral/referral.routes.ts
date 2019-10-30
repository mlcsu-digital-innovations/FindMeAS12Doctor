import { Routes } from '@angular/router';
import { ReferralCreateComponent } from './referral-create/referral-create.component';
import { ReferralEditComponent } from './referral-edit/referral-edit.component';
import { ReferralListComponent } from './referral-list/referral-list.component';
import { AuthorizationGuard } from 'src/app/authorization.guard';

export const ReferralRoutes: Routes = [
  {
    path: 'referral',
    pathMatch: 'full',
    redirectTo: 'referral/list'
  },
  {
    path: 'referral/list',
    component: ReferralListComponent,
    canActivate: [AuthorizationGuard] 
  },
  {
    path: 'referral/new',
    component: ReferralCreateComponent,
    canActivate: [AuthorizationGuard] 
  },
  {
    path: 'referral/edit/:referralId',
    component: ReferralEditComponent,
    canActivate: [AuthorizationGuard] 
  }
];
