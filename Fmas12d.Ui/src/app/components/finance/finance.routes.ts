import { AuthorizationGuard } from 'src/app/authorization.guard';
import { ClaimListComponent } from './claim-list/claim-list.component';
import { ClaimViewComponent } from './claim-view/claim-view.component';
import { Routes } from '@angular/router';

export const FinanceRoutes: Routes = [
  {
    path: 'finance',
    pathMatch: 'full',
    redirectTo: 'finance/claims/list'
  },
  {
    path: 'finance/claims/list',
    component: ClaimListComponent,
    canActivate: [AuthorizationGuard]
  },
  {
    path: 'finance/claim/:claimId',
    component: ClaimViewComponent,
    canActivate: [AuthorizationGuard]
  }
];
