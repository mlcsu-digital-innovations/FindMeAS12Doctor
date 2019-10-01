import { Routes } from '@angular/router';
import { ReferralCreateComponent } from './referral-create/referral-create.component';
import { ReferralEditComponent } from './referral-edit/referral-edit.component';
import { ReferralListComponent } from './referral-list/referral-list.component';

export const ReferralRoutes: Routes = [
  {
    path: 'referral',
    pathMatch: 'full',
    redirectTo: 'referral/list'
  },
  {
    path: 'referral/list',
    component: ReferralListComponent
  },
  {
    path: 'referral/new',
    component: ReferralCreateComponent
  },
  {
    path: 'referral/edit/:id',
    component: ReferralEditComponent
  }
];
