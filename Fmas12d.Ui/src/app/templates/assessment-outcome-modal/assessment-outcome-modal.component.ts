import { AllocationConfirmation } from 'src/app/interfaces/allocation-confirmation';
import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CurrentAssessment } from 'src/app/interfaces/current-assessment';
import { NameIdList } from 'src/app/interfaces/name-id-list';
import { AssessmentUser } from 'src/app/interfaces/assessment-user';

@Component({
  selector: 'app-assessment-outcome-modal',
  templateUrl: './assessment-outcome-modal.component.html',
  styleUrls: ['./assessment-outcome-modal.component.css']
})
export class AssessmentOutcomeModalComponent implements OnInit {

  @Input()
  currentAssessment: CurrentAssessment;

  @Input()
  outcome: NameIdList;

  @Input()
  attendingDoctors: AssessmentUser[];

  @Input()
  confirmedDate: Date;

  @Input()
  patientIdentifier: string;

  @Output() actioned = new EventEmitter<AllocationConfirmation>();

  ngOnInit() {
    this.attendingDoctors = this.attendingDoctors.filter(doctor => doctor.selected === true);
  }

  modalAction(action: boolean) {

    this.actioned.emit(
      {
        confirmed: action
      }
    );
  }

}
