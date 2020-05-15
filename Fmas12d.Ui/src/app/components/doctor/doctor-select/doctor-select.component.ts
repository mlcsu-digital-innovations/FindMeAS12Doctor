import { ActivatedRoute, ParamMap } from '@angular/router';
import { Assessment } from 'src/app/interfaces/assessment';
import { AssessmentAvailability } from 'src/app/interfaces/assessment-availability';
import { AssessmentService } from 'src/app/services/assessment/assessment.service';
import { AvailableDoctor } from 'src/app/interfaces/available-doctor';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { map } from 'rxjs/operators';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of } from 'rxjs';
import { RouterService } from 'src/app/services/router/router.service';
import { switchMap, catchError } from 'rxjs/operators';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-doctor-select',
  templateUrl: './doctor-select.component.html',
  styleUrls: ['../doctor-styles.css']
})
export class DoctorSelectComponent implements OnInit {

  allDoctors: AvailableDoctor[];
  assessment$: Observable<Assessment | any>;
  assessmentId: number;
  availableDoctors: AvailableDoctor[];
  cancelModal: NgbModalRef;
  collectionSize: number;
  doctorForm: FormGroup;
  filteredDoctorList: AvailableDoctor[];
  hasDoctorSearchFailed: boolean;
  isAvailableDoctorSearching: boolean;
  isDoctorFieldsShown: boolean;
  isDoctorSearching: boolean;
  isSavingSelection: boolean;
  page = 1;
  pageSize = 10;
  referralId: number;
  selectDoctor: FormGroup;
  selectedDoctors: AvailableDoctor[] = [];


  @ViewChild('cancelSelection', null) cancelSelectionTemplate;

  constructor(
    private assessmentService: AssessmentService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private route: ActivatedRoute,
    private routerService: RouterService,
    private toastService: ToastService
  ) { }

  ngOnInit() {
    this.doctorForm = this.formBuilder.group({
      searchDoctor: [],
      doctorDistance: [5],
      pageSize: [10]
    });

    this.assessment$ = this.route.paramMap.pipe(
      switchMap(
        (params: ParamMap) => {
          return this.assessmentService.getAvailableDoctors(+params.get('assessmentId'))
            .pipe(
              map((assessment: AssessmentAvailability) => {
                this.referralId = assessment.referralId;
                this.assessmentId = assessment.id;
                this.allDoctors = assessment.availableDoctors;
                console.log(assessment);
                this.DisplayDoctorsWithinSearchRadius(this.doctorDistance.value);
                return assessment;
              })
            );
        }
      ),
      catchError((err) => {

        this.toastService.displayError({
          title: 'Error',
          message: 'Error Retrieving Assessment Information'
        });

        const emptyAssessment = {} as Assessment;
        return of(emptyAssessment);
      })
    );
    this.OnChanges();
  }

  AddSelectedDoctor(id: number) {
    const doctorFromList = this.allDoctors.find(doctor => doctor.id === id);
    const doctorAlreadySelected = this.selectedDoctors.findIndex(doctor => doctor.id === id);

    doctorFromList.isSelected = true;

    if (doctorAlreadySelected === -1) {
      this.selectedDoctors.push(doctorFromList);
    }
  }

  Cancel() {
    // if selectedDoctors array has values then ask the user for confirmation
    if (this.selectedDoctors.length > 0) {
      this.cancelModal = this.modalService.open(this.cancelSelectionTemplate, {
        size: 'lg'
      });
    } else {
      this.routerService.navigate([`assessment/edit/${this.referralId}`]);
    }
  }

  DisplayDoctorsWithinSearchRadius(searchRadius: number) {
    this.filteredDoctorList = this.allDoctors.filter(doctor => doctor.distance <= searchRadius);
    this.OnSort({column: 'distance', direction: 'desc'});
    // this.UpdateAvailableDoctorList();
  }

  get doctorDistance() {
    return this.doctorForm.controls.doctorDistance;
  }

  get pageSizeField() {
    return this.doctorForm.controls.pageSize;
  }

  OnCancelModalAction(action: boolean) {

    this.cancelModal.close();

    if (action) {
      this.routerService.navigatePrevious();
    }
  }

  OnChanges(): void {
    this.doctorDistance.valueChanges.subscribe(val => {
      this.DisplayDoctorsWithinSearchRadius(val);
    });

    this.pageSizeField.valueChanges.subscribe(val => {
      this.pageSize = val;
      this.UpdateAvailableDoctorList();
    });
  }

  OnSort(event: any) {
    if (event.direction === 'desc') {
      this.filteredDoctorList.sort((a, b) => (a[event.column] > b[event.column]) ? -1 : 1);
    } else {
      this.filteredDoctorList.sort((a, b) => (a[event.column] > b[event.column]) ? 1 : -1);
    }
    this.UpdateAvailableDoctorList();
  }

  PageChanged(page) {
    this.page = page;
    this.UpdateAvailableDoctorList();
  }

  RemoveSelectedDoctor(id: number) {
    this.selectedDoctors = this.selectedDoctors.filter(doctor => doctor.id !== id);
  }

  SearchDoctors() {
    this.routerService.navigate([`assessment/${this.assessmentId}/add-doctor`]);
  }

  ToggleSelection(id: number, event) {
    if (event.target.checked === true) {
      this.AddSelectedDoctor(id);
    } else {
      this.RemoveSelectedDoctor(id);
    }
  }

  UpdateSelectedDoctors() {
    this.isSavingSelection = true;
    const selectedDoctorIds: number[] = [];

    this.selectedDoctors.forEach(doctor => {
      selectedDoctorIds.push(doctor.id);
    });

    const userIds = {
      UserIds: selectedDoctorIds
    };

    this.assessmentService.updateSelectedDoctors(this.assessmentId, userIds)
      .subscribe(() => {
        this.isSavingSelection = false;
        this.toastService.displaySuccess({
          title: 'Success',
          message: 'Assessment Updated'
        });
        this.routerService.navigateByUrl('/referral/list');
      },
      error => {
        this.isSavingSelection = false;
        this.toastService.displayError({
          title: 'Error',
          message: 'Unable to update selected doctors!'
        });
      });
    }

  UpdateAvailableDoctorList() {
    this.availableDoctors = this.filteredDoctorList.slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
  }
}
