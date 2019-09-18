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
import { PatientService } from "../services/patient/patient.service";
import { ReferralService } from "../services/referral/referral.service";

import { throwError, Observable, of } from "rxjs";
import { tap, switchMap, catchError } from "rxjs/operators";

import { PatientSearchResult } from "../interfaces/patient-search-result";
import { PatientSearchParams } from "../interfaces/patient-search-params";
import { TypeAheadResult } from "../interfaces/typeahead-result";
import { PostcodeSearchResult } from "../interfaces/postcode-search-result";
import { Patient } from "../interfaces/patient";

import { NgbModal, NgbModalRef } from "@ng-bootstrap/ng-bootstrap";
import { debounceTime, distinctUntilChanged } from "rxjs/operators";
import { map } from "rxjs/operators";
import { Referral } from "../interfaces/referral";

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
  creatingReferral: boolean;
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
  patientDetails: Patient;
  patientIdValidated: boolean;

  unknownGpPracticeId: number;
  unknownCcgId: number;

  @ViewChild("dangerTpl", null) dangerTemplate;
  @ViewChild("patientResults", null) patientResultTemplate;
  @ViewChild("cancelReferral", null) cancelReferralTemplate;

  constructor(
    private formBuilder: FormBuilder,
    private patientSearchService: PatientSearchService,
    private toastService: ToastService,
    private modalService: NgbModal,
    private renderer: Renderer2,
    private gpPracticeListService: GpPracticeListService,
    private postcodeValidationService: PostcodeValidationService,
    private ccgListService: CcgListService,
    private amhpListService: AmhpListService,
    private patientService: PatientService,
    private referralService: ReferralService
  ) {}

  ngOnInit() {

    // ToDo: Get the correct values for these ?
    this.unknownCcgId = 280;
    this.unknownGpPracticeId = 1;

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

    this.patientDetails = {} as Patient;
    this.patientIdValidated = false;

    this.onChanges();

    // used for development testing
    // this.gpFieldsShown = true;
    // this.residentialPostcodeFieldShown = true;
    // this.ccgFieldsShown = true;
    // this.amhpFieldsShown = true;
  }

  get nhsNumber(): string {
    return this.patientForm.controls.nhsNumber.value;
  }

  get alternativeIdentifier(): string {
    return this.patientForm.controls.alternativeIdentifier.value;
  }

  get gpPractice(): TypeAheadResult {
    return this.patientForm.controls.gpPractice.value;
  }

  get ccg(): TypeAheadResult {
    return this.patientForm.controls.ccg.value;
  }

  get amhpUser(): TypeAheadResult {
    return this.patientForm.controls.amhp.value;
  }

  get residentialPostcode(): string {
    return this.patientForm.controls.residentialPostcode.value;
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

  get amhpField() {
    return this.patientForm.controls.amhp;
  }

  onChanges(): void {
    this.nhsNumberField.valueChanges.subscribe(val => {
      this.patientIdValidated = val === this.patientDetails.NhsNumber;
    });

    this.alternativeIdentifierField.valueChanges.subscribe((val: string) => {

      if (this.patientDetails.AlternativeIdentifier) {
        this.patientIdValidated = val.toUpperCase() === this.patientDetails.AlternativeIdentifier.toUpperCase();
      }
    });
  }

  IsSearchingForPatient(): boolean {
    return this.searchingForPatient;
  }

  IsSearchingForPostcode(): boolean {
    return this.searchingForPostcode;
  }

  IsPatientIdValidated(): boolean {
    return this.patientIdValidated;
  }

  HasNoPatientIdErrors(): boolean {
    return (
      this.nhsNumberField.errors == null &&
      this.alternativeIdentifierField.errors == null
    );
  }

  HasValidNhsNumberOrAlternativeIdentifier(): boolean {
    return (
      (this.HasValidNHSNumber() || this.HasValidAlternativeIdentifier()) &&
      this.HasNoPatientIdErrors()
    );
  }

  HasValidGpOrPostcodeOrCcg(): boolean {
    return (
      this.gpPractice.id !== undefined ||
      this.residentialPostcode !== "" ||
      this.ccg.id !== undefined
    );
  }

  HasValidLeadAmhp(): boolean {
    return this.amhpUser.id !== undefined;
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
      this.SetGpPracticeField(this.unknownGpPracticeId, "Unknown");
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
      this.SetFieldFocus("#ccg");
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
      this.SetCcgField(this.unknownCcgId, "Unknown");
      this.SetFieldFocus("#amhp");
    } else {
      this.SetCcgField(null, "");
      this.SetFieldFocus("#ccg");
    }
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

  HasValidReferral(): boolean {
    // referral needs the following to be valid:
    // NHS number OR Alternative Identifier
    // GP Practice Or Postcode OR CCG
    // Lead AMHP details
    return (
      this.HasValidNhsNumberOrAlternativeIdentifier() &&
      this.HasValidGpOrPostcodeOrCcg() &&
      this.HasValidLeadAmhp()
    );
  }

  CancelReferral(): void {
    this.cancelModal = this.modalService.open(this.cancelReferralTemplate, {
      size: "lg"
    });
  }

  CreatePatient() {
    this.patientDetails.NhsNumber =
      +this.nhsNumber === 0 ? null : +this.nhsNumber;
    this.patientDetails.AlternativeIdentifier =
      this.alternativeIdentifier === "" ? null : this.alternativeIdentifier;
    this.patientDetails.GpPracticeId =
      this.gpPractice.id === 0 ? null : this.gpPractice.id;
    this.patientDetails.ResidentialPostcode =
      this.residentialPostcode === "" ? null : this.residentialPostcode;
    this.patientDetails.CcgId = this.ccg.id === 0 ? null : this.ccg.id;

    return this.patientService.createPatient(this.patientDetails).pipe(
      map((result: any) => {
        this.patientDetails.Id = result.id;
        this.SaveReferralDetails();
      })
    );
  }

  SaveReferralDetails(): void {
    const referral = {} as Referral;
    referral.LeadAmhpUserId = this.amhpUser.id;
    referral.PatientId = this.patientDetails.Id;

    this.referralService.createReferral(referral).subscribe(
      (result: Referral) => {
        this.toastService.show("Referral Created", {
          classname: "bg-success text-light",
          delay: 5000
        });
        this.creatingReferral = false;
        // navigate to the create examination page
      },
      error => {
        // this.searchingForPostcode = false;
        this.dangerMessage =
          "Server Error: Unable to create new referral ! Please try again in a few moments";
        this.toastService.show(this.dangerTemplate, {
          classname: "bg-danger text-light",
          delay: 10000
        });
        this.creatingReferral = false;
        return throwError(error);
      }
    );
  }

  CreateReferral() {
    this.creatingReferral = true;
    // create a new patient ?
    if (this.patientDetails.IsExistingPatient) {
      this.SaveReferralDetails();
    } else {
      this.CreatePatient().subscribe();
    }
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
    await this.Delay(100);
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

    this.patientDetails.Id = this.patientResult.patientId;
    this.patientDetails.AlternativeIdentifier = this.patientResult.alternativeIdentifier;
    this.patientDetails.NhsNumber = this.patientResult.nhsNumber;
    this.patientDetails.GpPracticeId = this.patientResult.gpPracticeId;
    this.patientDetails.ResidentialPostcode = this.patientResult.residentialPostcode;
    this.patientDetails.CcgId = this.patientResult.ccgId;
    this.patientDetails.IsExistingPatient = true;

    this.gpFieldsShown = true;
    this.SetGpPracticeField(
      this.patientResult.gpPracticeId,
      this.patientResult.gpPracticeNameAndPostcode
    );
    this.SetResidentialPostcodeField(this.patientResult.residentialPostcode);
    this.patientModal.close();
    this.SetFieldFocus("#amhp");

    this.nhsNumberField.markAsPristine();
    this.nhsNumberField.setValue(this.patientResult.nhsNumber);

    // only show the postcode field if the gpPractice field is null
    if (
      this.patientResult.residentialPostcode !== "" &&
      this.patientResult.gpPracticeId == null
    ) {
      this.residentialPostcodeFieldShown = true;
    }

    // only show the ccg field if the postcode field is null
    if (
      this.patientResult.residentialPostcode !== "" &&
      this.patientResult.gpPracticeId == null
    ) {
      this.residentialPostcodeFieldShown = true;
    }


    this.patientIdValidated = true;

    console.log(this.patientIdValidated);

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
            this.residentialPostcodeValidationMessage =
              "Unable to validate postcode";
            this.residentialPostcodeField.setErrors({
              UnableToValidatePostcode: true
            });
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

    this.patientSearchService.patientSearch(params).subscribe(
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
            this.patientIdValidated = true;
            this.gpFieldsShown = true;
            this.SetFieldFocus("#gpPractice");
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
              delay: 10000
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
          delay: 10000
        });
        return throwError(error);
      }
    );
  }
}
