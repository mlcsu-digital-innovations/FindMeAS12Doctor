import { CommonModule } from '@angular/common';
import { ComponentsModule } from 'src/app/components/components.module';
import { DoctorOnCallConfirmRejectPage } from './doctor-on-call-confirm-reject.page';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: DoctorOnCallConfirmRejectPage
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
  declarations: [DoctorOnCallConfirmRejectPage]
})
export class DoctorOnCallConfirmRejectPageModule {}
