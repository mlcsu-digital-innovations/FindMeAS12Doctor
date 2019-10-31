import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'amhp-examination-list', pathMatch: 'full'
  },  
  {
    path: 'amhp-examination-list',
    loadChildren:
      './pages/amhp-examination-list/amhp-examination-list.module#AmhpExaminationListPageModule'
  },
  { 
    path: 'amhp-examination-outcome', 
    loadChildren: 
      './pages/amhp-examination-outcome' + 
      '/amhp-examination-outcome.module#AmhpExaminationOutcomePageModule' 
  },
  {
    path: 'amhp-examination-view/:id',
    loadChildren: 
      './pages/amhp-examination-view/amhp-examination-view.module#AmhpExaminationViewPageModule'
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
  

];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
