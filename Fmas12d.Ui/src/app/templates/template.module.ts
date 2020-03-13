import { AllocationCompleteModalComponent } from './allocation-complete-modal/allocation-complete-modal.component';
import { CancelActionModalComponent } from './cancel-action-modal/cancel-action-modal.component';
import { CcgSelectionModalComponent } from './ccg-selection/ccg-selection-modal';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';
import { OnCallDoctorModalComponent } from './on-call-doctor-modal/on-call-doctor-modal.component';
import { PatientResultsModalComponent } from './patient-results-modal/patient-results-modal.component';
import { UnregisteredUsersModalComponent } from './unregistered-users-modal/unregistered-users-modal.component';

@NgModule({
  declarations: [
    AllocationCompleteModalComponent,
    CancelActionModalComponent,
    CcgSelectionModalComponent,
    OnCallDoctorModalComponent,
    PatientResultsModalComponent,
    UnregisteredUsersModalComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    NgbModule,
    ReactiveFormsModule
  ],
  providers: [],
  exports: [
    AllocationCompleteModalComponent,
    CancelActionModalComponent,
    CcgSelectionModalComponent,
    OnCallDoctorModalComponent,
    PatientResultsModalComponent,
    UnregisteredUsersModalComponent
  ]
})
export class TemplateModule {}
