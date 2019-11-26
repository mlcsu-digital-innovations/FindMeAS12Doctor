import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ComponentsModule } from 'src/app/components/components.module';
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
    ComponentsModule,
    FormsModule,
    IonicModule,
    RouterModule.forChild(routes)
  ],
  declarations: [AmhpAssessmentViewPage]
})
export class AmhpAssessmentViewPageModule {}
