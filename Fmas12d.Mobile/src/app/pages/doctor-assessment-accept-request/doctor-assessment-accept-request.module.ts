import { CommonModule } from '@angular/common';
import { ComponentsModule } from 'src/app/components/components.module';
import { DoctorAssessmentAcceptRequestPage } from './doctor-assessment-accept-request.page';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: DoctorAssessmentAcceptRequestPage
  }
];

@NgModule({
  imports: [
    CommonModule,
    ComponentsModule,
    FormsModule,
    IonicModule,
    RouterModule.forChild(routes)
  ],
  declarations: [DoctorAssessmentAcceptRequestPage]
})
export class DoctorAssessmentAcceptRequestPageModule {}
