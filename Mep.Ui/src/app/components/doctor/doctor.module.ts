import { CommonModule, DecimalPipe } from '@angular/common';
import { DoctorAcceptComponent } from './doctor-accept/doctor-accept.component';
import { DoctorRoutes } from './doctor.routes';
import { DoctorSelectComponent } from './doctor-select/doctor-select.component';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedComponentsModule } from '../shared-components.module';

@NgModule({
  declarations: [
    DoctorAcceptComponent,
    DoctorSelectComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(DoctorRoutes),
    SharedComponentsModule
  ],
  providers: [
    DecimalPipe
  ]
})
export class DoctorModule {}
