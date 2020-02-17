import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cancel-action-modal',
  templateUrl: './cancel-action-modal.component.html',
  styleUrls: ['./cancel-action-modal.component.css']
})
export class CancelActionModalComponent {

  @Input()
  modalTitle: string;

  @Input()
  modalBody: string;

  @Input()
  referralStatus: string;

  @Output() actioned = new EventEmitter<boolean>();

  modalAction(action: boolean) {
    this.actioned.emit(action);
  }

}
