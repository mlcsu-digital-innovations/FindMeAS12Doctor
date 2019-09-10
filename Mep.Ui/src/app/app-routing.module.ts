import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReferralCreateComponent } from './referral-create/referral-create.component';


const routes: Routes = [
  {path: 'create-referral', component: ReferralCreateComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
