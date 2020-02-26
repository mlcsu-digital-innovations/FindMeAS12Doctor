import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { RouteGuardService } from './services/route-guard/route-guard.service';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home', pathMatch: 'full'
  },
  {
    path: 'amhp-assessment-list',
    canActivate: [RouteGuardService],
    loadChildren:
      './pages/amhp-assessment-list/amhp-assessment-list.module#AmhpAssessmentListPageModule'
  },
  {
    path: 'amhp-assessment-outcome',
    canActivate: [RouteGuardService],
    loadChildren:
      './pages/amhp-assessment-outcome' +
      '/amhp-assessment-outcome.module#AmhpAssessmentOutcomePageModule' 
  },
  {
    path: 'amhp-assessment-view/:id',
    canActivate: [RouteGuardService],
    loadChildren:
      './pages/amhp-assessment-view/amhp-assessment-view.module#AmhpAssessmentViewPageModule'
  },
  {
    path: 'help',
    canActivate: [RouteGuardService],
    loadChildren: './pages/help/help.module#HelpPageModule'
  },
  {
    path: 'home',
    canActivate: [RouteGuardService],
    loadChildren: () => import('./pages/home/home.module')
      .then(m => m.HomePageModule)
  },
  {
    path: 'login',
    loadChildren: './pages/login/login.module#LoginPageModule' 
  },
  {
    path: 'doctor-assessments',
    canActivate: [RouteGuardService],
    loadChildren: './pages/doctor-assessments/doctor-assessments.module' +
    '#DoctorAssessmentsPageModule'
  },
  {
    path: 'doctor-assessment-details/:id',
    canActivate: [RouteGuardService],
    loadChildren: './pages/doctor-assessment-details/doctor-assessment-details.module' +
    '#DoctorAssessmentDetailsPageModule'
  },
  {
    path: 'doctor-assessment-request-response/:id',
    canActivate: [RouteGuardService],
    loadChildren: './pages/doctor-assessment-request-response/doctor-assessment-request-response.module' +
    '#DoctorAssessmentRequestResponsePageModule'
  },
  {
    path: 'amhp-assessment-accept-request',
    canActivate: [RouteGuardService],
    loadChildren: './pages/amhp-assessment-accept-request/amhp-assessment-accept-request.module' +
    '#AmhpAssessmentAcceptRequestPageModule'
  },
  {
    path: 'doctor-availability-view',
    canActivate: [RouteGuardService],
    loadChildren: './pages/doctor-availability-view/doctor-availability-view.module' +
    '#DoctorAvailabilityViewPageModule' },
  {
    path: 'doctor-availability-add',
    canActivate: [RouteGuardService],
    loadChildren: './pages/doctor-availability-add/doctor-availability-add.module' +
    '#DoctorAvailabilityAddPageModule'
  },
  {
    path: 'doctor-availability-edit/:id',
    canActivate: [RouteGuardService],
    loadChildren: './pages/doctor-availability-edit/doctor-availability-edit.module' +
    '#DoctorAvailabilityEditPageModule'
  },
  {
    path: 'claims-list',
    loadChildren: './pages/claims-list/claims-list.module#ClaimsListPageModule'
  },
  {
    path: 'claims-create/:assessmentId',
    loadChildren: './pages/claims-create/claims-create.module#ClaimsCreatePageModule'
  },
  {
    path: 'claims-details/:claimId',
    loadChildren: './pages/claims-details/claims-details.module#ClaimsDetailsPageModule'
  },
  {
    path: 'doctor-on-call-list', 
    canActivate: [RouteGuardService],
    loadChildren: './pages/doctor-on-call-list/doctor-on-call-list.module' +
    '#DoctorOnCallListPageModule'
  },
  {
    path: 'doctor-on-call-confirm-reject',
    canActivate: [RouteGuardService],
    loadChildren: './pages/doctor-on-call-confirm-reject/doctor-on-call-confirm-reject.module' +
    '#DoctorOnCallConfirmRejectPageModule'
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
