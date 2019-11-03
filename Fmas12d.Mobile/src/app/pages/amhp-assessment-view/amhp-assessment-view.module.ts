import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { IonicModule } from '@ionic/angular';

import { AmhpAssessmentViewPage } from './amhp-assessment-view.page';

const routes: Routes = [
  {
    path: '',
    component: AmhpAssessmentViewPage
  }
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RouterModule.forChild(routes)
  ],
  declarations: [AmhpAssessmentViewPage]
})
export class AmhpAssessmentViewPageModule {}
