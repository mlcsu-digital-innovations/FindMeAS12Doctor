import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { IonicModule } from '@ionic/angular';

import { ClaimsConfirmationPage } from './claims-confirmation.page';

const routes: Routes = [
  {
    path: '',
    component: ClaimsConfirmationPage
  }
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RouterModule.forChild(routes)
  ],
  declarations: [ClaimsConfirmationPage]
})
export class ClaimsConfirmationPageModule {}
