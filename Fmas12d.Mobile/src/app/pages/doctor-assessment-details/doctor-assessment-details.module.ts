import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { IonicModule } from '@ionic/angular';

import { DoctorAssessmentDetailsPage } from './doctor-assessment-details.page';

const routes: Routes = [
  {
    path: '',
    component: DoctorAssessmentDetailsPage
  }
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RouterModule.forChild(routes)
  ],
  declarations: [DoctorAssessmentDetailsPage]
})
export class DoctorAssessmentDetailsPageModule {}
