import { AllocationCompleteModalComponent } from './allocation-complete-modal/allocation-complete-modal.component';
import { CancelActionModalComponent } from './cancel-action-modal/cancel-action-modal.component';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { OnCallDoctorModalComponent } from './on-call-doctor-modal/on-call-doctor-modal.component';
import { PatientResultsModalComponent } from './patient-results-modal/patient-results-modal.component';
import { UnregisteredUsersModalComponent } from './unregistered-users-modal/unregistered-users-modal.component';
import { UserContactDetailModalComponent } from './user-contact-detail-modal/user-contact-detail-modal.component';
import { UserFinanceDetailModalComponent } from './user-finance-detail-modal/user-finance-detail-modal.component';

@NgModule({
  declarations: [
    AllocationCompleteModalComponent,
    CancelActionModalComponent,
    OnCallDoctorModalComponent,
    PatientResultsModalComponent,    
    UnregisteredUsersModalComponent, 
    UserContactDetailModalComponent, 
    UserFinanceDetailModalComponent
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
    OnCallDoctorModalComponent,
    PatientResultsModalComponent,
    UnregisteredUsersModalComponent,
    UserContactDetailModalComponent,
    UserFinanceDetailModalComponent
  ]
})
export class TemplateModule {}
