import { Component, Input, Output, EventEmitter } from '@angular/core';
import { PatientSearchResult } from 'src/app/interfaces/patient-search-result';
import { PatientAction } from '../../enums/PatientModalAction.enum';

@Component({
  selector: 'app-patient-results-modal',
  templateUrl: './patient-results-modal.component.html',
  styleUrls: ['./patient-results-modal.component.css']
})
export class PatientResultsModalComponent {

  cancelAction = PatientAction.Cancel;
  existingReferral = PatientAction.ExistingReferral;
  existingPatient = PatientAction.ExistingPatient;

  @Input()
  patientResult: PatientSearchResult;

  @Output() actioned = new EventEmitter<number>();

  modalAction(action: number) {
    this.actioned.emit(action);
  }
}
