import { AmhpAssessmentViewPage } from './amhp-assessment-view.page';
import { CommonModule } from '@angular/common';
import { ComponentsModule } from 'src/app/components/components.module';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserContactModalPage } from '../user-contact-modal/user-contact-modal.page';

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
  declarations: [AmhpAssessmentViewPage, UserContactModalPage],
  entryComponents: [
    UserContactModalPage
  ]
})
export class AmhpAssessmentViewPageModule {}
