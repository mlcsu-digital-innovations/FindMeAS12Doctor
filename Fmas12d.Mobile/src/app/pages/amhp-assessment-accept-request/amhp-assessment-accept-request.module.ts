import { AmhpAssessmentAcceptRequestPage } from './amhp-assessment-accept-request.page';
import { CommonModule } from '@angular/common';
import { ComponentsModule } from 'src/app/components/components.module';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: AmhpAssessmentAcceptRequestPage
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
  declarations: [AmhpAssessmentAcceptRequestPage]
})
export class AmhpAssessmentAcceptRequestPageModule {}
