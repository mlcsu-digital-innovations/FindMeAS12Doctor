import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { OnCallDoctorListComponent } from './on-call-doctor-list/on-call-doctor-list.component';
import { OnCallDoctorRoutes } from './on-call-doctor.routes';
import { RouterModule } from '@angular/router';
import { SharedComponentsModule } from '../shared-components.module';

@NgModule({
  declarations: [
    OnCallDoctorListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(OnCallDoctorRoutes),
    SharedComponentsModule
  ],
  providers: []
})
export class OnCallDoctorModule {}
