import { CancelActionModalComponent } from './cancel-action-modal/cancel-action-modal.component';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { PatientResultsModalComponent } from './patient-results-modal/patient-results-modal.component';
import { UnregisteredUsersModalComponent } from './unregistered-users-modal/unregistered-users-modal.component';


@NgModule({
  declarations: [
    CancelActionModalComponent,
    PatientResultsModalComponent,
    UnregisteredUsersModalComponent
  ],
  imports: [
    CommonModule
  ],
  providers: [],
  exports: [
    CancelActionModalComponent,
    PatientResultsModalComponent,
    UnregisteredUsersModalComponent
  ]
})
export class TemplateModule {}
