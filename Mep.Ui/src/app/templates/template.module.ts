import { CancelActionModalComponent } from './cancel-action-modal/cancel-action-modal.component';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { PatientResultsModalComponent } from './patient-results-modal/patient-results-modal.component';


@NgModule({
  declarations: [
    CancelActionModalComponent,
    PatientResultsModalComponent
  ],
  imports: [
    CommonModule
  ],
  providers: [],
  exports: [
    CancelActionModalComponent,
    PatientResultsModalComponent
  ]
})
export class TemplateModule {}
