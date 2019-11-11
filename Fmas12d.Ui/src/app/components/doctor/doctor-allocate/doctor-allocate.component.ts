import { ActivatedRoute, ParamMap } from '@angular/router';
import { Assessment } from 'src/app/interfaces/assessment';
import { AssessmentAvailability } from 'src/app/interfaces/assessment-availability';
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
  assessment$: Observable<Assessment | any>;
  assessmentId: number;
  cancelModal: NgbModalRef;
  doctorForm: FormGroup;
  selectDoctor: FormGroup;
  unknownDoctorId: number;

  @ViewChild('cancelAssessment', null) cancelAssessmentTemplate;

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
          return this.assessmentService.getAvailableDoctors(+params.get('assessmentId'))
            .pipe(
              map((assessment: AssessmentAvailability) => {
                this.assessmentId = assessment.id;
                console.log(assessment);
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

  Cancel() {
    if (this.allocatedDoctors.length > 0) {
      this.cancelModal = this.modalService.open(this.cancelAssessmentTemplate, {
        size: 'lg'
      });
    } else {
      this.routerService.navigatePrevious();
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
      this.allDoctors.sort((a, b) => (a[event.column] > b[event.column]) ? -1 : 1);
    } else {
      this.allDoctors.sort((a, b) => (a[event.column] > b[event.column]) ? 1 : -1);
    }
    this.UpdateAvailableDoctorList();
  }

  UpdateAssessment() {

  }
}
