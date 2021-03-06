import { CommonModule } from '@angular/common';
import { ComponentsModule } from 'src/app/components/components.module';
import { DoctorAssessmentRequestResponsePage } from './doctor-assessment-request-response.page';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../shared.module';

const routes: Routes = [
  {
    path: '',
    component: DoctorAssessmentRequestResponsePage
  }
];

@NgModule({
  imports: [
    CommonModule,
    ComponentsModule,
    FormsModule,
    IonicModule,
    RouterModule.forChild(routes),
    SharedModule
  ],
  declarations: [DoctorAssessmentRequestResponsePage]
})
export class DoctorAssessmentRequestResponsePageModule {}
