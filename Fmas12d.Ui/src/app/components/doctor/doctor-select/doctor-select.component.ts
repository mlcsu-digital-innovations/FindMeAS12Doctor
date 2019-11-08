import { ActivatedRoute, ParamMap } from '@angular/router';
import { Assessment } from 'src/app/interfaces/assessment';
import { AssessmentService } from 'src/app/services/assessment/assessment.service';
import { AvailableDoctor } from 'src/app/interfaces/available-doctor';
import { Component, OnInit, ViewChild } from '@angular/core';
import { map } from 'rxjs/operators';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of } from 'rxjs';
import { RouterService } from 'src/app/services/router/router.service';
import { switchMap, catchError } from 'rxjs/operators';
import { ToastService } from 'src/app/services/toast/toast.service';
import { UserAvailabilityService } from 'src/app/services/user-availability/user-availability.service';

@Component({
  selector: 'app-doctor-select',
  templateUrl: './doctor-select.component.html',
  styleUrls: ['./doctor-select.component.css']
})
export class DoctorSelectComponent implements OnInit {


  allDoctors: AvailableDoctor[];
  assessment$: Observable<Assessment | any>;
  availableDoctors: AvailableDoctor[];
  cancelModal: NgbModalRef;
  collectionSize: number;
  doctorForm: FormGroup;
  hasDoctorSearchFailed: boolean;
  isAvailableDoctorSearching: boolean;
  isDoctorFieldsShown: boolean;
  isDoctorSearching: boolean;
  page = 1;
  pageSize = 10;
  selectDoctor: FormGroup;
  selectedDoctors: AvailableDoctor[] = [];
  unknownDoctorId: number;

  @ViewChild('cancelAssessment', null) cancelAssessmentTemplate;

  constructor(
    private assessmentService: AssessmentService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private route: ActivatedRoute,
    private routerService: RouterService,
    private toastService: ToastService,
    private userAvailabilityService: UserAvailabilityService
  ) { }

  ngOnInit() {
    this.unknownDoctorId = 0;

    this.doctorForm = this.formBuilder.group({
      searchDoctor: [],
      doctorDistance: [],
      pageSize: [10]
    });

    this.assessment$ = this.route.paramMap.pipe(
      switchMap(
        (params: ParamMap) => {
          return this.assessmentService.getAssessment(+params.get('assessmentId'))
            .pipe(
              map(assessment => {
                return assessment;
              })
            );
        }
      ),
      catchError((err) => {

        this.toastService.displayError({
          title: 'Error',
          message: 'Error Retrieving Referral Information'
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

    doctorFromList.selected = true;

    if (doctorAlreadySelected === -1) {
      this.selectedDoctors.push(doctorFromList);
    }
  }

  Cancel() {
    // if selectedDoctors array has values then ask the user for confirmation
    if (this.selectedDoctors.length > 0) {
      this.cancelModal = this.modalService.open(this.cancelAssessmentTemplate, {
        size: 'lg'
      });
    } else {
      this.routerService.navigatePrevious();
    }
  }

  FetchAvailableDoctors(maxDistance: number) {
    this.isAvailableDoctorSearching = true;
    this.userAvailabilityService.getAvailableDoctors(maxDistance)
    .subscribe(x => {
      this.isAvailableDoctorSearching = false;
      this.allDoctors = x;
      this.collectionSize = this.allDoctors.length;
      this.allDoctors.sort((a, b) => (a.distanceFromAssessment > b.distanceFromAssessment) ? 1 : -1);
      this.UpdateAvailableDoctorList();
    }
    , (err) => {
      this.isAvailableDoctorSearching = false;
      this.toastService.displayError({
        title: 'Search Error',
        message: 'Error Retrieving Available Doctors'
      });
    });
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
      this.FetchAvailableDoctors(val);
    });

    this.pageSizeField.valueChanges.subscribe(val => {
      this.pageSize = val;
      this.UpdateAvailableDoctorList();
    });
  }

  OnSort(event: any) {
    if (event.direction === 'desc') {
      this.allDoctors.sort((a, b) => (a[event.column] > b[event.column]) ? -1 : 1);
    } else {
      this.allDoctors.sort((a, b) => (a[event.column] > b[event.column]) ? 1 : -1);
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
    this.routerService.navigate(['search-doctors']);
  }

  ToggleSelection(id: number, event) {
    if (event.target.checked === true) {
      this.AddSelectedDoctor(id);
    } else {
      this.RemoveSelectedDoctor(id);
    }
  }

  UpdateAssessment() {
    // ToDo: use service to update the assessment with the selected doctors
    console.log('Save details ...');
  }

  UpdateAvailableDoctorList() {
    this.availableDoctors = this.allDoctors.slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
  }
}
