import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home', pathMatch: 'full'
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
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
