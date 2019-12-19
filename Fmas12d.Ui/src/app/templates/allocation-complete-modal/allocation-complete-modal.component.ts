import { AllocationConfirmation } from 'src/app/interfaces/allocation-confirmation';
import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { NgbDateStruct, NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';
import * as moment from 'moment';

@Component({
  selector: 'app-allocation-complete-modal',
  templateUrl: './allocation-complete-modal.component.html',
  styleUrls: ['./allocation-complete-modal.component.css']
})
export class AllocationCompleteModalComponent implements OnInit {

  public scheduledDate: NgbDateStruct;
  public scheduledTime: NgbTimeStruct;
  public minimumDate: NgbDateStruct;
  public dateError = '';

  @Input()
  minDate: Date;

  @Input()
  isPlanned: boolean;

  @Output() actioned = new EventEmitter<AllocationConfirmation>();

  ngOnInit() {
    this.minimumDate = this.ConvertToDateStruct(new Date());
    this.scheduledDate = this.ConvertToDateStruct(new Date());
    this.scheduledTime = this.ConvertToTimeStruct(new Date());
  }

  ConvertToDateStruct(dateValue: Date): NgbDateStruct {

    const momentDate = moment(dateValue);
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
      0,
      0
    );
  }


  modalAction(action: boolean) {

    const selectedDate = this.CreateDateFromPickerObjects(this.scheduledDate, this.scheduledTime);
    this.dateError = '';

    if (isNaN(selectedDate.getTime())) {
      this.dateError = 'Invalid Date Format';
    } else {
      const nowDate = new Date();
      nowDate.setHours(nowDate.getMinutes() - 5);

      if (selectedDate < nowDate) {
        this.dateError = 'Assessment Date / Time cannot be in the past';
      }
    }

    if (this.dateError !== '' && action === true) {
      return;
    }

    this.actioned.emit(
      {
        confirmed: action,
        scheduledDate: selectedDate
      }
    );
  }

}
