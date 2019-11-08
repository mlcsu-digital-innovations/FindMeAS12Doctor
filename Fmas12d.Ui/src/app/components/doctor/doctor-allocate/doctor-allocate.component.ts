import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Assessment } from 'src/app/interfaces/assessment';
import { of, Observable } from 'rxjs';
import { AssessmentService } from 'src/app/services/assessment/assessment.service';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { RouterService } from 'src/app/services/router/router.service';
import { ToastService } from 'src/app/services/toast/toast.service';
import { UserAvailabilityService } from 'src/app/services/user-availability/user-availability.service';
import { switchMap, map, catchError } from 'rxjs/operators';
import { AvailableDoctor } from 'src/app/interfaces/available-doctor';

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
    private toastService: ToastService,
    private userAvailabilityService: UserAvailabilityService
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
          return this.assessmentService.getAssessment(+params.get('assessmentId'))
            .pipe(
              map((assessment: Assessment) => {
                this.assessmentId = assessment.id;
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

  UpdateAssessment() {

  }
}
