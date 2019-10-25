import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { IonicModule } from '@ionic/angular';

import { AmhpExaminationViewPage } from './amhp-examination-view.page';

const routes: Routes = [
  {
    path: '',
    component: AmhpExaminationViewPage
  }
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RouterModule.forChild(routes)
  ],
  declarations: [AmhpExaminationViewPage]
})
export class AmhpExaminationViewPageModule {}
