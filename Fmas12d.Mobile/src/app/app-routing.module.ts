import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular/dist/msal-guard.service';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login', pathMatch: 'full'
  },
  {
    path: 'amhp-assessment-list',
    loadChildren:
      './pages/amhp-assessment-list/amhp-assessment-list.module#AmhpAssessmentListPageModule'
  },
  {
    path: 'amhp-assessment-outcome',
    loadChildren:
      './pages/amhp-assessment-outcome' +
      '/amhp-assessment-outcome.module#AmhpAssessmentOutcomePageModule' 
  },
  {
    path: 'amhp-assessment-view/:id',
    loadChildren:
      './pages/amhp-assessment-view/amhp-assessment-view.module#AmhpAssessmentViewPageModule'
  },
  {
    path: 'help',
    loadChildren: './pages/help/help.module#HelpPageModule'
  },
  {
    path: 'home',
    canActivate: [MsalGuard],
    loadChildren: () => import('./pages/home/home.module')
      .then(m => m.HomePageModule)
  },
  {
    path: 'login',
    loadChildren: './pages/login/login.module#LoginPageModule' 
  },
  {
    path: 'amhp-assessment-requests',
    loadChildren: './pages/amhp-assessment-requests/amhp-assessment-requests.module' +
    '#AmhpAssessmentRequestsPageModule'
  },
  {
    path: 'amhp-assessment-request-response/:id',
    loadChildren: './pages/amhp-assessment-request-response/amhp-assessment-request-response.module' +
    '#AmhpAssessmentRequestResponsePageModule'
  },
  {
    path: 'amhp-assessment-accept-request',
    loadChildren: './pages/amhp-assessment-accept-request/amhp-assessment-accept-request.module' +
    '#AmhpAssessmentAcceptRequestPageModule'
  },
  {
    path: 'doctor-availability-view',
    loadChildren: './pages/doctor-availability-view/doctor-availability-view.module' +
    '#DoctorAvailabilityViewPageModule' },
  {
    path: 'doctor-availability-add',
    loadChildren: './pages/doctor-availability-add/doctor-availability-add.module' +
    '#DoctorAvailabilityAddPageModule'
  },
  {
    path: 'doctor-availability-edit/:id',
    loadChildren: './pages/doctor-availability-edit/doctor-availability-edit.module' +
    '#DoctorAvailabilityEditPageModule'
  },




];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
