import { AllocationConfirmation } from 'src/app/interfaces/allocation-confirmation';
import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-allocation-complete-modal',
  templateUrl: './allocation-complete-modal.component.html',
  styleUrls: ['./allocation-complete-modal.component.css']
})
export class AllocationCompleteModalComponent {

  public scheduledDate: Date;
  public scheduledTime: Date;

  @Input()
  minDate: Date;

  @Input()
  isPlanned: boolean;

  @Output() actioned = new EventEmitter<AllocationConfirmation>();

  modalAction(action: boolean) {
    console.log(this.scheduledDate);
    console.log(this.scheduledTime);
    this.actioned.emit({confirmed: action, scheduledDate: null});
  }

}
