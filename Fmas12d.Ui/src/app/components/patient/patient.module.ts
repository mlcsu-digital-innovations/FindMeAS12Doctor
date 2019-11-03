import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { PatientEditComponent } from './patient-edit/patient-edit.component';
import { PatientRoutes } from './patient.routes';
import { RouterModule } from '@angular/router';
import { SharedComponentsModule } from '../shared-components.module';

@NgModule({
  declarations: [
    PatientEditComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(PatientRoutes),
    SharedComponentsModule
  ],
  providers: []
})
export class PatientModule {}
