import { AmhpAssessmentListPage } from './amhp-assessment-list.page';
import { CommonModule } from '@angular/common';
import { ComponentsModule } from 'src/app/components/components.module';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: AmhpAssessmentListPage
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
  declarations: [
    AmhpAssessmentListPage    
  ],
  exports: [    
  ]
})
export class AmhpAssessmentListPageModule { }
