import { Component, OnInit, ViewChild, Renderer2 } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NhsNumberValidFormat } from '../helpers/nhs-number.validator';
import { PatientSearchService } from '../services/patient-search/patient-search.service';
import { ToastService } from '../services/toast/toast.service';
import { throwError } from 'rxjs';

import { PatientSearchResult } from '../interfaces/patient-search-result';
import { PatientSearchParams } from '../interfaces/patient-search-params';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-referral-create',
  templateUrl: './referral-create.component.html',
  styleUrls: ['./referral-create.component.css']
})
export class ReferralCreateComponent implements OnInit {
  myForm: FormGroup;
  patientForm: FormGroup;
  value = false;
  searchingForPatient: boolean;
  dangerMessage: string;
  patientResult: PatientSearchResult;
  patientModal: NgbModalRef;

  @ViewChild('dangerTpl', null) dangerTemplate;
  @ViewChild('patientResults', null) patientResultTemplate;

  constructor(
    private formBuilder: FormBuilder,
    private patientService: PatientSearchService,
    private toastService: ToastService,
    private modalService: NgbModal,
    private renderer: Renderer2
  ) {}

  ngOnInit() {
    this.patientForm = this.formBuilder.group({
      nhsNumber: [
        '',
        [
          Validators.maxLength(10),
          Validators.pattern('^[1-9]\\d{9}$'),
          NhsNumberValidFormat
        ]
      ],
      alternativeIdentifier: [
        '',
        [Validators.maxLength(200), Validators.pattern('.*[0-9].*')]
      ]
    });
  }

  get patient() {
    return this.patientForm.controls;
  }

  get nhsNumberField() {
    return this.patientForm.controls.nhsNumber;
  }

  get alternativeIdentifierField() {
    return this.patientForm.controls.alternativeIdentifier;
  }

  IsSearchingForPatient(): boolean {
    return this.searchingForPatient;
  }

  submit() {
    console.log(this.patientForm.controls.nhsNumber);
  }

  HasInvalidNHSNumber(): boolean {
    return (
      this.nhsNumberField.value !== '' && this.nhsNumberField.errors !== null
    );
  }

  HasValidNHSNumber(): boolean {
    return (
      this.nhsNumberField.value !== '' && this.nhsNumberField.errors == null
    );
  }

  HasInvalidAlternativeIdentifier(): boolean {
    return (
      this.alternativeIdentifierField.value !== '' &&
      this.alternativeIdentifierField.errors !== null
    );
  }

  HasValidAlternativeIdentifier(): boolean {
    return (
      this.alternativeIdentifierField.value !== '' &&
      this.alternativeIdentifierField.errors == null
    );
  }

  DisableIfFieldHasValue(fieldName: string): boolean {
    if (fieldName in this.patientForm.controls) {
      return this.patientForm.controls[fieldName].value !== '';
    } else {
      throw new Error(
        `DisableIfFieldHasValue(fieldName: string) unable to find field [${fieldName}]`
      );
    }
  }

  DisablePatientValidationButtonIfFieldsAreInvalid(): boolean {
    // field is only valid if it has a value and there aren't any errors
    const nhsNumberFieldInValid =
      this.nhsNumberField.value === '' || this.nhsNumberField.errors !== null;
    const alternativeIdentifierFieldInValid =
      this.alternativeIdentifierField.value === '' ||
      this.alternativeIdentifierField.errors !== null;

    return nhsNumberFieldInValid && alternativeIdentifierFieldInValid;
  }

  async Delay(milliseconds: number) {
    return new Promise(resolve => setTimeout(resolve, milliseconds));
  }

  ResetFocus(): void {
    this.renderer.selectRootElement('#nhsNumber').focus();
  }

  async CancelPatientResultsModal() {
    this.alternativeIdentifierField.setValue('');
    this.nhsNumberField.setValue('');
    this.patientModal.close();

    // ToDo: Find a better way to set focus whilst waiting for modal to close !
    await this.Delay(150);
    this.ResetFocus();
  }

  UseExistingPatient(): void {
    // ToDo: copy the existing patient details
    this.patientModal.close();
  }

  UseExistingReferral(): void {
    // ToDo: navigate to the existing referral page
    this.patientModal.close();
  }

  ValidatePatient(): void {
    if (
      this.searchingForPatient ||
      this.HasInvalidNHSNumber() ||
      this.HasInvalidAlternativeIdentifier()
    ) {
      return;
    }

    // prevent further buttons clicks and update the page
    this.searchingForPatient = true;
    const params = {} as PatientSearchParams;

    if (this.HasValidNHSNumber()) {
      params.nhsNumber = this.nhsNumberField.value;
    } else {
      params.alternativeIdentifier = this.alternativeIdentifierField.value;
    }

    this.patientService.patientSearch(params).subscribe(
      (results: PatientSearchResult[]) => {
        this.searchingForPatient = false;
        // if there are any matching results then display them in a modal
        switch (results.length) {
          case 0:
            // no matching patients found, inform user with toast ?
            this.toastService.show('No existing patients found', {
              classname: 'bg-success text-light',
              delay: 5000
            });
            break;
          case 1:
            this.patientResult = results[0];
            this.patientModal = this.modalService.open(
              this.patientResultTemplate,
              { size: 'lg' }
            );
            break;
          default:
            this.dangerMessage =
              'Validation Error: Multiple patients found ! Please inform a system administrator';
            this.toastService.show(this.dangerTemplate, {
              classname: 'bg-danger text-light',
              delay: 15000
            });
            break;
        }
      },
      error => {
        this.searchingForPatient = false;
        this.dangerMessage =
          'Server Error: Unable to validate patient details ! Please try again in a few moments';
        this.toastService.show(this.dangerTemplate, {
          classname: 'bg-danger text-light',
          delay: 15000
        });
        return throwError(error);
      }
    );
  }
}
