import { AuthorizationGuard } from 'src/app/authorization.guard';
import { Routes } from '@angular/router';
import { ClaimListComponent } from './claim-list/claim-list.component';

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
  }
];
