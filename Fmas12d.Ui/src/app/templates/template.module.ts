import { AllocationCompleteModalComponent } from './allocation-complete-modal/allocation-complete-modal.component';
import { CancelActionModalComponent } from './cancel-action-modal/cancel-action-modal.component';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { PatientResultsModalComponent } from './patient-results-modal/patient-results-modal.component';
import { UnregisteredUsersModalComponent } from './unregistered-users-modal/unregistered-users-modal.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AllocationCompleteModalComponent,
    CancelActionModalComponent,
    PatientResultsModalComponent,
    UnregisteredUsersModalComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    NgbModule
  ],
  providers: [],
  exports: [
    AllocationCompleteModalComponent,
    CancelActionModalComponent,
    PatientResultsModalComponent,
    UnregisteredUsersModalComponent
  ]
})
export class TemplateModule {}
