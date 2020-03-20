import { AllocationConfirmation } from 'src/app/interfaces/allocation-confirmation';
import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import * as moment from 'moment';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Claimant } from 'src/app/interfaces/claimant';
import { Ccg } from 'src/app/interfaces/ccg';

@Component({
  selector: 'app-user-bank-details-modal',
  templateUrl: './user-bank-details-modal-component.html',
  styleUrls: ['./user-bank-details-modal-component.css']
})
export class UserBankDetailsModalComponent implements OnInit {

  @Input() claimant: Claimant;

  @Input() ccg: Ccg;

  @Output() actioned = new EventEmitter<{ok: boolean, vsr: string}>();

  vsrForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {

    this.vsrForm = this.formBuilder.group({
      claimant: [
        {
          value: this.claimant.displayName,
          disabled: true
        }
      ],
      ccg: [
        {
          value: this.ccg.name,
          disabled: true
        }
      ],
      vsrNumber: [
        '',
        [
          Validators.required,
          Validators.maxLength(10),
          Validators.pattern(/^\d{0,10}$/)
        ]
      ]
    });
  }

  modalAction(action: boolean) {
    this.actioned.emit(
      {ok: action, vsr: this.vsrField.value}
    );
  }

  get vsrField() {
    return this.vsrForm.controls.vsrNumber;
  }

}
