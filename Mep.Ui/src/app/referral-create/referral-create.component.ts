import { Component, OnInit, ViewChild, Renderer2 } from "@angular/core";
import {
  FormGroup,
  FormBuilder,
  Validators,
  ReactiveFormsModule
} from "@angular/forms";
import { NhsNumberValidFormat } from "../helpers/nhs-number.validator";
import { PatientSearchService } from "../services/patient-search/patient-search.service";
import { ToastService } from "../services/toast/toast.service";
import { GpPracticeListService } from "../services/gp-practice-list/gp-practice-list.service";
import { CcgListService } from "../services/ccg-list/ccg-list.service";
import { PostcodeValidationService } from "../services/postcode-validation/postcode-validation.service";
import { AmhpListService } from "../services/amhp-list/amhp-list.service";

import { throwError, Observable, of } from "rxjs";
import { tap, switchMap, catchError, first } from "rxjs/operators";

import { PatientSearchResult } from "../interfaces/patient-search-result";
import { PatientSearchParams } from "../interfaces/patient-search-params";
import { TypeAheadResult } from "../interfaces/typeahead-result";
import { PostcodeSearchResult } from "../interfaces/postcode-search-result";

import { NgbModal, NgbModalRef } from "@ng-bootstrap/ng-bootstrap";
import { debounceTime, distinctUntilChanged } from "rxjs/operators";

@Component({
  selector: "app-referral-create",
  templateUrl: "./referral-create.component.html",
  styleUrls: ["./referral-create.component.css"]
})
export class ReferralCreateComponent implements OnInit {
  myForm: FormGroup;
  patientForm: FormGroup;
  value = false;
  searchingForPatient: boolean;
  searchingForPostcode: boolean;
  dangerMessage: string;
  patientResult: PatientSearchResult;
  patientModal: NgbModalRef;
  cancelModal: NgbModalRef;
  gpSearching: boolean;
  gpSearchFailed: boolean;
  gpFieldsShown: boolean;
  residentialPostcodeFieldShown: boolean;
  residentialPostcodeValidationMessage: string;
  ccgSearching: boolean;
  ccgSearchFailed: boolean;
  ccgFieldsShown: boolean;
  amhpSearching: boolean;
  amhpSearchFailed: boolean;
  amhpFieldsShown: boolean;

  @ViewChild("dangerTpl", null) dangerTemplate;
  @ViewChild("patientResults", null) patientResultTemplate;
  @ViewChild("cancelReferral", null) cancelReferralTemplate;

  constructor(
    private formBuilder: FormBuilder,
    private patientService: PatientSearchService,
    private toastService: ToastService,
    private modalService: NgbModal,
    private renderer: Renderer2,
    private gpPracticeListService: GpPracticeListService,
    private postcodeValidationService: PostcodeValidationService,
    private ccgListService: CcgListService,
    private amhpListService: AmhpListService
  ) {}

  ngOnInit() {
    const postcodeRegex =
      "^([A-Za-z][A-Ha-hJ-Yj-y]?[0-9][A-Za-z0-9]? ?[0-9][A-Za-z]{2}|[Gg][Ii][Rr] ?0[Aa]{2})|(Unknown)$";

    this.residentialPostcodeValidationMessage = "Invalid Postcode";

    this.patientForm = this.formBuilder.group({
      nhsNumber: [
        "",
        [
          Validators.maxLength(10),
          Validators.pattern("^[1-9]\\d{9}$"),
          NhsNumberValidFormat
        ]
      ],
      alternativeIdentifier: [
        "",
        [Validators.maxLength(200), Validators.pattern(".*[0-9].*")]
      ],
      gpPractice: [""],
      unknownGpPractice: false,
      residentialPostcode: [
        "",
        [
          Validators.minLength(6),
          Validators.maxLength(8),
          Validators.pattern(postcodeRegex)
        ]
      ],
      unknownResidentialPostcode: false,
      ccg: [""],
      unknownCcg: false,
      amhp: [""]
    });

    // used for development testing
    this.gpFieldsShown = true;
    this.residentialPostcodeFieldShown = true;
    this.ccgFieldsShown = true;
    this.amhpFieldsShown = true;
  }

  get patient() {
    return this.patientForm.controls;
  }

  get nhsNumberField() {
    return this.patientForm.controls.nhsNumber;
  }

  get residentialPostcodeField() {
    return this.patientForm.controls.residentialPostcode;
  }

  get alternativeIdentifierField() {
    return this.patientForm.controls.alternativeIdentifier;
  }

  get gpPracticeField() {
    return this.patientForm.controls.gpPractice;
  }

  get ccgField() {
    return this.patientForm.controls.ccg;
  }

  IsSearchingForPatient(): boolean {
    return this.searchingForPatient;
  }

  IsSearchingForPostcode(): boolean {
    return this.searchingForPostcode;
  }

  SetGpPracticeField(id: number | null, text: string | null) {
    const gpPractice = {} as TypeAheadResult;
    gpPractice.id = id;
    gpPractice.resultText = text;

    this.gpPracticeField.setValue(gpPractice);
  }

  SetCcgField(id: number | null, text: string | null) {
    const ccg = {} as TypeAheadResult;
    ccg.id = id;
    ccg.resultText = text;

    this.ccgField.setValue(ccg);
  }

  SetResidentialPostcodeField(text: string | null) {
    this.residentialPostcodeField.setValue(text);
  }

  ToggleGpPracticeUnknown(event: any) {
    if (event.target.checked) {
      // set the field to unknown, show the postcode field and set focus
      this.SetGpPracticeField(null, "Unknown");
      this.residentialPostcodeFieldShown = true;
      this.SetFieldFocus("#residentialPostcode");
    } else {
      this.SetGpPracticeField(null, "");
      this.SetResidentialPostcodeField("");
      this.residentialPostcodeFieldShown = false;
      this.SetFieldFocus("#gpPractice");
    }
  }

  ToggleResidentialPostcodeUnknown(event: any) {
    if (event.target.checked) {
      // set the field to unknown, show the CCG field and set focus
      this.SetResidentialPostcodeField("Unknown");
      this.ccgFieldsShown = true;
    } else {
      this.SetResidentialPostcodeField("");
      this.SetCcgField(null, "");
      this.ccgFieldsShown = false;
      this.SetFieldFocus("#residentialPostcode");
    }
  }

  ToggleCcgUnknown(event: any) {
    if (event.target.checked) {
      // set the field to unknown, show the CCG field and set focus
      this.SetCcgField(null, "Unknown");
      // this.ccgFieldsShown = true;
    } else {
      this.SetCcgField(null, "");
      this.SetFieldFocus("#ccg");
    }
  }

  submit() {
    console.log(this.patientForm.controls.nhsNumber);
  }

  HasInvalidNHSNumber(): boolean {
    return (
      this.nhsNumberField.value !== "" && this.nhsNumberField.errors !== null
    );
  }

  HasValidPostcode(): boolean {
    return (
      this.residentialPostcodeField.value !== "" &&
      this.residentialPostcodeField.errors == null
    );
  }

  HasInvalidPostcode(): boolean {
    return (
      this.residentialPostcodeField.value !== "" &&
      this.residentialPostcodeField.errors !== null
    );
  }

  HasValidNHSNumber(): boolean {
    return (
      this.nhsNumberField.value !== "" && this.nhsNumberField.errors == null
    );
  }

  HasInvalidAlternativeIdentifier(): boolean {
    return (
      this.alternativeIdentifierField.value !== "" &&
      this.alternativeIdentifierField.errors !== null
    );
  }

  HasValidAlternativeIdentifier(): boolean {
    return (
      this.alternativeIdentifierField.value !== "" &&
      this.alternativeIdentifierField.errors == null
    );
  }

  CancelReferral(): void {
    this.cancelModal = this.modalService.open(
      this.cancelReferralTemplate,
      { size: "lg" }
    );
  }

  CancelCancellation(): void {
    // close the modal
    this.cancelModal.close();
  }

  ConfirmCancellation(): void {
    // close the modal
    this.cancelModal.close();

    // ToDo: navigate to the previous page
  }

  FormatTypeAheadResults(value: any): string {
    return value.resultText || "";
  }

  amhpSearch = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => (this.amhpSearching = true)),
      switchMap(term =>
        this.amhpListService.GetAmhpList(term).pipe(
          tap(() => (this.amhpSearchFailed = false)),
          catchError(() => {
            this.ccgSearchFailed = true;
            return of([]);
          })
        )
      ),
      tap(() => (this.amhpSearching = false))
    )

  ccgSearch = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => (this.ccgSearching = true)),
      switchMap(term =>
        this.ccgListService.GetCcgList(term).pipe(
          tap(() => (this.ccgSearchFailed = false)),
          catchError(() => {
            this.ccgSearchFailed = true;
            return of([]);
          })
        )
      ),
      tap(() => (this.ccgSearching = false))
    )

  gpSearch = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => (this.gpSearching = true)),
      switchMap(term =>
        this.gpPracticeListService.GetGpPracticeList(term).pipe(
          tap(() => (this.gpSearchFailed = false)),
          catchError(() => {
            this.gpSearchFailed = true;
            return of([]);
          })
        )
      ),
      tap(() => (this.gpSearching = false))
    )

  DisableIfFieldHasValue(fieldName: string): boolean {
    if (fieldName in this.patientForm.controls) {
      return this.patientForm.controls[fieldName].value !== "";
    } else {
      throw new Error(
        `DisableIfFieldHasValue(fieldName: string) unable to find field [${fieldName}]`
      );
    }
  }

  DisablePatientValidationButtonIfFieldsAreInvalid(): boolean {
    // field is only valid if it has a value and there aren't any errors
    const nhsNumberFieldInValid =
      this.nhsNumberField.value === "" || this.nhsNumberField.errors !== null;
    const alternativeIdentifierFieldInValid =
      this.alternativeIdentifierField.value === "" ||
      this.alternativeIdentifierField.errors !== null;

    return nhsNumberFieldInValid && alternativeIdentifierFieldInValid;
  }

  DisablePostcodeValidationButtonIfFieldIsInvalid(): boolean {
    return (
      this.residentialPostcodeField.value === "" ||
      this.residentialPostcodeField.errors !== null
    );
  }

  async Delay(milliseconds: number) {
    return new Promise(resolve => setTimeout(resolve, milliseconds));
  }

  async SetFieldFocus(fieldName: string) {
    // ToDo: Find a better way to do this !
    await this.Delay(150);
    this.renderer.selectRootElement(fieldName).focus();
  }

  async CancelPatientResultsModal() {
    this.alternativeIdentifierField.setValue("");
    this.nhsNumberField.setValue("");
    this.patientModal.close();
    this.SetFieldFocus("#nhsNumber");
  }

  async UseExistingPatient() {
    // ToDo: copy the existing patient details

    this.gpFieldsShown = true;
    this.SetGpPracticeField(
      this.patientResult.gpPracticeId,
      this.patientResult.gpPracticeNameAndPostcode
    );
    this.SetResidentialPostcodeField(this.patientResult.residentialPostcode);
    this.patientModal.close();
    this.SetFieldFocus("#gpPractice");

    if (this.patientResult.residentialPostcode !== "") {
      this.residentialPostcodeFieldShown = true;
    }
  }

  UseExistingReferral(): void {
    // ToDo: navigate to the existing referral page
    this.patientModal.close();
  }

  ValidatePostcode(): void {

    if (this.searchingForPostcode || this.HasInvalidPostcode()) {
      return;
    }

    this.searchingForPostcode = true;

    this.postcodeValidationService
      .validatePostcode(this.residentialPostcodeField.value)
      .subscribe(
        (result: PostcodeSearchResult) => {
          this.searchingForPostcode = false;

          if (result.code == null) {
            this.residentialPostcodeField.setErrors(null);
            this.residentialPostcodeValidationMessage = 'Unable to validate postcode';
            this.residentialPostcodeField.setErrors({ UnableToValidatePostcode: true });
            this.SetFieldFocus("#residentialPostcode");
          }
        },
        error => {
          this.searchingForPostcode = false;
          this.dangerMessage =
            "Server Error: Unable to validate residential postcode ! Please try again in a few moments";
          this.toastService.show(this.dangerTemplate, {
            classname: "bg-danger text-light",
            delay: 15000
          });
          return throwError(error);
        }
      );
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
            this.toastService.show("No existing patients found", {
              classname: "bg-success text-light",
              delay: 5000
            });
            break;
          case 1:
            this.patientResult = results[0];
            this.patientModal = this.modalService.open(
              this.patientResultTemplate,
              { size: "lg" }
            );
            break;
          default:
            this.dangerMessage =
              "Validation Error: Multiple patients found ! Please inform a system administrator";
            this.toastService.show(this.dangerTemplate, {
              classname: "bg-danger text-light",
              delay: 15000
            });
            break;
        }
      },
      error => {
        this.searchingForPatient = false;
        this.dangerMessage =
          "Server Error: Unable to validate patient details ! Please try again in a few moments";
        this.toastService.show(this.dangerTemplate, {
          classname: "bg-danger text-light",
          delay: 15000
        });
        return throwError(error);
      }
    );
  }
}
