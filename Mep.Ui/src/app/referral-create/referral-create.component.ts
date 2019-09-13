import { Component, OnInit, ViewChild, Renderer2 } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { NhsNumberValidFormat } from '../helpers/nhs-number.validator';
import { PatientSearchService } from '../services/patient-search/patient-search.service';
import { ToastService } from '../services/toast/toast.service';
import { GpPracticeListService } from '../services/gp-practice-list/gp-practice-list.service';
import { throwError, Observable, of } from 'rxjs';
import { tap, switchMap, catchError, first } from 'rxjs/operators';

import { PatientSearchResult } from '../interfaces/patient-search-result';
import { PatientSearchParams } from '../interfaces/patient-search-params';
import { GpPractice } from '../interfaces/gp-practice';

import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

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
  gpSearching: boolean;
  gpSearchFailed: boolean;
  gpFieldsShown: boolean;

  @ViewChild('dangerTpl', null) dangerTemplate;
  @ViewChild('patientResults', null) patientResultTemplate;

  constructor(
    private formBuilder: FormBuilder,
    private patientService: PatientSearchService,
    private toastService: ToastService,
    private modalService: NgbModal,
    private renderer: Renderer2,
    private gpPracticeListService: GpPracticeListService
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
      ],
      gpPractice: [''],
      unknownGpPractice: false
    });

    // used for development testing
    // this.gpFieldsShown = true;
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

  get gpPracticeField() {
    return this.patientForm.controls.gpPractice;
  }

  IsSearchingForPatient(): boolean {
    return this.searchingForPatient;
  }

  SetGpPracticeField(id: number | null, text: string | null) {
    const gpPractice = {} as GpPractice;
    gpPractice.id = id;
    gpPractice.resultText = text;

    this.gpPracticeField.setValue(gpPractice);
  }

  ToggleGpPracticeUnknown(event: any) {
    if (event.target.checked) {
      // set the field to unknown, show the postcode field and set focus
      this.SetGpPracticeField(null, 'Unknown');
    } else {
      // ToDo: Hide and clear the residential postcode field
      this.SetGpPracticeField(null, '');
      this.ResetFocus('#gpPractice');
    }
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

  FormatGpMatches(value: any): string {
    return value.resultText || '';
  }

  gpSearch = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => this.gpSearching = true),
      switchMap(term =>
        this.gpPracticeListService.GetGpPracticeList(term).pipe(
          tap(() => this.gpSearchFailed = false),
          catchError(() => {
            this.gpSearchFailed = true;
            return of([]);
          }))
        ),
        tap(() => this.gpSearching = false)
    )

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

  ResetFocus(fieldName: string): void {
    this.renderer.selectRootElement(fieldName).focus();
  }

  async CancelPatientResultsModal() {
    this.alternativeIdentifierField.setValue('');
    this.nhsNumberField.setValue('');
    this.patientModal.close();

    // ToDo: Find a better way to set focus whilst waiting for modal to close !
    await this.Delay(150);
    this.ResetFocus('#nhsNumber');
  }

  async UseExistingPatient() {
    // ToDo: copy the existing patient details

    this.gpFieldsShown = true;
    this.SetGpPracticeField(this.patientResult.gpPracticeId, this.patientResult.gpPracticeNameAndPostcode);
    this.patientModal.close();

    // ToDo: Find a better way to set focus whilst waiting for modal to close !
    await this.Delay(150);
    this.ResetFocus('#gpPractice');
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
      params.NhsNumber = this.nhsNumberField.value;
    } else {
      params.AlternativeIdentifier = this.alternativeIdentifierField.value;
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
