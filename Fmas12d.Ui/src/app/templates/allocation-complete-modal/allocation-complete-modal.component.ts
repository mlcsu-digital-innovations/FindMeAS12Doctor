import { AllocationConfirmation } from 'src/app/interfaces/allocation-confirmation';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NgbDateStruct, NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';
import * as moment from 'moment';

@Component({
  selector: 'app-allocation-complete-modal',
  templateUrl: './allocation-complete-modal.component.html',
  styleUrls: ['./allocation-complete-modal.component.css']
})
export class AllocationCompleteModalComponent {

  public scheduledDate: Date;
  public scheduledTime: Date;
  public minimumDate: NgbDateStruct;

  @Input()
  minDate: any;

  @Input()
  isPlanned: boolean;

  @Output() actioned = new EventEmitter<AllocationConfirmation>();

  constructor() {
    console.log(this.minDate);
    this.minimumDate = this.ConvertToDateStruct(this.minDate);
    console.log(this.minimumDate);
  }

  ConvertToDateStruct(dateValue: Date): NgbDateStruct {

    console.log(dateValue);

    const momentDate = moment(dateValue);
    console.log(momentDate);
    const dateStruct = {} as NgbDateStruct;
    dateStruct.day = momentDate.date();
    dateStruct.month = momentDate.month() + 1;
    dateStruct.year = momentDate.year();

    return dateStruct;
  }

  ConvertToTimeStruct(dateValue: Date): NgbTimeStruct {

    // round up to the next 5 minute interval
    const start = moment(dateValue);
    const remainder = 5 - (start.minute() % 5);
    const momentDate = moment(start).add(remainder, 'minutes');
    const timeStruct = {} as NgbTimeStruct;
    timeStruct.hour = momentDate.hour();
    timeStruct.minute = momentDate.minutes();
    timeStruct.second = momentDate.seconds();

    return timeStruct;
  }


  CreateDateFromPickerObjects(datePart: NgbDateStruct, timePart: NgbTimeStruct): Date {
    return new Date(
      datePart.year,
      datePart.month - 1,
      datePart.day,
      timePart.hour,
      timePart.minute,
      timePart.second,
      0
    );
  }

  modalAction(action: boolean) {
    console.log(this.scheduledDate);
    console.log(this.scheduledTime);
    this.actioned.emit({confirmed: action, scheduledDate: null});
  }

}
