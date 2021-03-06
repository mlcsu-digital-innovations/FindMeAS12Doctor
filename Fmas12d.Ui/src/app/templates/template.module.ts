import { AllocationCompleteModalComponent } from './allocation-complete-modal/allocation-complete-modal.component';
import { AssessmentOutcomeModalComponent } from './assessment-outcome-modal/assessment-outcome-modal.component';
import { CancelActionModalComponent } from './cancel-action-modal/cancel-action-modal.component';
import { CcgSelectionModalComponent } from './ccg-selection/ccg-selection-modal';
import { CommonModule } from '@angular/common';
import { ContactDetailModalComponent } from './contact-detail-modal/contact-detail.modal.component';
import { FinanceExportModalComponent } from './finance-export/finance-export-modal';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';
import { OnCallDoctorModalComponent } from './on-call-doctor-modal/on-call-doctor-modal.component';
import { PatientResultsModalComponent } from './patient-results-modal/patient-results-modal.component';
import { UnregisteredUsersModalComponent } from './unregistered-users-modal/unregistered-users-modal.component';
import { UserBankDetailsModalComponent } from './user-bank-details-modal/user-bank-details-modal-component';
import { UserContactDetailModalComponent } from './user-contact-detail-modal/user-contact-detail-modal.component';
import { UserFinanceDetailModalComponent } from './user-finance-detail-modal/user-finance-detail-modal.component';

@NgModule({
  declarations: [
    AllocationCompleteModalComponent,
    AssessmentOutcomeModalComponent,
    CancelActionModalComponent,
    CcgSelectionModalComponent,
    ContactDetailModalComponent,
    FinanceExportModalComponent,
    OnCallDoctorModalComponent,
    PatientResultsModalComponent,
    UnregisteredUsersModalComponent,
    UserBankDetailsModalComponent,
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
    AssessmentOutcomeModalComponent,
    CancelActionModalComponent,
    CcgSelectionModalComponent,
    ContactDetailModalComponent,
    FinanceExportModalComponent,
    OnCallDoctorModalComponent,
    PatientResultsModalComponent,
    UnregisteredUsersModalComponent,
    UserBankDetailsModalComponent,
    UserContactDetailModalComponent,
    UserFinanceDetailModalComponent
  ]
})
export class TemplateModule {}
