import { AmhpAssessmentRequestResponsePage } from './amhp-assessment-request-response.page';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: AmhpAssessmentRequestResponsePage
  }
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RouterModule.forChild(routes)
  ],
  declarations: [AmhpAssessmentRequestResponsePage]
})
export class AmhpAssessmentRequestResponsePageModule {}
