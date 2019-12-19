import { ActivatedRoute, ParamMap } from '@angular/router';
import { AllocationConfirmation } from 'src/app/interfaces/allocation-confirmation';
import { Assessment } from 'src/app/interfaces/assessment';
import { AssessmentSelected } from 'src/app/interfaces/assessment-selected';
import { AssessmentService } from 'src/app/services/assessment/assessment.service';
import { AvailableDoctor } from 'src/app/interfaces/available-doctor';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { of, Observable } from 'rxjs';
import { RouterService } from 'src/app/services/router/router.service';
import { switchMap, map, catchError } from 'rxjs/operators';
import { ToastService } from 'src/app/services/toast/toast.service';


@Component({
  selector: 'app-doctor-allocate',
  templateUrl: './doctor-allocate.component.html',
  styleUrls: ['../doctor-styles.css']
})
export class DoctorAllocateComponent implements OnInit {

  allocatedDoctors: AvailableDoctor[] = [];
  allocationModal: NgbModalRef;
  assessment$: Observable<Assessment | any>;
  assessment: AssessmentSelected;
  assessmentId: number;
  cancelModal: NgbModalRef;
  confirmModal: NgbModalRef;
  doctorForm: FormGroup;
  isSavingAllocation: boolean;
  isSchedulingAssessment: boolean;
  selectDoctor: FormGroup;
  selectedDoctors: AvailableDoctor[] = [];
  unknownDoctorId: number;

  @ViewChild('cancelAssessment', null) cancelAssessmentTemplate;
  @ViewChild('confirmSelection', null) confirmSelectionTemplate;
  @ViewChild('allocationModal', null) confirmAllocationTemplate;

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
      doctorDistance: [],
      pageSize: [10]
    });

    this.assessment$ = this.route.paramMap.pipe(
      switchMap(
        (params: ParamMap) => {
          return this.assessmentService.getSelectedDoctors(+params.get('assessmentId'))
            .pipe(
              map((assessment: AssessmentSelected) => {
                this.assessmentId = assessment.id;
                this.assessment = assessment;
                this.selectedDoctors = assessment.doctorsSelected;
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
  }

  AllDoctorsAllocated() {
    this.allocationModal = this.modalService.open(
      this.confirmAllocationTemplate,
      { size: 'lg' }
    );
  }

  AllocatedDoctorsToAssessment() {

    this.isSavingAllocation = true;

    const allocatedDoctorIds: number[] = [];

    this.allocatedDoctors.forEach(doctor => {
      allocatedDoctorIds.push(doctor.id);
    });

    const userIds = {
      UserIds: allocatedDoctorIds
    };

    this.assessmentService.updateAllocatedDoctors(this.assessmentId, userIds)
      .subscribe(() => {
        this.isSavingAllocation = false;
        this.toastService.displaySuccess({
          title: 'Success',
          message: 'Doctors Allocated'
        });
        this.routerService.navigateByUrl('/referral/list');
      },
      error => {
        this.isSavingAllocation = false;
        this.toastService.displayError({
          title: 'Error',
          message: 'Unable to allocate doctors!'
        });
      });
  }

  AddSelectedDoctor(id: number) {
    const doctorFromList = this.selectedDoctors.find(doctor => doctor.id === id);
    const doctorAlreadySelected = this.allocatedDoctors.findIndex(doctor => doctor.id === id);

    doctorFromList.isSelected = true;

    if (doctorAlreadySelected === -1) {
      this.allocatedDoctors.push(doctorFromList);
    }
  }

  Cancel() {
    if (this.allocatedDoctors.length > 0) {
      this.cancelModal = this.modalService.open(this.cancelAssessmentTemplate, {
        size: 'lg'
      });
    } else {
      this.routerService.navigatePrevious();
    }
  }

  ConfirmAllocation() {
    this.confirmModal = this.modalService.open(
      this.confirmSelectionTemplate,
      { size: 'lg' }
    );
  }

  OnAllocationAction(confirmation: AllocationConfirmation) {

    this.allocationModal.close();

    if (confirmation.confirmed === true) {
      this.isSchedulingAssessment = true;
      this.assessmentService.scheduleAssessment(this.assessmentId, confirmation.scheduledDate)
      .subscribe(() => {
        this.isSchedulingAssessment = false;
        this.toastService.displaySuccess({
          title: 'Success',
          message: 'Assessment Scheduled'
        });
        this.routerService.navigateByUrl('/referral/list');
      },
      error => {
        this.isSchedulingAssessment = false;
        this.toastService.displayError({
          title: 'Error',
          message: 'Unable to schedule assessment'
        });
      });
    }
  }

  OnCancelConfirmAction(action: boolean) {
    this.confirmModal.close();
    if (action) {
      this.AllocatedDoctorsToAssessment();
    }
  }

  OnCancelModalAction(action: boolean) {
    this.cancelModal.close();
    if (action) {
      this.routerService.navigatePrevious();
    }
  }

  OnSort(event: any) {
    if (event.direction === 'desc') {
      this.selectedDoctors.sort((a, b) => (a[event.column] > b[event.column]) ? -1 : 1);
    } else {
      this.selectedDoctors.sort((a, b) => (a[event.column] > b[event.column]) ? 1 : -1);
    }
  }

  RemoveSelectedDoctor(id: number) {
    this.allocatedDoctors = this.allocatedDoctors.filter(doctor => doctor.id !== id);
  }

  ToggleSelection(id: number, event) {
    if (event.target.checked === true) {
      this.AddSelectedDoctor(id);
    } else {
      this.RemoveSelectedDoctor(id);
    }
  }
}
