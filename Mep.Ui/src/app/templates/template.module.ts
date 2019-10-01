import { CommonModule } from '@angular/common';
import { DangerToastComponent } from './danger-toast/danger-toast.component';
import { NgModule } from '@angular/core';
import { PatientResultsModalComponent } from './patient-results-modal/patient-results-modal.component';
import { SuccessToastComponent } from './success-toast/success-toast.component';
import { WarningToastComponent } from './warning-toast/warning-toast.component';
import { CancelActionModalComponent } from './cancel-action-modal/cancel-action-modal.component';

@NgModule({
  declarations: [
    CancelActionModalComponent,
    DangerToastComponent,
    PatientResultsModalComponent,
    SuccessToastComponent,
    WarningToastComponent
  ],
  imports: [
    CommonModule
  ],
  providers: [],
  exports: [
    CancelActionModalComponent,
    DangerToastComponent,
    PatientResultsModalComponent,
    SuccessToastComponent,
    WarningToastComponent
  ]
})
export class TemplateModule {}
