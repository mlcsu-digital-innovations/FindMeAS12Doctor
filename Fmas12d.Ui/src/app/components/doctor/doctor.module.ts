import { CommonModule, DecimalPipe } from '@angular/common';
import { DoctorAddComponent } from './doctor-add/doctor-add.component';
import { DoctorAllocateComponent } from './doctor-allocate/doctor-allocate.component';
import { DoctorRoutes } from './doctor.routes';
import { DoctorSelectComponent } from './doctor-select/doctor-select.component';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedComponentsModule } from '../shared-components.module';

@NgModule({
  declarations: [
    DoctorAddComponent,
    DoctorAllocateComponent,
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
